
using System;
using Azure;
using Azure.Data.Tables;
using Tecan.Function;

public class IotDataProcessed : IotData, ITableEntity
{
  public string PartitionKey { get; set; }
  public string RowKey { get; set; }
  public DateTimeOffset? Timestamp { get; set; }
  public ETag ETag { get; set; }
}