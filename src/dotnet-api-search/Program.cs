using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotnetApiSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string apiUrl = "http://packagesearch.azurewebsites.net/Search/";
            var writer = new ConsoleWriter();
            
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            
            // We are assuming just a single string on invocation (naive, I know)
            var query = args[0];
            
            var client = new HttpClient();
            var url = String.Format("{0}?searchTerm={1}", apiUrl, query.ToLowerInvariant());
            var json = client.GetStringAsync(url).Result;
            var searchResults = JsonConvert.DeserializeObject<List<SearchResult>>(json);
            if (searchResults.Count > 0)
            {
                writer.WriteSuccess($"{searchResults.Count} results found:");
                foreach (var item in searchResults) 
                {
                    DisplayResult(item);
                }
            } else 
            {
                writer.WriteWarning("No packages found with a given API");
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: dotnet api-search <query>");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("\t<query>:\tMandatory, the api to search for");
        }
        
        private static void DisplayResult(SearchResult item)
        {
            Console.WriteLine();
            Console.WriteLine($"\t{item.PackageDetails.Name} ({item.PackageDetails.Version})");
            Console.WriteLine($"\t\tFound in type {item.FullTypeName}");
            if (item.Tfms.Length > 0)
            {
                Console.WriteLine("\t\tIn framework(s) {0}", String.Join(", ", item.Tfms));
            }
            Console.WriteLine();
        }
    }
}
