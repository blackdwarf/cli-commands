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
            // Console.WriteLine(url);
            var json = client.GetStringAsync(url).Result;
            Console.WriteLine(json);
            // var searchResults = ParseResults(json);
            // Console.WriteLine(searchResults.Count);
            // var jsonr = JArray.Parse(result);
            // Console.WriteLine(jsonr[0]);
            var searchResults = JsonConvert.DeserializeObject<List<SearchResult>>(json);
            // Console.WriteLine(searchResults.ToString());
            if (searchResults.Count > 0)
            {
                writer.WriteSuccess($"{searchResults.Count} results found:");
                foreach (var item in searchResults) 
                {
                    Console.WriteLine($"\t{item.PackageDetails.Name}");
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
