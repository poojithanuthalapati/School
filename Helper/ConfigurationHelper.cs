using Microsoft.Extensions.Configuration;
using System.IO;


namespace School.Helper
{
    public class ConfigurationHelper
    {
        private static IConfigurationRoot _configuration;

        static ConfigurationHelper()
        {
            // Build the configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        // Method to read a specific section from appsettings.json
        public static T GetSection<T>(string sectionName) where T : class, new()
        {
            var section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }

        // Method to read a specific value by key
        public static string GetValue(string key)
        {
            return _configuration[key];
        }
    }
}
