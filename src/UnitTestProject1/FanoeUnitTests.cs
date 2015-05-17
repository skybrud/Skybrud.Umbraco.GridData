using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Umbraco.GridData.Fanoe;
using Skybrud.Umbraco.GridData.Values;

namespace UnitTestProject1 {
    
    [TestClass]
    public class FanoeUnitTests {
        
        [ClassInitialize]
        public static void Startup(TestContext ctx) {

            GridContext.Current.Converters.Add(new FanoeGridConverter());

        }
    
        [TestMethod]
        public void TestSample1() {

            // Load the JSON
            string json = File.ReadAllText(PathResolver.MapPath("~/Samples/Sample1.json"));

            // Get the Grid model
            GridDataModel model = GridDataModel.Deserialize(json);

            Assert.IsNotNull(model);

            Assert.AreEqual(1, model.Sections.Length);
            Assert.AreEqual(6, model.Sections[0].Rows.Length);

            GridControl[] controls = model.GetAllControls();

            Assert.AreEqual(13, controls.Length);

            Hest(controls[0], "headline", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[1], "quote", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[2], "banner_headline", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[3], "banner_tagline", typeof(GridControlTextValue), typeof(GridEditorTextConfig));
            Hest(controls[4], "rte", typeof(GridControlRichTextValue));
            Hest(controls[5], "rte", typeof(GridControlRichTextValue));
            Hest(controls[6], "rte", typeof(GridControlRichTextValue));
            Hest(controls[7], "rte", typeof(GridControlRichTextValue));
            Hest(controls[8], "rte", typeof(GridControlRichTextValue));
            Hest(controls[9], "media_wide", typeof(GridControlMediaValue), typeof(GridEditorMediaConfig));
            Hest(controls[10], "rte", typeof(GridControlRichTextValue));
            Hest(controls[11], "rte", typeof(GridControlRichTextValue));
            Hest(controls[12], "rte", typeof(GridControlRichTextValue));

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