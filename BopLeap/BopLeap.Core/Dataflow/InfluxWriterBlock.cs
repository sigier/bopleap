using System.Threading.Tasks.Dataflow;
using BopLeap.Core.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace BopLeap.Core.Dataflow
{
    internal static class InfluxWriterBlock
    {
        public static ActionBlock<Trade[]> Create(string influxUrl, string token, string org, string bucket)
        {
            var client = new InfluxDBClient(influxUrl, token);

            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 1,
                BoundedCapacity = 10
            };

            return new ActionBlock<Trade[]>((Func<Trade[], Task>)(trades =>
            {
                try
                {
                    using var writeApi = client.GetWriteApi();

                    var points = trades.Select(trade => PointData.Measurement("trades")
                            .Tag("symbol", trade.Symbol)
                            .Field("price", trade.Price)
                            .Field("quantity", trade.Quantity)
                            .Timestamp(trade.EventTime, WritePrecision.Ms))
                        .ToList();

                    writeApi.WritePoints(points, bucket, org);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[InfluxWriter] Error writing to InfluxDB: {ex.Message}");
                }
            }), options);
        }

    }
}
