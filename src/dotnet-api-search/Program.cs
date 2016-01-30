using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using DotnetCli.Extensions.Utils;

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
            Console.WriteLine(json);
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
            // throw new NotImplementedException();
            Console.WriteLine("Use the Force, Luke!");
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
        
        
        // private static List<SearchResult> ParseResults(string json)
        // {
        //     List<SearchResult> results = new List<SearchResult>();
        //     
        //     var jarray = JArray.Parse(json);
        //     if (jarray.Count == 0)
        //     {
        //         return results;
        //     }
        //     foreach (var item in jarray)
        //     {
        //         results.Add(new SearchResult {
        //             PackageDetails = new PackageDetails 
        //             {
        //                 Name = item["PackageDetails"]["Name"].ToString(),
        //                 Version = item["PackageDetails"]["Version"].ToString()
        //             },
        //             FullTypeName = item["FullTypeName"].ToString(),
        //             Tfms = item["Tfms"].ToObject<string[]>()
        //         });
        //     }
        //     
        //     return results;
        // }
        // 
    }
}
