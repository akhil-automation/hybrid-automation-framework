using Microsoft.Extensions.Configuration;
namespace HybridFramework.Tests.Config{
    public static class ConfigManager{
        private static IConfiguration _config;
        static ConfigManager(){
            _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
        public static string UIBaseUrl => _config["UI:BaseUrl"]!;
        public static string Browser => _config["UI:Browser"]!;
        public static bool Headless => bool.Parse(_config["UI:Headless"]!);
        public static int SlowMo => int.Parse(_config["UI:SlowMo"]!);

        public static string APIBaseUrl => _config["API:BaseUrl"]!;
        public static string AdminUsername => _config["Credentials:AdminUsername"]!;
        public static string AdminPassword => _config["Credentials:AdminPassword"]!;
        public static string APIUsername => _config["API:Username"]!;
        public static string APIPassword => _config["API:Password"]!;


    }
}