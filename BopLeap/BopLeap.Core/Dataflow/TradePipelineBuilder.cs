using System.Threading.Tasks.Dataflow;
using BopLeap.Core.Models;

namespace BopLeap.Core.Dataflow;

/// <summary>
/// Dataflow pipeline for trades with batching, retry, and fan-out.
/// </summary>
public class TradePipelineBuilder
{
    public ITargetBlock<Trade> EntryPoint { get; }

    public TradePipelineBuilder(
        ActionBlock<Trade[]> influxWriter,
        ActionBlock<Trade[]> alertNotifier,
        int batchSize = 100)
    {
        var buffer = new BufferBlock<Trade>(
            new DataflowBlockOptions
            {
                BoundedCapacity = DataflowBlockOptions.Unbounded
            });

        var batch = new BatchBlock<Trade>(batchSize);

        var retryingTransform = new TransformBlock<Trade[], Trade[]>(async trades =>
        {
            // Polly goes here 
            return trades;
        });

        var broadcast = new BroadcastBlock<Trade[]>(batchArray => batchArray);

        buffer.LinkTo(batch, new DataflowLinkOptions { PropagateCompletion = true });
        batch.LinkTo(retryingTransform, new DataflowLinkOptions { PropagateCompletion = true });
        retryingTransform.LinkTo(broadcast, new DataflowLinkOptions { PropagateCompletion = true });

        broadcast.LinkTo(influxWriter, new DataflowLinkOptions { PropagateCompletion = true });
        broadcast.LinkTo(alertNotifier, new DataflowLinkOptions { PropagateCompletion = true });

        EntryPoint = buffer;
    }
}