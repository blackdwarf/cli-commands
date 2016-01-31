using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Dnx.Runtime.Common.CommandLine;

namespace DotnetApiSearch
{
    public class Program
    {
        public static int Main(string[] args)
        {
          
            var app = new CommandLineApplication();
            app.Name = "dotnet api-search";
            app.FullName = ".NET API CLI search tool";
            app.Description = "Reverse package search using the API as a keyword";
            app.HelpOption("-h|--help");
            
            var url = app.Option("-u|--url", "Overrides for the default URL for the Package Search API", CommandOptionType.SingleValue);
            var query = app.Argument("<QUERY>", "The API to search for in the shape of a single keyword");
            
            
            // var writer = new ConsoleWriter();
            // 
            // if (args.Length == 0)
            // {
            //     ShowHelp();
            //     return 0;
            // }
            
            // We are assuming just a single string on invocation (naive, I know)
            app.OnExecute(() => 
            {
                var apisearch = new ApiSearchCommand();
                apisearch.ApiUrl = url.Value() ?? "http://packagesearch.azurewebsites.net/Search/";
                apisearch.Query = query.Value;
                
                if (String.IsNullOrEmpty(apisearch.Query))
                {
                    Reporter.Output.WriteLine("<QUERY> argument is required. Use -h|--help to see help");
                    return 0;
                }
                
                return apisearch.SearchApi();
                
            });
            
            try
            {
                return app.Execute(args);
            } 
            catch (Exception ex)
            {
                Reporter.Error.WriteLine(ex.Message.Red());
                return 1;
            } 
        }

        // private static void ShowHelp()
        // {
        //     Console.WriteLine("Usage: dotnet api-search <query>");
        //     Console.WriteLine();
        //     Console.WriteLine("Options:");
        //     Console.WriteLine("\t<query>:\tMandatory, the api to search for");
        // }
        
    }
}
