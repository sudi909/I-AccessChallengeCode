using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace I_AccessChallengeCode
{
    public partial class WebForm : System.Web.UI.Page
    {
        // to build random chars (Alphanumeric and Blank space)
        private const String chars = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private Random random = new Random();

        // path file create the CSV file
        private string csvPath = @"D:\testing.csv";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Insert Button
        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            int records = 100000; // 100k record row
            int stringContentLength = 256000; // 1K bytes length for String Content

            // create CSV file loop row from variable records 
            File.AppendAllLines(csvPath,
                (from r in Enumerable.Range(0, records)
                 let guid = Guid.NewGuid() // to create Globally Unique Identifier
                 let stringContent = GenerateRandomString(stringContentLength) // call func GenerateRandomString() to generate String Content
                 select $"{guid},{stringContent}"));
        }

        // Search Button
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string ID = "";
            int matchTimes = 0;

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[3] {
                new DataColumn("ID", typeof(string)),
                new DataColumn("String Content", typeof(string)),
                new DataColumn("Match Times", typeof(int))
            });

            // read CSV file line by line
            using (StreamReader reader = new StreamReader(csvPath))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    foreach (string cell in line.Split(','))
                    {
                        if (cell.Length <= 36) // GUID is 36 length
                        {
                            ID = cell; // Store the GUID
                        }
                        else
                        {
                            matchTimes = search(cell); // call func search to compare the search text with string content
                            if (matchTimes > 0)
                            {
                                dataTable.Rows.Add(ID, cell.Substring(0, 50), matchTimes); // in here I'am using substring for not show to many string content easy to see the data
                                //dataTable.Rows.Add(ID, cell, matchTimes); // If want to see all the string content just uncomment this and comment the subtring one.
                            }
                        }
                    }
                }

                GridViewResult.DataSource = dataTable;
                GridViewResult.DataBind();
            }
        }

        // func to generate random string
        private string GenerateRandomString(int strLength)
        {
            return new string(Enumerable.Repeat(chars, strLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // func to compare the search text with string content
        // must to compare it char by char because cannot use any .Net library search
        private int search(string stringContent)
        {
            string searchTxt = txtBoxSearch.Text;
            string remainderText;
            int match = 0;
            int i = 0;
            int scLength = stringContent.Length;
            int searchLength = searchTxt.Length;

            while (scLength >= searchLength)
            {
                remainderText = stringContent.Substring(i, searchLength);

                if (remainderText.Equals(searchTxt))
                {
                    match += 1; // if found equal then the matchtimes+1 to display to the datatable
                }

                scLength -= searchLength; //
                i++;
            }

            return match;
        }
    }
}