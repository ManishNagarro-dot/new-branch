using System;

namespace GOI.Seeker.Master.Shared.Utilities
{
    public class SettingService : ISettingService
    {
        private const string InsightsInstrumentationKey = "InsightsInstrumentationKey";
        // App Insights
        public string GetInsightsInstrumentationKey()
        {
            return GetEnvironmentVariable(InsightsInstrumentationKey);
        }

        //*** PRIVATE ***//
        private static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
