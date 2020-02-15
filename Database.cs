using LiteDB;
using Microsoft.Extensions.Logging;

namespace tobyheighwaydotcom
{
    public class Visitors
    {
        public string Id { get; set; }
        public long Count { get; set; }
    }

    public class Database
    {
        private readonly ILogger<Database> _logger;

        public Database(ILogger<Database> logger)
        {
            _logger = logger;
        }

        public long IncrementVisitorCount()
        {
            const string singletonId = "Visitors.Singleton";
            using var db = new LiteDatabase($"/var/lib/{nameof(tobyheighwaydotcom)}/database");
            var collection = db.GetCollection<Visitors>(nameof(Visitors));
            var visitors = collection.FindById(singletonId);

            if (visitors is null)
            {
                _logger.LogWarning("Initialising new visitor item");
                visitors = new Visitors { Id = singletonId, Count = 0 };
                collection.Insert(visitors);
            }

            visitors.Count++;
            collection.Update(visitors);
            return visitors.Count;
        }
    }
}