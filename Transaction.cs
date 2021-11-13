using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DaoSaver
{
	public class Transaction : TableEntity
	{
		public string Type { get; set; }
		public string Units { get; set; }
		public string Price { get; set; }
		public string Value { get; set; }
		public string Profit { get; set; }
		public string Product { get; set; }
		public DateTime TransactionTime { get; set; }

		public static async Task<List<Transaction>> GetTransactions(int days = 60)
		{
			var transactions = new List<Transaction>();
			TableContinuationToken token = null;

			do
			{
				var query = new TableQuery<Transaction>()
					.Where(TableQuery.GenerateFilterConditionForDate("TransactionTime", QueryComparisons.GreaterThanOrEqual, DateTime.Now.AddDays(-days)));

				var seg = await Startup.Transactions.ExecuteQuerySegmentedAsync<Transaction>(query, token);
				token = seg.ContinuationToken;
				transactions.AddRange(seg);
 			} while (token != null);

			return transactions.OrderBy(t => t.Product).ThenBy(t => t.PartitionKey).ToList();
		}
	}
}
