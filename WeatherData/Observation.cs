using System;
using System.Diagnostics;

namespace Weather
{
    public struct Observation
    {
        public DateTime TimeStamp;
        public float Barometric_Pressure;

        public static Observation Parse(string text)
        {
            var data = text.Split('\t');

            Debug.Assert(data.Length == 8);

            var timestamp = DateTime.Parse(data[(int)ObservationMetrics.Date_Time].Replace("_", "-"));
            var pressure = float.Parse(data[(int)ObservationMetrics.Barometric_Pressure]);

            return new Observation()
            {
                TimeStamp = timestamp,
                Barometric_Pressure = pressure
            };
        }

        public static bool TryParse(string text, out Observation WeatherObservation)
        {
            WeatherObservation = new Observation()
            {
                TimeStamp = DateTime.MinValue,
                Barometric_Pressure = float.NaN
            };

            var data = text.Split('\t');

            if (data.Length != 8) return false;
            if (!DateTime.TryParse(data[(int)ObservationMetrics.Date_Time].Replace("_", "-"), out DateTime timestamp)) return false;
            if (!float.TryParse(data[(int)ObservationMetrics.Barometric_Pressure], out float pressure)) return false;

            WeatherObservation = new Observation()
            {
                TimeStamp = timestamp,
                Barometric_Pressure = pressure
            };

            return true;
        }
    }
}
