# Smoulder

## Introduction
Smoulder meets the need for low-profile, slow burn processing of data in a non-time critical environment. Intended uses include aggregating statistics over a constant data stream and creating arbitrarily complex reports on large data volumes with the ability to take snapshots of the current resultant states without waiting for completion or interrupting the data processing. This is achieved by separating data preparation, processing and aggregation of results into separate loosely-coupled processes, linked together by defined data packets through internally accessible message queues.
The entire system can be extended by creating new concrete implementations of the base abstract interfaces, allowing for real flexibility in the applications the system can be used for. Each of the three parts of the system is built to be run in a separate thread, allowing performance to scale up or down depending on the hardware the system is hosted on and decoupling each process from the other. They will communicate with each other over two concurrent queues, a thread-safe feature of C#.
System Description

 

### Loader
This component is responsible for converting the incoming data into usable data packets, using customisable data validation to sanitise incoming data. Data can then be bundled into a data packet containing multiple data points, or simply applied as a stream of single data points using a customisable data object. This stream of sanitised data is then made available to the Processor by means of an inter-thread message queue.
### Processor
Responsible for computing the results from the provided data packet, retains necessary information about previous data packets if required. This could be keeping track of a cumulative number (e.g. number of data objects meeting a certain criterion) or calculated statistics (e.g. number of peaks in a continuous data stream).
These produced results are bundled into a results object that is then made available to the Aggregator by means of a message queue.
### Aggregator
Responsible for calculating the up-to-date status of the tracked statistics and providing them to external components that are polling for the results. This decouples the reporting and processing elements, ensuring that the polling for results doesn’t affect processor performance and large data volumes won’t slow down returning of results. The aggregator could also be configured to publish data to an external service while still allowing the processor to remain agnostic or update a database with updated values. The action the aggregator takes is deliberately open-ended, allowing developers to tailor the result to each individual need.
##Example hypothetical use cases
###TrainData
Smoulder has been designed in this way to meet the needs of data processing that is either not time critical or inefficient to achieve in one batch. A potential case would be the processing of train scheduling data into a table of active trains for use by the departure board. Currently, the processing of this data is done in singular, monolithic batches that can take up to 4 mins to complete. A potential application of the Smoulder design would be to have each activation message be a data packet processed by the Loader, the processor then retrieves the necessary schedule data from the database creating a result object that represents a departure board row. The Aggregator would then update and curate database table of active schedules, deleting out-of-date data and replacing it with update data. This database table would take the place of the trainDataCache, allowing for fast recall of this data to multiple instances of the departure board app.
### PAM KPI Calculation
Smoulder could see use as an alternative to the KPI calculations in PAM. The current system is like the trainData processing, doing large batches of processing all at once. At a set periodicity, the current system takes every single wagon in the database and calculates the KPI statistics for it, saving each result as a row in a kpi stats table. Smoulder could be setup to slowly continuously trawl through the wagon table and calculate the KPI states at that point in time for each wagon and update the relevant row in the kpi stats table. Ultimately, the total amount of processing would be the same, but the change in peak processing would be pronounced.
## Cost-effectiveness
In both examples, the processing of the information is done in large discrete batch processes. This means the graph of processing power over time has large spikes in it. This has significant cost implications as services and products are migrated to Azure. To accommodate for these large peaks, the Azure tier needs to be set at a sufficiently powerful level to be able to absorb the sudden increases in load. This pattern is shown in the TrainData dtu usage. Due to the slow-burn nature of the proposed system the Azure hosting service could be set to a lower and less expensive tier. In the example, hypothetical and exaggerated data, the area under each graph is similar, but the Azure tier can be set to be less powerful, reducing the amount of wasted resource being paid for.
 
##Resource Requirement

Development
Task	Estimated days
Lay out blueprint/interface of each component	5
Research and implement starting separate threads for each component	7
Define Data/Result objects	4
Research and implement inter-thread communication	7
Hook-up components to queues	4
Implement simple example of system plus debug	8
Total	35 + 3 contingency = 38 days

Documentation
Task	Estimated Days
README/System description	1
Installation docs	1

Total time – 40 days
