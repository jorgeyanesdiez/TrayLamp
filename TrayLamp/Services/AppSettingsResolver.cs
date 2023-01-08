using System;
using System.IO;
using System.Text.Json;
using TrayLamp.Abstractions;
using TrayLamp.Models;

namespace TrayLamp.Services
{
    public class AppSettingsResolver : IResolver<string, AppSettings>
    {
        public AppSettings Resolve(string input)
        {
            AppSettings result = new();
            try
            {
                string content = File.ReadAllText(input);
                AppSettings? candidate = JsonSerializer.Deserialize<AppSettings>(content);
                if (candidate != null) { result = candidate; }
            }
            catch (Exception ex) when (ex is IOException)
            {
                // Ignore
            }
            return result;
        }
    }
}
