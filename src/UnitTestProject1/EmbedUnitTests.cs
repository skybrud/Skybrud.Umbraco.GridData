using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Values;

// ReSharper disable IsExpressionAlwaysTrue
// ReSharper disable SuspiciousTypeConversion.Global

namespace UnitTestProject1 {

    [TestClass]
    public class EmbedUnitTests {

        [TestMethod]
        public void Legacy() {

            // Load the JSON
            string json = File.ReadAllText(PathResolver.MapPath("~/Samples/EmbedLegacy.json"));

            // Parse the grid control
            JObject control = JObject.Parse(json);

            // Get the JToken for the value
            JToken token = control.GetValue("value");

            GridControlEmbedValue embed = GridControlEmbedValue.Parse(null, token);

            Assert.IsTrue(embed.IsValid);
            Assert.IsTrue(embed is IHtmlString);
            Assert.AreEqual(Environment.NewLine, embed.GetSearchableText());

            Assert.AreEqual(0, embed.Width);
            Assert.AreEqual(0, embed.Height);
            Assert.AreEqual(null, embed.Url);
            Assert.AreEqual(null, embed.Info);
            Assert.AreEqual("<iframe width=\"360\" height=\"203\" src=\"https://www.youtube.com/embed/VTnDYxwhSaI?feature=oembed\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>", embed.Preview + "");

            Assert.AreEqual("<iframe width=\"360\" height=\"203\" src=\"https://www.youtube.com/embed/VTnDYxwhSaI?feature=oembed\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>", embed.HtmlValue + "");

        }

        [TestMethod]
        public void Embed() {

            // Load the JSON
            string json = File.ReadAllText(PathResolver.MapPath("~/Samples/Embed.json"));

            // Parse the grid control
            JObject control = JObject.Parse(json);

            // Get the JToken for the value
            JToken token = control.GetValue("value");

            GridControlEmbedValue embed = GridControlEmbedValue.Parse(null, token);

            Assert.IsTrue(embed.IsValid);
            Assert.IsTrue(embed is IHtmlString);
            Assert.AreEqual(Environment.NewLine, embed.GetSearchableText());

            //Assert.AreEqual(true, embed.Constrain);
            Assert.AreEqual(360, embed.Width);
            Assert.AreEqual(240, embed.Height);
            Assert.AreEqual("https://www.youtube.com/watch?v=VTnDYxwhSaI", embed.Url);
            Assert.AreEqual("", embed.Info);
            Assert.AreEqual("<iframe width=\"360\" height=\"203\" src=\"https://www.youtube.com/embed/VTnDYxwhSaI?feature=oembed\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>", embed.Preview + "");

            Assert.AreEqual("<iframe width=\"360\" height=\"203\" src=\"https://www.youtube.com/embed/VTnDYxwhSaI?feature=oembed\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>", embed.HtmlValue + "");

        }

    }

}