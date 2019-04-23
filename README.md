# Smoulder

## TODO
- Use to create an azure function to work out any potential issues with that.
- Rename OnNoQueueItem() to something less blathery

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
## Cost-effectiveness
In both examples, the processing of the information is done in large discrete batch processes. This means the graph of processing power over time has large spikes in it. This has significant cost implications as services and products are migrated to Azure. To accommodate for these large peaks, the Azure tier needs to be set at a sufficiently powerful level to be able to absorb the sudden increases in load. This pattern is shown in the TrainData dtu usage. Due to the slow-burn nature of the proposed system the Azure hosting service could be set to a lower and less expensive tier. In the example, hypothetical and exaggerated data, the area under each graph is similar, but the Azure tier can be set to be less powerful, reducing the amount of wasted resource being paid for. This has shown to be a reality, with the hosting for TD.net being turned down from P1 to S0 Azure tier, a power of magnitude cheaper.

## Setup
### Data Objects
Decide the form of your data objects. One will become `TProcessData`, the other will become `TDistributeData` when passed to the generic `Build<TProcessData,TDistributeData>()` method. This will produce a `Smoulder<TProcessData,TDistributeData>`, giving type safety to the `Enqueue`/`Dequeue` calls.

### Worker units
Create `Loader`, `Processor` and `Distributor` classes that implement `ILoader`, `IProcessor` and `IDistributor`. The `Loader` has access to the `ProcessorQueue`, the `Processor` has access to the `ProcessorQueue` and `DistributorQueue`, and the `Distributor` has access to the `DistributorQueue`. The queues will be hooked up in the `Smoulder.Start()` method, so you can assume they are successfully hooked up by the time you get to the `Action()` method.

The implementation of the worker units should:
- implement relevant interface
- Extend the relevant workerUnitBase (i.e. `myProcessor: ProcessorBase<myProcessData,myDistributeData>, IProcessor`)
- Override the `Action()` method

Optionally, the implementation can override:
- one of the overloads of `Startup()`, which is called when the Smoulder.Start() method is called. This allows you to initialise any variables from the passed in startupParameters object that can't be done in the constructor.
- the `Finalise()` which will be called when the Smoulder.Stop() method is called. This could be used to ensure all the remaining data is processed before the smoulder shuts down.
- the `OnNoQueueItem()` method, which is called if there was nothing on the queue for $Timeout number of milliseconds
- the `OnError()` method, which is called if there is an uncaught error in Action(), OnNoQueueItem() or the method inside of smoulder that contians the dequeuing logic.

Any of these methods has access to:
- 
#### Action(TData item)
The `Action(TData item)` method on a workerUnit is the main payload. This is what will be called continuously until the `Smoulder.Stop()` method is called. A reccommended format for the action method for a processor would be:

    public override void Action(TProcessData item CancellationToken cancellationToken)
    {
        TDistributeData outgoingData = Do.Something(item);
            
        Enqueue(rawData);
    }

#### Available methods
##### Loader
- `Enqueue(TData itemToEnqueue)` - enqueues the item onto the `ProcessorQueue`
- `int GetProcessorQueueCount()` - Returns the number of items on the `ProcessorQueue`

##### Processor
- `Enqueue(TDistributeData itemToEnqueue)` - enqueues the item onto the `DistributorQueue`
- `bool Dequeue(out TProcessData item)` - does a TryTake(item, Timeout) on the `ProcessorQueue`. You usually don't have to call this as it is called for you and the result passed to Action(), but it's available for completeness. You could use:

        public override void Finalise()
        {
            while (Dequeue(out var incomingData))
            {
                ProcessData(incomingData);
            }
        }

to cycle through all the remaining items on the queue for example.

- `bool Peek(out TProcessData item)` - Allows for peeking the `ProcessorQueue`. This is implemented here as BlockingCollections don't allow it normally due to multithreading concerns. There is only one producer and consumer for each queue, so Peek is assumed to be safe. If you break it, let me know.
- `int GetDistributorQueueCount()` - Duh
- `int GetProcessorQueueCount()` - Duh

##### Distributor
- `bool Dequeue(out TDistributeData item)` - enqueues the item onto the `DistributorQueue`
- `bool Peek(out TDistributeData item)`- Allows for peeking the `DistributorQueue`.
- `int GetDistributorQueueCount()` - Duh

#### Startup()
Startup is deliberatly distict to the constructor so that a Smoulder can be restarted after being stopped. The constructor will only run when the workerUnits are instantiated, but the Startup method is called every time the Smoulder.Start() method is called. An example for a use for this is closing a connection to a message queue in the Finalise() method and connecting in the Startup() method.

### Instantiation
Once classes for the workerUnits have been created, a Smoulder object can be instantiated. This is acheived with following two lines:

    var smoulderFactory = new SmoulderFactory();
    var smoulder = smoulderFactory.Build(new Loader(), new Processor(), new Distributor());
    
Obviously, any way of creating concrete implementations of the workerUnits can be done, such as through an IOC container.
After instantiating the Smoulder object, starting it is as simple as:

    smoulder.Start();
    
While stopping can be achieved with:

    smoulder.Stop();
    
When the `Start()` and `Stop()` methods are called is left entirely up to the implementing developer. For example, If the smoulder is being used inside a windows service the `OnStart()` and `OnStop()` methods are obvious candidates.

# Advanced Features

## QueueBounding
The maximum number of items allowed in the queues can be set when the `SmoulderFactory.Build()` is called.
The following line sets the maximum number of ProcessData objects to 50 and the number of DistributeData objects to 100.

    var smoulder = smoulderFactory.Build(_movementLoader, _movementProcessor, _movementDistributor,50,100);
    
The `Enqueue()` method on `Loader`s and `Processor`s will block until an item is removed.

## Dequeue timeout
By default smoulder will wait `1000`milliseconds before giving up waiting for an item on the queue and calling `OnNoQueueItem()` instead. This can be changed by setting the `Timeout` attribute on the `Processor` and `Distributor` objects.
If the timeout is set to `-1`, it will wait forever or until an item arrives on the queue.

## Multiple Smoulders with IOC
Say you want two smoulder objects in the same application and you're using an IOC container. You have two different implementations of `ILoader`. The best way to split these up so you're IOC container knows which is which is to create two interfaces, say `IProcessorA` and `IProcessorB` that both implement `IProcessor`. Then you can hook your IOC up using these two interfaces and everything is peachy.

## Working Examples
### Random Number Generator
This simply generates random numbers and passes them through, doing some nominal work on them to show that data could get from one end to the other.

### Temperature Analysis
Takes a cutdown temperature data file generated by a USB dongle and finds useful temperature stats about the office. This is obviously a batch process, which just doesn't fit the continuous data setup that smoulder encourages. I suggest the best way to use smoulder in a batch process would be to on the timed event create a smoulder, then when all the work is done stop the smoulder and wait for the next timed interval. **This would require the ability for the worker units to report they are finished back up to the Smoulder object. At the moment the Smoulder object can cancel the worker units, but not the other way round**

### StockMarket Analysis
The point of this is obviously not to actually make any money, but the www.alphavantage.co API is free, easier to access than train data source, the dataset is pretty interesting and most importantly the dataset is continuous. This will allow me to set this running and watch memory/CPU usage over time to find any leaks and/or allow me to optimise how the system runs. The loader will scrape data for a small number of stocks, the processor will decide if one should buy/sell the stock and save the stock data to the database and the distributor will tell the user if a buy/sell action should be taken. I should be able later to look at the historic price and buy/sell actions to see if the process would have made any money.

Changing the Task.Delays to Thread.Sleeps has made the CPU usage make sense, no longer maxes out at 100%. Means the StockMarket exercise has already been fortuitous. Further, it's lead to optional parameters for the startup() methods, a good step forward methinks.

#### Planned Improvements
There are improvements I would like to make to this program, but they are domain specific and wouldn't help the Smoulder project progress. That said:
- Get the stock price at the point of the buy/sell decision in the distributor so I can work out if the system would actually have worked.
- Instead of taking .First from the slowK and slowD data series, use all the data returned, then analyse all the new data. This will significantly improve the granularity of the decision making process and there is easily the processing time to spare.

### TrainData Consumer
Martin can give me an ActiveMQ with some real train data on it coming down from TD.NET. It's his test connection, so this would be a good use for it until the project kicks off. The throughput will be much higher than the StockMarket Analysis so it should make for a good progression. This is not intended to be a prototype, first version nor to see the light of day. It is intended to be a test bed for Smoulder using high-volume, relevant, real-world data.

Do I want 1 Smoulder for each data type? That would be preferable methinks. Wonder if I can reuse the loader if I write it generic. Good thing the startup() is configurable...

Edit - SmoulderV3 is now being used in the TrainData project to do just this.

## Example hypothetical use cases
### PAM KPI Calculation
Smoulder could see use as an alternative to the KPI calculations in PAM. The current system is like the trainData processing, doing large batches of processing all at once. At a set periodicity, the current system takes every single wagon in the database and calculates the KPI statistics for it, saving each result as a row in a kpi stats table. Smoulder could be setup to slowly continuously trawl through the wagon table and calculate the KPI states at that point in time for each wagon and update the relevant row in the kpi stats table. Ultimately, the total amount of processing would be the same, but the change in peak processing would be pronounced.
### RDD
Processing large volumes of incoming data

### Rostering
Standardise the internal workings of their microservices the sync data with EDS
