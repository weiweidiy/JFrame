using NUnit.Framework;
using JFrame.Configuration;
using System.Text;
using System;

namespace JFrameTest
{
    public class TestConfigurationManager
    {

        //        string AppConfigContent = "< App > " + "\n" +

        //    < Config > 123 </ Config >
        //</ App >"
        string AppConfigContent = "";
        [SetUp]
        public void Setup()
        {
            var sb = new StringBuilder();
            sb.Append("<App>"); sb.Append("\n");
            sb.Append("<Config>"); sb.Append(123); sb.Append("</Config>"); sb.Append("\n");
            sb.Append("<ConfigArr>"); sb.Append("\n");
            sb.Append("<Config>"); sb.Append("a1"); sb.Append("</Config>");sb.Append("\n");
            sb.Append("<Config>"); sb.Append("a2"); sb.Append("</Config>"); sb.Append("\n");
            sb.Append("</ConfigArr>"); sb.Append("\n");
            sb.Append("</App>");
            AppConfigContent = sb.ToString();

            Console.WriteLine(AppConfigContent);
        }


        /// <summary>
        /// ≤‚ ‘◊¢≤·≈‰÷√Œƒº˛
        /// </summary>
        [Test]
        public void TestRegisterConfig()
        {
            //Arrange
            var manager = new ConfigurationManager();

            //Act
            manager.RegistConfiguration("App", "D:/App.txt");

            //Assert
            Assert.AreEqual(1, manager.GetRegistCount());
        }

        /// <summary>
        /// ≤‚ ‘¥”◊÷∑˚¥Æº”‘ÿ≈‰÷√
        /// </summary>
        [Test]
        public void TestLoadConfigFromString()
        {
            //Arrange
            var manager = new ConfigurationManager();

            //Act
            manager.Load("App", AppConfigContent,"");

            //Assert
            //Assert.AreEqual("123", manager["App"]["Config"].GetValue());
            Assert.AreEqual("456", manager["App"]["ConfigArr"]["0"].ToString());
        }
    }
}