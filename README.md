#  BopLeap

**BopLeap** is a streaming-friendly trading data engine.

---

## 🛠️ Tech Stack

| Component         | Technology                                 | Purpose                              |
|-------------------|---------------------------------------------|--------------------------------------|
| **Runtime**        | [.NET 8](https://dotnet.microsoft.com)     | Core backend and dataflow logic      |
| **Data Processing**| DataFlow (TPL)                             | Asynchronous in-process pipelines    |
| **Time-series DB** | [InfluxDB 2.x](https://www.influxdata.com) | Storing ticker data & metrics        |
| **Visualization**  | [Grafana](https://grafana.com)             | Real-time dashboards and alerts      |
| **Ticker Source**  |      Trading platforms                     | Live market data ingestion         |

