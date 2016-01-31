using System;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.DotNet.Cli.Utils;
using Newtonsoft.Json;

namespace DotnetApiSearch 
{
    public class ApiSearchCommand
    {
        public string Query { get; set; }
        public string ApiUrl { get; set; }
        
        public int SearchApi()
        {


                var client = new HttpClient();
                var url = String.Format("{0}?searchTerm={1}", ApiUrl, Query.ToLowerInvariant());
                try 
                {
                    var json = client.GetStringAsync(url).Result;
                    var searchResults = JsonConvert.DeserializeObject<List<SearchResult>>(json);
                    if (searchResults.Count > 0)
                    {
                        // writer.WriteSuccess($"{searchResults.Count} results found:");
                        Reporter.Output.WriteLine($"{searchResults.Count} results found:");
                        foreach (var item in searchResults) 
                        {
                            DisplayResult(item);
                        }
                    } else 
                    {
                        // writer.WriteWarning("No packages found with a given API");
                        Reporter.Output.WriteLine("No packages found with a given API");
                    }
                    return 0;
                    
                }
                catch (Exception ex)
                {
                    Reporter.Error.WriteLine(ex.Message.Red());
                    return 1;
                }
        }
        
        private static void DisplayResult(SearchResult item)
        {
            Reporter.Output.WriteLine();
            Reporter.Output.WriteLine($"\t{item.PackageDetails.Name} ({item.PackageDetails.Version})");
            Reporter.Output.WriteLine($"\t\tFound in type {item.FullTypeName}");
            if (item.Tfms.Length > 0)
            {
                Reporter.Output.WriteLine(String.Format("\t\tIn framework(s) {0}", String.Join(", ", item.Tfms)));
            }
            Reporter.Output.WriteLine();
        }

    }
}
