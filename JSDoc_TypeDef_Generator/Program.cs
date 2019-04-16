using Mono.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JSDoc_TypeDef_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
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
                Console.Error.WriteLine("Usage: gentypedef <JSON Object>");
                return;
#endif
            }
            string output = "/**\n * [Enter Type Description Here]\n * @typedef {Object} [Enter Type Name Here]\n";
            object JSON = JsonConvert.DeserializeObject(JSONString);
            ProcessObject(JSON);
            Console.ReadLine();
        }
        static string ProcessArray(object array) {
        }
        static string ProcessObject(object obj) {
            if (obj.GetType().IsArray)
                return ProcessArray(obj);
        }
        static string AddProperty(string ExistingTypeDef, string Name, string Type) {

        }
    }
}
