using Mono.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSDoc_TypeDef_Generator
{
    class Program
    {
        private const string EXENAME = "gentypedef";
        static void Main(string[] args)
        {
            string JSONFilePath = null;
            string OutputFilePath = null;
            string SchemaFilePath = null;
            bool shouldShowHelp = false;
            var options = new OptionSet {
                { "f|file=", "Path to a file containing a JSON object", f => {
                    if (!File.Exists(f)) throw new OptionException($"File \"{f}\" not found or inaccessible","--file");
                    JSONFilePath =f;
                } },
                { "o|output=", "Path to a file where output JSDoc will be written. Cannot be used with the --schema option", f => {
                    if (!File.Exists(f)) throw new OptionException($"File \"{f}\" not found or inaccessible","--output");
                    OutputFilePath = f;
                } },
                { "s|schema=", "Path to a file where esiting JSDoc schema will be read from, new types will be appended to this file. Cannot be used with the --output option.", f => {
                    if (!File.Exists(f)) throw new OptionException($"File \"{f}\" not found or inaccessible","--schema");
                    SchemaFilePath = f;
                } },
                { "h|help", "Show this message and exit", h => shouldShowHelp = h != null },
            };
            List<string> extra;
            try {
                extra = options.Parse(args);
                if (!shouldShowHelp) {
                    if (JSONFilePath == null) throw new OptionException("JSON File must be specified", "--file");
                    if (OutputFilePath == null && SchemaFilePath == null) throw new OptionException("Either an output or schema file must be specified", "--output");
                    if (OutputFilePath != null && SchemaFilePath != null) throw new OptionException("You cannot specify an output and schema file at the same time", "--output");
                }
            } catch (OptionException e) {
                Console.Write($"{EXENAME}: ");
                Console.WriteLine(e.Message);
                Console.WriteLine($"Try '{EXENAME} --help' for more information.");
#if DEBUG
                Console.ReadLine();
#endif
                return;
            }
            if (shouldShowHelp) {
                Console.WriteLine($"Usage: {EXENAME} -f <Input File> (-o <Output File>|-s <Schema File>)");
                Console.WriteLine("Generates JSDoc typedefs based on a JSON object");
                Console.WriteLine("Nested objects are supported, in this case multiple typedefs will be produced");
                Console.WriteLine();

                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
#if DEBUG
                Console.ReadLine();
#endif
                return;
            }


            string JSONString;
            if (args.Length == 1)
            {
                JSONString = args[0];
            }
            else {
#if DEBUG
                Console.WriteLine("Enter JSON Object (Or ENTER to use example):");
                JSONString = Console.ReadLine();
                if (JSONString == "") JSONString = "{\"Code\":\"Mon1\",\"FileID\":26,\"ID\":56,\"Name\":\"Monday 1\",\"Number\":1,\"Periods\":[{\"Code\":\"TG\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":false,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":386,\"Index\":0,\"Load\":0.2,\"Name\":\"Tutor AM\",\"Number\":1,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"1\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":false,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":387,\"Index\":0,\"Load\":1,\"Name\":\"Period 1\",\"Number\":2,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"2\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":true,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":388,\"Index\":0,\"Load\":1,\"Name\":\"Period 2\",\"Number\":3,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"3\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":true,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":389,\"Index\":0,\"Load\":1,\"Name\":\"Period 3\",\"Number\":4,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":true,\"Day\":null},{\"Code\":\"4\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":false,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":390,\"Index\":0,\"Load\":1,\"Name\":\"Period 4\",\"Number\":5,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"5\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":true,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":391,\"Index\":0,\"Load\":1,\"Name\":\"Period 5\",\"Number\":6,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"6\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":false,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":392,\"Index\":0,\"Load\":1,\"Name\":\"Period 6\",\"Number\":7,\"Quadruples\":false,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null},{\"Code\":\"7\",\"DayID\":56,\"DayNumber\":1,\"Doubles\":true,\"EndTime\":\"00:00:00\",\"FileID\":26,\"ID\":393,\"Index\":0,\"Load\":1,\"Name\":\"Period 7\",\"Number\":8,\"Quadruples\":true,\"SiteMove\":false,\"StartTime\":\"00:00:00\",\"Triples\":false,\"Day\":null}]}";
#else
                Console.Error.WriteLine("This utility generates a JSDOC TypeDef comment based on a JSON object");
                Console.Error.WriteLine("Usage: gentypedef <JSON_File_Path>");
                return;
#endif
            }
            JSDoc.TypeDef[] tds;
            try {
                tds = JSDoc.JSDScope.ParseJSON(JSONString).TypeDefinitions;
                foreach (var td in tds) {
                    Console.WriteLine(Commentify(td.ToString()));
                }
            } catch (JsonReaderException) {
                Console.Error.WriteLine("Invalid JSON");
            }
        }
        static string Commentify(string s) {
            return "/**\n * "+s.Replace("\n", "\n * ") + "\n */";
        }
    }
}
