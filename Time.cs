using NodaTime;

namespace tobyheighwaydotcom
{
    public class Time
    {
        private static readonly ZonedClock Clock = new ZonedClock(
            SystemClock.Instance, DateTimeZoneProviders.Tzdb["Australia/Sydney"], CalendarSystem.Iso);

        public static ZonedDateTime GetCurrentSydneyTime() => Clock.GetCurrentZonedDateTime();
    }
}