using System;
using System.IO;
using System.Reflection;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Values;

namespace ConsoleApplication1 {
    
    public class Program {

        static void Main(string[] args) {

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "", "Example.json");

            // Deserialize the grid data
            GridDataModel gridData = GridDataModel.Deserialize(File.ReadAllText(path));

            Console.WriteLine(path);
            Console.WriteLine();


            if (gridData == null)
            {
                Console.WriteLine("WTF?");
                return;
            }




            foreach (var control in gridData.GetAllControls()) {

                Console.WriteLine(control.Editor.Alias.PadRight(20) + " => " + (control.Value == null ? "NULL" : control.Value.GetType().FullName));

                if (control.Editor.Alias == "rte") {

                    Console.WriteLine();
                    Console.WriteLine("RTE as String:".PadRight(20) + " => " + control.GetValue<GridControlRichTextValue>().Value);
                    Console.WriteLine("RTE as HtmlString:".PadRight(20) + " => " + control.GetValue<GridControlRichTextValue>().HtmlValue);
                    Console.WriteLine();

                } else if (control.Editor.Alias == "media") {

                    var media = control.GetValue<GridControlMediaValue>();

                    Console.WriteLine();
                    Console.WriteLine("Media:".PadRight(20) + " => " + media.Image + " (" + media.Id + ")");
                    Console.WriteLine();

                }

                //Console.WriteLine( JsonConvert.SerializeObject(control, Formatting.Indented) );

            }

        }

    }


}