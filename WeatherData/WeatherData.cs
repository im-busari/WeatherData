using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Weather
{
    public class WeatherData
    {
        public static IEnumerable<Observation> ReadRange(
            TextReader text,
            DateTime? start = null,
            DateTime? end = null,
            Action<string> errorHandler = null)
        {
            return ReadAll(text, errorHandler)
                    .SkipWhile((item) => item.TimeStamp < (start ?? DateTime.MinValue))
                    .TakeWhile((item) => item.TimeStamp <= (end ?? DateTime.MaxValue));
        }


        public static IEnumerable<Observation> ReadAll(TextReader text, Action<string> errorHandler = null)
        {
            string line = null;
            while ((line = text.ReadLine()) != null)
            {
                if (Observation.TryParse(line, out Observation wo))
                {
                    yield return wo;
                }
                else
                {
                    try
                    {
                        errorHandler?.Invoke(line);
                    }
                    catch { }
                }
            }
        }
    }
    }
}
