using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Converters.Fanoe;
using Skybrud.Umbraco.GridData.Values;

namespace UnitTestProject1 {
    
    [TestClass]
    public class UnitTest1 {
    
        [TestMethod]
        public void TestSample1() {

            // Remove the converter for the Fanoe starter kit
            GridContext.Current.Converters.Remove<FanoeGridConverter>();

            Assert.AreEqual(1, GridContext.Current.Converters.Count);

            // Load the JSON
            string json = File.ReadAllText(PathResolver.MapPath("~/Samples/Sample1.json"));

            // Get the Grid model
            GridDataModel model = GridDataModel.Deserialize(json);

            Assert.IsNotNull(model);

            Assert.AreEqual(1, model.Sections.Length);
            Assert.AreEqual(7, model.Sections[0].Rows.Length);

            GridControl[] controls = model.GetAllControls();

            Assert.AreEqual(14, controls.Length);

            Hest(controls[0], "headline", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[1], "quote", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[2], "banner_headline");
            Hest(controls[3], "banner_tagline");
            Hest(controls[4], "rte", typeof(GridControlRichTextValue));
            Hest(controls[5], "rte", typeof(GridControlRichTextValue));
            Hest(controls[6], "rte", typeof(GridControlRichTextValue));
            Hest(controls[7], "rte", typeof(GridControlRichTextValue));
            Hest(controls[8], "rte", typeof(GridControlRichTextValue));
            Hest(controls[9], "media_wide");
            Hest(controls[10], "rte", typeof(GridControlRichTextValue));
            Hest(controls[11], "rte", typeof(GridControlRichTextValue));
            Hest(controls[12], "rte", typeof(GridControlRichTextValue));
            Hest(controls[13], "media", typeof(GridControlMediaValue), typeof(GridEditorMediaConfig));

            {
                Assert.AreEqual(JsonConvert.SerializeObject(model, Formatting.None), model.JObject.ToString(Formatting.None));
                Assert.AreEqual(JsonConvert.SerializeObject(model.Sections[0], Formatting.None), model.Sections[0].JObject.ToString(Formatting.None));

                GridRow row = model.Sections[0].Rows[6];
                Assert.AreEqual("Hest", row.Name);
                Assert.AreEqual(2, row.Areas.Length);
                Assert.AreEqual(true, row.HasAreas);
                Assert.AreEqual(0, row.Styles.Count);
                Assert.AreEqual(1, row.Config.Count);
                Assert.AreEqual("dark", row.Config["class"]);
                Assert.AreEqual(JsonConvert.SerializeObject(row, Formatting.None), row.JObject.ToString(Formatting.None));

                GridArea area1 = model.Sections[0].Rows[6].Areas[0];
                Assert.AreEqual(4, area1.Grid);
                Assert.AreEqual(1, area1.Controls.Length);
                Assert.AreEqual("Hest", area1.Row.Name);
                Assert.AreEqual(4, area1.Grid);
                Assert.AreEqual(1, area1.Config.Count);
                Assert.AreEqual(1, area1.Styles.Count);
                Assert.AreEqual("yellow", area1.Config["class"]);
                Assert.AreEqual("150px", area1.Styles["margin-bottom"]);
                Assert.AreEqual(JsonConvert.SerializeObject(area1, Formatting.None), area1.JObject.ToString(Formatting.None));

                GridArea area2 = model.Sections[0].Rows[6].Areas[1];
                Assert.AreEqual(8, area2.Grid);
                Assert.AreEqual(0, area2.Controls.Length);
                Assert.AreEqual(JsonConvert.SerializeObject(area2, Formatting.None), area2.JObject.ToString(Formatting.None));
            }

        }

        private void Hest(GridControl control, string alias, Type value = null, Type config = null) {

            Assert.AreEqual(alias, control.Editor.Alias);

            if (value == null) {
                Assert.IsNull(control.Value);
            } else {
                Assert.IsNotNull(control.Value);
                Assert.AreEqual(control.Value.GetType(), value);
            }

            if (config == null) {
                Assert.IsNull(control.Editor.Config);
            } else {
                Assert.IsNotNull(control.Editor.Config);
                Assert.AreEqual(control.Editor.Config.GetType(), config);
            }

        }
    
    }

}