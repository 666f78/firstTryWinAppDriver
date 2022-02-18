using Microsoft.Extensions.Configuration;

namespace task1.Config
{
    internal class ConfigSetUp
    {
        public IConfiguration Config;
        public ConfigSetUp()
        {
            SetUpConfig();
        }

        private void SetUpConfig()
        {
            Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
    }
}
