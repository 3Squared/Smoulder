﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class ProcessorBase<TProcessData, TDistributeData> : IProcessor<TProcessData, TDistributeData> where TProcessData : new()
    {
        private BlockingCollection<TProcessData> _processorQueue;
        private ConcurrentQueue<TProcessData> _underlyingQueue;
        private BlockingCollection<TDistributeData> _distributorQueue;
        protected int Timeout = 1000;

        private Func<TProcessData, CancellationToken, TDistributeData> _action = (data,token) => throw new NotImplementedException();
        private Action<CancellationToken> _onEmptyQueue = token => { };
        private Action<Exception> _onError = e => { };
        private Action _startup = () => { };
        private Action _finalise = () => { };

        public void RegisterProcessorQueue(BlockingCollection<TProcessData> processorQueue, ConcurrentQueue<TProcessData> underlyingQueue)
        {
            _processorQueue = processorQueue;
            _underlyingQueue = underlyingQueue;
        }

        public void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue)
        {
            _distributorQueue = distributorQueue;
        }

        public void Enqueue(TDistributeData itemToEnqueue)
        {
            _distributorQueue.Add(itemToEnqueue);
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public bool Dequeue(out TProcessData item)
        {
            return _processorQueue.TryTake(out item, Timeout);
        }

        public bool Peek(out TProcessData item)
        {
            return _underlyingQueue.TryPeek(out item);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_processorQueue.TryTake(out var item, Timeout, cancellationToken))
                    {
                        Enqueue(Action(item, cancellationToken));
                    }
                    else
                    {
                        OnEmptyQueue(cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    OnError(e);
                }
            }
        }

        public virtual void Startup()
        {
            _startup();
        }

        public void SetStartup(Action startup)
        {
            _startup = startup;

        }

        public virtual TDistributeData Action(TProcessData processData, CancellationToken cancellationToken)
        {
            return _action(processData,cancellationToken);
        }

        public virtual void SetAction(Func<TProcessData, CancellationToken, TDistributeData> action)
        {
            _action = action;
        }

        public virtual void Finalise()
        {
            _finalise();
        }

        public void SetFinalise(Action finalise)
        {
            _finalise = finalise;
        }

        public virtual void OnError(Exception e)
        {
            _onError(e);
        }

        public void SetOnError(Action<Exception> onError)
        {
            _onError = onError;
        }

        public virtual void OnEmptyQueue(CancellationToken cancellationToken)
        {
            _onEmptyQueue(cancellationToken);
        }

        public void SetOnEmptyQueue(Action<CancellationToken> onEmptyQueue)
        {
            _onEmptyQueue = onEmptyQueue;
        }
    }
}
