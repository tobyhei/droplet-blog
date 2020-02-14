using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NodaTime;

namespace tobyheighwaydotcom
{
    public class HomePage
    {
        private string processStartTime = Time.GetCurrentSydneyTime().ToString();
        private int _visits = 0;
        
        public async Task Get(HttpContext context)
        {
            var now = Time.GetCurrentSydneyTime();
            _visits++;
            
            await context.Response.WriteAsync(
                "Welcome to Tobias Heighway's Home Page." +
                $"\n\nProcess start time: {processStartTime}" +
                $"\n\nCurrent time: {now}" +
                $"\n\nVisitor number {_visits}");
        }
    }
}