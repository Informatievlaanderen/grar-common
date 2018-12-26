namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Bus
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    internal class Bus : IDisposable
    {
        private readonly ConcurrentBag<IDataflowBlock> _consumers;
        private readonly BufferBlock<object> _queue;
        private readonly ConcurrentBag<IDisposable> _subscriptions;

        public Bus(int capacity = 100)
        {
            _queue = new BufferBlock<object>(new DataflowBlockOptions {BoundedCapacity = capacity});
            _subscriptions = new ConcurrentBag<IDisposable>();
            _consumers = new ConcurrentBag<IDataflowBlock>();
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions) subscription.Dispose();
        }

        public async Task ProduceAsync<T>(T item)
        {
            await _queue.SendAsync(item);
        }

        public void SubscribeConsumer<T>(Action<T> consumerAction)
        {
            var consumer = new ActionBlock<object>(m => consumerAction((T) m),
                new ExecutionDataflowBlockOptions {BoundedCapacity = 1});

            var subscription = _queue.LinkTo(consumer, new DataflowLinkOptions
            {
                PropagateCompletion = true
            }, m => m is T);

            _consumers.Add(consumer);
            _subscriptions.Add(subscription);
        }

        public Task Complete()
        {
            _queue.Complete();
            var dataFlows = new[] {_queue}.Concat(_consumers).ToList();
            //foreach (var dataFlow in dataFlows)
            //{
            //    dataFlow.Complete();
            //}
            return Task.WhenAll(dataFlows.Select(x => x.Completion));
        }
    }
}
