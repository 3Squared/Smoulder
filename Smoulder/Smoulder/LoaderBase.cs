﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smoulder.Interfaces;

namespace Smoulder
{
    public abstract class LoaderBase : WorkerUnitBase, ILoader
    {
        public ConcurrentQueue<IProcessDataObject> ProcessorQueue;

        public void RegisterProcessorQueue(ConcurrentQueue<IProcessDataObject> processorQueue)
        {
            ProcessorQueue = processorQueue;
        }

        public override async Task Start(CancellationToken cancellationToken)
        {
            await Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Action(cancellationToken);
                }
                catch (Exception e)
                {
                    await CatchError(e);
                }
            }
        }
    }
}
