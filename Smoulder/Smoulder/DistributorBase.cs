﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class DistributorBase<TDistributeData> : IDistributor<TDistributeData> where TDistributeData : new()
    {
        private BlockingCollection<TDistributeData> _distributorQueue;
        private ConcurrentQueue<TDistributeData> _underlyingQueue;
        protected int Timeout = 1000;


        private Action<TDistributeData, CancellationToken> _action = (data, token) => throw new NotImplementedException();
        private Action<CancellationToken> _onEmptyQueue = token => { };
        private Action<Exception> _onError = e => { };
        private Action _startup = () => { };
        private Action _finalise = () => { };

        public void RegisterDistributorQueue(BlockingCollection<TDistributeData> distributorQueue, ConcurrentQueue<TDistributeData> underlyingQueue)
        {
            _distributorQueue = distributorQueue;
            _underlyingQueue = underlyingQueue;
        }

        public int GetDistributorQueueCount()
        {
            return _distributorQueue.Count;
        }

        public bool Dequeue(out TDistributeData item)
        {
            return _distributorQueue.TryTake(out item, Timeout);
        }

        public bool Peek(out TDistributeData item)
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
                    if (_distributorQueue.TryTake(out var item, Timeout, cancellationToken))
                    {
                        Action(item, cancellationToken);
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

        public virtual void Action(TDistributeData item, CancellationToken cancellationToken)
        {
            _action(item, cancellationToken);
        }

        public void SetAction(Action<TDistributeData, CancellationToken> action)
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
