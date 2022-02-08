using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charts;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DaoSaver 
{
	public class MarketStep : TableEntity
    {
	    public float Open { get; set; }

	    public float High { get; set; }

	    public float Low { get; set; }

	    public float Close { get; set; }
	    public long Volume { get; set; }

	    public static async Task<List<MarketStep>> GetSteps(string indice, DateTime startDate)
	    {
		    int days = (DateTime.Now - startDate).Days;
		    return await GetSteps(indice, days);
	    }

	    public static async Task<List<MarketStep>> GetSteps(string indice, int days = 5)
	    {
		    var steps = new List<MarketStep>();
		    TableContinuationToken token = null;

		    var startDateString = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
		    do
		    {
			    string startDateFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, startDateString);
			    string marketKeyFiler = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, indice);
			    string finalFilter = TableQuery.CombineFilters(startDateFilter, TableOperators.And, marketKeyFiler);

			    var query = new TableQuery<MarketStep>().Where(finalFilter);

			    // var query = new TableQuery<MarketStep>()
				   //  .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, startDateString))
				   //  .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, indice));

			    var seg = await Startup.History.ExecuteQuerySegmentedAsync<MarketStep>(query, token);
			    token = seg.ContinuationToken;
			    steps.AddRange(seg);
		    } while (token != null);

		    return steps.OrderBy(t => t.RowKey).ToList();
	    }

	    public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
	    {
		    base.ReadEntity(properties, operationContext);
		    this.Open = (float)properties["Open"].DoubleValue;
		    this.High = (float)properties["High"].DoubleValue;
		    this.Low = (float)properties["Low"].DoubleValue;
		    this.Close = (float)properties["Close"].DoubleValue;
		    this.Volume = (long)properties["Volume"].Int64Value;
	    }
    }
}
