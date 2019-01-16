# Smoulder

## TODO
- Play with the namespaces to try and find a better solution than Smoulder.Smoulder

## Introduction
Smoulder meets the need for low-profile, slow burn processing of data in a non-time critical environment. Intended uses include aggregating statistics over a constant data stream and creating arbitrarily complex reports on large data volumes with the ability to take snapshots of the current resultant states without waiting for completion or interrupting the data processing. This is achieved by separating data preparation, processing and aggregation of results into separate loosely-coupled processes, linked together by defined data packets through internally accessible message queues.
The entire system can be implemented by creating new concrete implementations of the base abstract interfaces, allowing for real flexibility in the applications the system can be used for. Each of the three parts of the system is built to be run in a separate thread, allowing performance to scale up or down depending on the hardware the system is hosted on and decoupling each process from the other. They will communicate with each other over two concurrent queues, a thread-safe feature of C#.
## System Description
### Loader
This component is responsible for retrieving data and converting it into usable data packets. Data can then be bundled into a data packet containing multiple data points, or simply applied as a stream of single data points using a customisable data object. This stream of sanitised data is then made available to the Processor by means of an inter-thread message queue called the ProcessorQueue.
### Processor
Responsible for computing the results from the provided data packet, retains necessary information about previous data packets if required. This could be keeping track of a cumulative number (e.g. number of data objects meeting a certain criterion) or calculated statistics (e.g. number of peaks in a continuous data stream).
These produced results are bundled into a results object that is then made available to the Distributor by means of a message queue.
### Distributor
Responsible for calculating the up-to-date status of the tracked statistics and providing them to external components that are polling for the results. This decouples the reporting and processing elements, ensuring that the polling for results doesn’t affect processor performance and large data volumes won’t slow down returning of results. The distributor could also be configured to publish data to an external service or database while still allowing the processor to remain agnostic or update a database with updated values. The action the distributor takes is deliberately open-ended, allowing developers to tailor the result to each individual need.
## Example hypothetical use cases
### TrainData
Smoulder has been designed in this way to meet the needs of data processing that is either not time critical or inefficient to achieve in one batch. A potential case would be the processing of train scheduling data into a table of active trains for use by the departure board. Currently, the processing of this data is done in singular, monolithic batches that can take up to 4 mins to complete. A potential application of the Smoulder design would be to have each activation message be a data packet processed by the Loader, the processor then retrieves the necessary schedule data from the database creating a result object that represents a departure board row. The Aggregator would then update and curate database table of active schedules, deleting out-of-date data and replacing it with update data. This database table would take the place of the trainDataCache, allowing for fast recall of this data to multiple instances of the departure board app.
### PAM KPI Calculation
Smoulder could see use as an alternative to the KPI calculations in PAM. The current system is like the trainData processing, doing large batches of processing all at once. At a set periodicity, the current system takes every single wagon in the database and calculates the KPI statistics for it, saving each result as a row in a kpi stats table. Smoulder could be setup to slowly continuously trawl through the wagon table and calculate the KPI states at that point in time for each wagon and update the relevant row in the kpi stats table. Ultimately, the total amount of processing would be the same, but the change in peak processing would be pronounced.
## Cost-effectiveness
In both examples, the processing of the information is done in large discrete batch processes. This means the graph of processing power over time has large spikes in it. This has significant cost implications as services and products are migrated to Azure. To accommodate for these large peaks, the Azure tier needs to be set at a sufficiently powerful level to be able to absorb the sudden increases in load. This pattern is shown in the TrainData dtu usage. Due to the slow-burn nature of the proposed system the Azure hosting service could be set to a lower and less expensive tier. In the example, hypothetical and exaggerated data, the area under each graph is similar, but the Azure tier can be set to be less powerful, reducing the amount of wasted resource being paid for.

## Working Examples
### Random Number Generator
This simply generates random numbers and passes them through, doing some nominal work on them to show that data could get from one end to the other.

### Temperature Analysis
Takes a cutdown temperature data file generated by a USB dongle and finds useful temperature stats about the office. This is obviously a batch process, which just doesn't fit the continuous data setup that smoulder encourages. I suggest the best way to use smoulder in a batch process would be to on the timed event create a smoulder, then when all the work is done stop the smoulder and wait for the next timed interval. **This would require the ability for the worker units to report they are finished back up to the Smoulder object. At the moment the Smoulder object can cancel the worker units, but not the other way round**

### StockMarket Analysis
The point of this is obviously not to actually make any money, but the www.alphavantage.co API is free, easier to access than train data source, the dataset is pretty interesting and most importantly the dataset is continuous. This will allow me to set this running and watch memory/CPU usage over time to find any leaks and/or allow me to optimise how the system runs. The loader will scrape data for a small number of stocks, the processor will decide if one should buy/sell the stock and save the stock data to the database and the distributor will tell the user if a buy/sell action should be taken. I should be able later to look at the historic price and buy/sell actions to see if the process would have made any money.

Changing the Task.Delays to Thread.Sleeps has made the CPU usage make sense, no longer maxes out at 100%. Means the StockMarket exercise has already been fortuitous. Further, it's lead to optional parameters for the startup() methods, a good step forward methinks.

####Planned Improvements
There are improvements I would like to make to this program, but they are domain specific and wouldn't help the Smoulder project progress. That said:
- Get the stock price at the point of the buy/sell decision in the distributor so I can work out if the system would actually have worked.
- Instead of taking .First from the slowK and slowD data series, use all the data returned, then analyse all the new data. This will significantly improve the granularity of the decision making process and there is easily the processing time to spare.

###TrainData Consumer
Martin can give me an ActiveMQ with some real train data on it coming down from TD.NET. It's his test connection, so this would be a good use for it until the project kicks off. The throughput will be much higher than the StockMarket Analysis so it should make for a good progression. This is not intended to be a prototype, first version nor to see the light of day. It is intended to be a test bed for Smoulder using high-volume, relevant, real-world data.

Do I want 1 Smoulder for each data type? That would be preferable methinks. Wonder if I can reuse the loader if I write it generic. Good thing the startup() is configurable...

## Setup
### Data Objects
Decide the form of your data objects. One will implement IProcessDataObject, the other will implement IDistributeDataObject.

### Worker units
Create Loader, Processor and Distributor classes that are members of ILoader, IProcessor and IDistributor. Override the Action() method for each with the action that you want the worker unit to take each cycle. Optionally, you can override the Finalise() method on each to do any data cleanup/final reporting when the worker unit is stopped. Consider that there may be data on the queues when the units are stopped, so you may want to either allow all the data to stream through the pipeline before close, dump all the data still in the queues to a file etc.

The Loader has access to the ProcessorQueue, the Processor has access to the ProcessorQueue and DistributorQueue, and the Distributor has access to the Distributor Queue. The queues will be hooked up in the Smoulder.Start() method, so you can assume they are successfully hooked up by the time you get to the Action() method.
The implementation of the worker units should:
-implement relevant interface
-Extend the relevant workerUNitBase (i.e. myProcessor: ProcessorBase, IProcessor)
-Override the Action() method

Optionally, the implementation can override:
-one of the overloads of Startup(), which is called when the Smoulder.Start() method is called. This allows you to initialise any variables from the passed in startupParameters object that can't be done in the constructor.
-the Finalise() which will be called when the Smoulder.Stop() method is called. This could be used to ensure all the remaining data is processed before the smoulder shuts down.