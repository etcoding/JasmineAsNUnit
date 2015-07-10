using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GooglePageTest
{
    [TestFixture]
    public class GoogleTests
    {
        [Test, TestCaseSource("DataProvider")]
        public string JsTests(string status)
        {
            Assert.AreEqual("passed", status);
            return status;
        }


        public IEnumerable<TestCaseData> DataProvider()
        {
            using (var driver = new OpenQA.Selenium.PhantomJS.PhantomJSDriver())
            {
                driver.Navigate().GoToUrl("https://www.google.com/search?q=test");

              //  this.BlockUntilElementIsAvailable(driver, "input");

                //Thread.Sleep(500); // needed for PhantomJS. Chrome works without.

                // load JS onto a page
                driver.ExecuteScript(File.ReadAllText("scripts/jasmine.js"));
                driver.ExecuteScript(File.ReadAllText("scripts/jquery-2.1.4.js"));
                driver.ExecuteScript(File.ReadAllText("scripts/boot.js"));  // this contains both reporter and bootstrapper
                driver.ExecuteScript(File.ReadAllText("scripts/googleTests.js"));
                driver.ExecuteScript(File.ReadAllText("scripts/execute.js"));

                // wait until all tests are done
                while ((driver.ExecuteScript("return ReportCollector.Status;") as string) != "Finished")
                {
                    Thread.Sleep(100);
                }

                // now get the results
                var specResultsJson = (string)driver.ExecuteScript("return JSON.stringify(ReportCollector.SpecResults);");

                var results = JsonConvert.DeserializeObject<JasmineResults[]>(specResultsJson);

                // and create test case data
                foreach (var result in results)
                {
                    string msg = null;
                    if (result.status == "passed")
                    {
                        msg = result.status;
                    }
                    else
                    {
                        foreach (var item in result.failedExpectations)
                        {
                            msg += "Matcher: " + item.matcherName + "; Message: " + item.message + Environment.NewLine;
                        }
                    }


                    var testCase = new TestCaseData(msg)
                    .SetDescription(result.description)
                    .SetName(result.fullName)
                    .Returns("passed");

                    yield return testCase;
                }
            }
        }

        private void BlockUntilElementIsAvailable(RemoteWebDriver driver, string elementSelector)
        {
            while (true)
            {
                var len = driver.ExecuteScript("return $(\"" + elementSelector + "\").length");
                if (Convert.ToInt32(len) > 0)
                    break;
                Thread.Sleep(100);
            }
        }

    }
}
