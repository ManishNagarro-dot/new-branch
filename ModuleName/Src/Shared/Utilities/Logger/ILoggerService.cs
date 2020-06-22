using System;
using System.Collections.Generic;

namespace GOI.Seeker.Master.Shared.Utilities
{
    public interface ILoggerService
    {
        void Log(string message);
        void Log(string eventName, Dictionary<string, string> props, Dictionary<string, double> measurements = null);
        void Log(string metricName, double value);
        void Log(Exception e);
    }
}
