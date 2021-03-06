﻿using Mono.Options;
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
                string JSONInput = null;
                string JSONFilePath = null;
                string OutputFilePath = null;
                string SchemaFilePath = null;
                bool shouldShowHelp = false;
                var parseOptions = new JSDoc.JSONParseOptions();
                options = new OptionSet {
                    { "j|json=", "Inline JSON input", f => {
                        JSONInput =f;
                    } },
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
                    { "d|dfa", "Detect fake arrays. Parse 'array-like' objects as arrays, eg. \"{1: 'a', 2: 'b', 3: 'c'}\"", dfa => {
                        parseOptions.DetectFakeArrays = dfa != null;
                    } },
                    { "l|array-limit=", "Number of array elements that will be analysed. Use this option to speed up the analysis on data that has long arrays", al => {
                        if (int.TryParse(al, out int res))
                            parseOptions.MaxArrayAnalysis = res;
                        else throw new OptionException($"Must specify a number","--array-limit");
                    } },
                    { "m|multi-limit=", "Maximum number of types that can be in a multi-type property", al => {
                        if (int.TryParse(al, out int res))
                            parseOptions.MaxMultiType = res;
                        else throw new OptionException($"Must specify a number","--multi-limit");
                    } },
                    { "h|help", "Show this message and exit", h => shouldShowHelp = h != null },
                };
                List<string> extra;

                extra = options.Parse(args);
                if (shouldShowHelp) {
                    Help();
                    return;
                }
                if (JSONFilePath == null && JSONInput == null) throw new OptionException("Must specify an input using either the --json or --file flag", "--json");
                if (JSONFilePath != null && JSONInput != null) throw new OptionException("You cannot specify an inline input and input file at the same time", "--json");
                if (OutputFilePath != null && SchemaFilePath != null) throw new OptionException("You cannot specify an output and schema file at the same time", "--output");
                bool outputInline = OutputFilePath == null && SchemaFilePath == null;

                JToken jt;
                using (TextReader textReader = JSONFilePath != null ? File.OpenText(JSONFilePath) : (TextReader)new StringReader(JSONInput))
                {
                    using (JsonTextReader jsr = new JsonTextReader(textReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        try
                        {
                            jt = (JToken)serializer.Deserialize(jsr);
                        }
                        catch (JsonException e)
                        {
                            Error("Invalid JSON in input file");
                            return;
                        }
                    }
                }

                JSDoc.JSDScope scope = JSDoc.JSDScope.GenerateFrom(jt, parseOptions);
                if (outputInline)
                {
                    Console.WriteLine(scope.ToString().Replace("\n", "\r\n"));
                }
                else 
                {
                    File.WriteAllText(OutputFilePath, scope.ToString().Replace("\n", "\r\n"));
                    Console.WriteLine($"Successfully wrote JSDoc to \"{OutputFilePath}\"");
                }
                
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
            Console.WriteLine($"Usage: {EXENAME} (-i <Inline JSON>|-f <Input File>) [-o <Output File>|-s <Schema File>] [OTHER OPTIONS]");
            Console.WriteLine("Generates JSDoc typedefs based on a JSON object");
            Console.WriteLine("Nested objects are supported, in this case multiple typedefs will be produced");
            Console.WriteLine();

            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
#if DEBUG
            Console.ReadLine();
#endif
        }

        private static void Error(string message) {
            Console.Write($"{EXENAME}: ");
            Console.WriteLine(message);
            Console.WriteLine($"Try '{EXENAME} --help' for usage information.");
#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
