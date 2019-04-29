using Mono.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSDoc_TypeDef_Generator
{
    class Program
    {
        private const string EXENAME = "gentypedef";
        private static OptionSet options;

        static void Main(string[] args)
        {
            try {
                string JSONFilePath = null;
                string OutputFilePath = null;
                string SchemaFilePath = null;
                bool shouldShowHelp = false;
                options = new OptionSet {
                    { "f|file=", "Path to a file containing a JSON object", f => {
                        if (!File.Exists(f)) throw new OptionException($"File \"{f}\" not found or inaccessible","--file");
                        JSONFilePath =f;
                    } },
                    { "o|output=", "Path to a file where output JSDoc will be written. Cannot be used with the --schema option", f => {
                        OutputFilePath = f;
                    } },
                    { "s|schema=", "Path to a file where esiting JSDoc schema will be read from, new types will be appended to this file. Cannot be used with the --output option.", f => {
                        if (!File.Exists(f)) throw new OptionException($"File \"{f}\" not found or inaccessible","--schema");
                        SchemaFilePath = f;
                    } },
                    { "h|help", "Show this message and exit", h => shouldShowHelp = h != null },
                };
                List<string> extra;

                extra = options.Parse(args);
                if (shouldShowHelp) {
                    Help();
                    return;
                }
                if (JSONFilePath == null) throw new OptionException("JSON File must be specified", "--file");
                if (OutputFilePath == null && SchemaFilePath == null) throw new OptionException("Either an output or schema file must be specified", "--output");
                if (OutputFilePath != null && SchemaFilePath != null) throw new OptionException("You cannot specify an output and schema file at the same time", "--output");

                JToken jt;
                using (StreamReader file = File.OpenText(JSONFilePath)) {
                    using (JsonTextReader jsr = new JsonTextReader(file)) {
                        JsonSerializer serializer = new JsonSerializer();
                        try {
                            jt = (JToken)serializer.Deserialize(jsr);
                        } catch (JsonException e) {
                            Error("Invalid JSON in input file");
                            return;
                        }
                    }
                }
                JSDoc.JSDScope scope = JSDoc.JSDScope.ParseJSONToken(jt, new JSDoc.JSONParseOptions() { DetectFakeArrays = true, MaxArrayAnalysis = 3 });
                File.WriteAllText(OutputFilePath, scope.ToString().Replace("\n", "\r\n"));
                Console.WriteLine($"Successfully wrote JSDoc to \"{OutputFilePath}\"");
                Console.WriteLine($"Root type is \"{scope.TypeDefinitions.First().Name}\"");
            } catch (OptionException e) {
                Error($"Value for {e.OptionName} was invalid\n{e.Message}");
            } catch (Exception e) {
                Error(e.Message);
            }
#if DEBUG
            Console.ReadLine();
#endif
        }

        private static void Help() {
            Console.WriteLine($"Usage: {EXENAME} -f <Input File> (-o <Output File>|-s <Schema File>)");
            Console.WriteLine("Generates JSDoc typedefs based on a JSON object");
            Console.WriteLine("Nested objects are supported, in this case multiple typedefs will be produced");
            Console.WriteLine();

            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
            return;
        }

        private static void Error(string message) {
            Console.Write($"{EXENAME}: ");
            Console.WriteLine(message);
            Console.WriteLine($"Try '{EXENAME} --help' for usage information.");
        }
    }
}
