using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Values;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1 {
    
    public class Program {

        static void Main(string[] args) {

            // Registering "custom" control types
            PiggyBank.OneLittlePiggy["macro"] = token => new GridControlMacroValue();
            PiggyBank.OneLittlePiggy["rte"] = token => new GridControlRichTextValue {
                Value = token.Value<string>()
            };

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "", "Example.json");

            // Deserialize the grid data
            GridData gridData = GridData.Deserialize(File.ReadAllText(path));

            foreach (var control in gridData.GetAllControls()) {

                Console.WriteLine(control.Editor.Alias.PadRight(16) + " => " + (control.Value == null ? "NULL" : control.Value.GetType().FullName));

                if (control.Editor.Alias == "rte") {

                    Console.WriteLine();
                    Console.WriteLine("Value of RTE: " + control.GetValue<GridControlRichTextValue>().Value);
                    Console.WriteLine("Value of RTE: " + control.GetValue<GridControlRichTextValue>().HtmlValue);

                }

                //Console.WriteLine( JsonConvert.SerializeObject(control, Formatting.Indented) );

            }

        }

    }


}