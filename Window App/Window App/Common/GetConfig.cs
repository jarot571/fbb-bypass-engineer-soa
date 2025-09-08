using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window_App.Common
{
    public class GetConfig
    {
        public string GetConfigText(string keyToFind)
        {
            var apiUrls = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("D:\\Config\\config.txt");

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    var parts = line.Split('=');
                    var key = parts[0].Trim();
                    var value = parts[1].Trim().Trim('\''); // remove quotes

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        apiUrls[key] = value;
                    }
                }
            }
            return apiUrls.ContainsKey(keyToFind) ? apiUrls[keyToFind] : null;
        }
    }
}
