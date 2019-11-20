using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;

namespace DowningLLP.ExcelDataProvider
{
    public class ExcelDataAccess
    {
        public static string AssemblyDirectory
        {

            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                string testemail = "test";
                string testemail1 = string.Empty;

                string test = testemail == string.Empty ? testemail1 == string.Empty ? "1" : "2" : "3";
                return Path.GetDirectoryName(path);

            }
        }

        private string fileName = ProjectDirectory + ConfigurationManager.AppSettings["DataSheetPath"];


        public static string ProjectDirectory
        {
            get
            {
                string entryassembly = Environment.CurrentDirectory;
                string projdirectory = Directory.GetParent(entryassembly).Parent.FullName;
                return projdirectory;
            }
        }

        private string WriteFileName = ProjectDirectory + ConfigurationManager.AppSettings["DataSheetPath"];

        #region OLEDB connection
        public string TestDataFileConnection()
        {
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", WriteFileName);
            return con;
        }
        #endregion
        #region Write Test Data
        public void WriteTestData(string email)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {

                connection.Open();
                OleDbCommand cmd1 = new OleDbCommand("Update[RegisteredEmail$] set Email = (@value2) where Scenario = 'RegisteredUser'", connection);

                cmd1.Parameters.AddWithValue("@value2", email);
                cmd1.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
        #region Read Test Data

        #region Excel file read
        private DataTable ExcelToDataTable(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
            }
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);//.xlsx
            //Set the First Row as Column Name                                                                                                                                      
            //Return as DataSet
            DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["RegisteredEmail"];
            stream.Close();
            //return
            return resultTable;
        }

        List<Datacollection> dataCol = new List<Datacollection>();

        public void PopulateInCollection()
        {
            DataTable table = ExcelToDataTable(fileName);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row-1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);
                }
                //dataCol.Add(new Datacollection { colName = "", colValue="", rowNumber=""});
            }
        }

        public string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();
                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UserData GetTestData(string keyName)
        {
            UserData userData = new UserData();
            PopulateInCollection();
            Datacollection data = dataCol.FirstOrDefault(x => x.colValue == keyName);
            data = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber);
            userData.Email = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "Email").colValue;
            userData.Password = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "Password").colValue;
            userData.PostCode = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "PostCode").colValue;
            return userData;
        }
        public UserData GetTestData1(string keyName,string sheet)
        {
            UserData userData = new UserData();
            PopulateInCollection();
            Datacollection data = dataCol.FirstOrDefault(x => x.colValue == keyName);
            data = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber);
            userData.Email = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "Email").colValue;
            userData.Password = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "Password").colValue;
            userData.PostCode = dataCol.FirstOrDefault(x => x.rowNumber == data.rowNumber && x.colName == "PostCode").colValue;
            return userData;
        }

        #endregion
        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }

    }
}
#endregion
