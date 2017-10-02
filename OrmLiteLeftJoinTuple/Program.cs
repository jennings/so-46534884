using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;

namespace OrmLiteLeftJoinTuple
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            using (var db = factory.OpenDbConnection())
            {
                // Populate database
                db.CreateTable<Order>();
                db.CreateTable<LineItem>();
                db.ExecuteSql(@"INSERT INTO ""Order"" (Name) VALUES ('Alpha')");

                var query = db.From<Order>()
                              .LeftJoin<LineItem>()
                              .Where(o => o.OrderId == 1);

                // This line returns a tuple with Item2 populated with "new LineItem()"
                // I expected it to be null, since there is no record in the LineItem table
                var results = db.SelectMulti<Order, LineItem>(query);

                Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));

                Console.WriteLine();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }

    class Order
    {
        [PrimaryKey]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public override string ToString() => $"OrderId = {OrderId}, Name = {Name}";
    }

    class LineItem
    {
        [PrimaryKey]
        public int LineItemId { get; set; }
        public int OrderId { get; set; }
        public override string ToString() => $"LineItemId = {LineItemId}, OrderId = {OrderId}";
    }
}
