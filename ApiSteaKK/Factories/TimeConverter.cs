using ApiSteaKK.Models;

namespace ApiSteaKK.Factories
{
    public class TimeConverter
    {
        private static readonly Dictionary<PlayTimeFormats, Func<int, string>> _converters = new Dictionary<PlayTimeFormats, Func<int, string>>();

        static TimeConverter()
        {
            _converters.Add(PlayTimeFormats.Minutes, GetMinutes);
            _converters.Add(PlayTimeFormats.Hours, GetHours);
            _converters.Add(PlayTimeFormats.Days, GetDays);
            _converters.Add(PlayTimeFormats.HoursMinutes, GetHoursAndMinutes);
            _converters.Add(PlayTimeFormats.DaysHours, GetDaysAndHours);
            _converters.Add(PlayTimeFormats.DaysMinutes, GetDaysAndMinutes);
            _converters.Add(PlayTimeFormats.DaysHoursMinutes, GetDaysAndHoursAndMinutes);
        }

        private static string GetMinutes(int minutes)
        {
            return $"{minutes}m";
        }

        private static string GetHours(int minutes)
        {
            return string.Format("{0:0.000}h", TimeSpan.FromMinutes(minutes).TotalHours);
        }

        private static string GetDays(int minutes)
        {
            return string.Format("{0:0.000}d", TimeSpan.FromMinutes(minutes).TotalDays);
        }

        private static string GetHoursAndMinutes(int minutes)
        {
            var hours = minutes / 60;

            return $"{hours}h{minutes - hours * 60}m";
        }

        private static string GetDaysAndHours(int minutes)
        {
            var allHours = minutes / 60;
            var days = allHours / 24;
            var hours = allHours - days * 24;

            return $"{days}d{hours}h";
        }

        private static string GetDaysAndMinutes(int minutes)
        {
            var days = minutes / 60 / 24;
            minutes = minutes - days * 24 * 60;

            return $"{days}d{minutes}m";
        }

        private static string GetDaysAndHoursAndMinutes(int minutes)
        {
            var allHours = minutes / 60;
            var days = allHours / 24;
            var allDayHours = days * 24;
            var hours = allHours - allDayHours;
            minutes = minutes - allDayHours * 60 - hours * 60;

            return $"{days}d{hours}h{minutes}m";
        }

        public static string GetTime(PlayTimeFormats timeFormat, string? time)
        {
            if (string.IsNullOrEmpty(time))
            {
                throw new ArgumentNullException("Time string is NULL or EMPTY!");
            }

            if (!int.TryParse(time, out var minutes))
            {
                throw new ArgumentNullException("Can't parse time!");
            }

            return _converters.ContainsKey(timeFormat) ?
                _converters[timeFormat](minutes) :
                throw new NotImplementedException($"Unsupported type of \"{nameof(PlayTimeFormats)}\"!");
        }
    }
}
