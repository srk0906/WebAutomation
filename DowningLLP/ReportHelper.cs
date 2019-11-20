using DowningLLP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DowningLLP.TestBase;

namespace DowningLLP
{
    class ReportHelper
    {


        private static string ReportResultPath()
        {

            string projdir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            String reportdir = Directory.GetParent(projdir).FullName;
            return reportdir + ConfigurationManager.AppSettings["ReportPath"];
        }
        public static void GetConsolidatedReport(List<TestBase.TestCaseResult> resultSet)
        {
            try
            {
                string resultPath = ReportResultPath();

                if (!Directory.Exists(resultPath))
                {
                    Directory.CreateDirectory(resultPath);
                }

                string consolidatedReport = Path.Combine(resultPath, string.Format("{0}{1}{2}{3}", "Consolidated", "_Report_", DateTime.Now.ToString("yyyy_MM_dd_HHmmss"), ".html"));
                string tempFileName = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + ConfigurationManager.AppSettings["ReportTemplatePath"];
                //StreamReader file = new StreamReader(@"C:\Users\aarthy.rajasekar\Exec\Template\template.html");
                StreamReader file = new StreamReader(tempFileName);
                string htmlContent = file.ReadToEnd();
                StringBuilder sb = new StringBuilder();



                string tableRow = "<tr><td>$testcategory$</td><td>$testscenario$</td><td>$status$</td></tr>";
                foreach (var item in resultSet)
                {
                    var logFile = Path.Combine(item.ResultDir, item.TestCaseName + "_Log.html");
                    var link = string.Format("<a href='{0}'> {1} </a>", logFile, item.TestCaseName);
                    sb.AppendLine(tableRow.Replace("$testcategory$", item.TestCaseCategory).Replace("$testscenario$", link).Replace("$status$", item.ExecutionResult.ToString()));


                }

                htmlContent = htmlContent.Replace("$totalCountValue$", resultSet.Count.ToString());
                htmlContent = htmlContent.Replace("$passedCountValue$", (resultSet.Where(a => a.ExecutionResult == TestExecutionOutcome.Pass)).ToList().Count.ToString());
                htmlContent = htmlContent.Replace("$failedCountValue$", (resultSet.Where(a => a.ExecutionResult == TestExecutionOutcome.Fail)).ToList().Count.ToString());
                // htmlContent = htmlContent.Replace("$failedCountValue$", (resultSet.Where(a => a.ExecutionResult == TestExecutionOutcome.Fail)).ToList().Count.ToString());
                htmlContent = htmlContent.Replace("$tableRows$", sb.ToString());

                using (var fileStream = new FileStream(consolidatedReport, FileMode.OpenOrCreate))
                using (StreamWriter swrite = new StreamWriter(fileStream))
                {
                    swrite.WriteLine(htmlContent);
                    swrite.Flush();
                    swrite.Close();
                }
            }
            catch (Exception ex)
            {
                //Summary.LogEvent(LogEventStatus.Info, string.Format("Failed to Create Consolidated Report. Exception Occured :{0}", ex));
            }
        }

    }
}
