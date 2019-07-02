﻿using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Smoulder.Interfaces
{
    public interface ILoader<T> : IWorkerUnit
    {
        void RegisterProcessorQueue(BlockingCollection<T> processorQueue);
        int GetProcessorQueueCount();
        void Enqueue(T itemToEnqueue);

        void SetAction(Action<CancellationToken> action);
        void Action(CancellationToken cancellationToken);
    }
}
