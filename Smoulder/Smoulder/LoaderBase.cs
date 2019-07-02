using System;
using System.Collections.Concurrent;
using System.Threading;
using Smoulder.Interfaces;

namespace Smoulder
{
    public class LoaderBase<T> : ILoader<T> where T : new()
    {
        private BlockingCollection<T> _processorQueue;

        private Func<CancellationToken, T> _action = token => throw new NotImplementedException();
        private Action<Exception> _onError = e => { };
        private Action _startup = () => { };
        private Action _finalise = () => { };

        public void RegisterProcessorQueue(BlockingCollection<T> processorQueue)
        {
            _processorQueue = processorQueue;
        }

        public int GetProcessorQueueCount()
        {
            return _processorQueue.Count;
        }

        public void Enqueue(T itemToEnqueue)
        {
            _processorQueue.Add(itemToEnqueue);
        }

        public void Start(CancellationToken cancellationToken)
        {
            Startup();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Enqueue(Action(cancellationToken));
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

        public virtual T Action(CancellationToken cancellationToken)
        {
            return _action(cancellationToken);
        }

        public void SetAction(Func<CancellationToken, T> action)
        {
            _action = action;
        }

        public virtual void Finalise()
        {
            _finalise();
        }

        public virtual void SetFinalise(Action finalise)
        {
            _finalise = finalise;
        }

        public virtual void OnError(Exception e)
        {
            _onError(e);
        }

        public virtual void SetOnError(Action<Exception> onError)
        {
            _onError = onError;
        }
    }
}
