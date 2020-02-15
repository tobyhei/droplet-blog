using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tobyheighwaydotcom
{
    public class HomePage
    {
        private static readonly string ProcessStartTime = Time.GetCurrentSydneyTime().ToString();
        private readonly Database _database;

        public HomePage(Database database)
        {
            _database = database;
        }

        public async Task Get(HttpContext context)
        {
            var now = Time.GetCurrentSydneyTime();
            var visits = _database.IncrementVisitorCount();
            
            await context.Response.WriteAsync(
                "Welcome to Tobias Heighway's Home Page." +
                $"\n\nProcess start time: {ProcessStartTime}" +
                $"\n\nCurrent time: {now}" +
                $"\n\nVisitor number {visits}");
        }
    }
}