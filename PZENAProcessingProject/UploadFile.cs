using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZENAProcessingProject
{
    public class UploadFile
    {
        private readonly string _connectionString;
        private int batchSize;
        private int bulkCopyTimeout;
        public UploadFile(string connectionString)
        {
            _connectionString = connectionString; //Can be set in a config file
            batchSize = 10000;
            bulkCopyTimeout = 0;

        }

        public void Upload(string uploadFilePath, string tableName)
        {
            var lines = System.IO.File.ReadAllLines(uploadFilePath);
            if (lines.Count() == 0) return;
            var columns = lines[0].Split(',');
            var table = new DataTable();

            foreach (var c in columns)
                table.Columns.Add(c);

            var dateColumns = new List<string> { "lastupdated", "firstadded", "firstpricedate", "lastpricedate", "firstquarter", "lastquarter", "date" }; // Date columns need to be null, not blank, in the dataTable before SQL upload

            for (int i = 1; i < lines.Count(); i++) //Avoid the headers row of the csv
            {
                var data = lines[i].Split(',');
                var row = table.NewRow();
                for (int j = 0; j < data.Length; j++)
                {
                    if (dateColumns.Contains(columns[j].ToLower()) && string.IsNullOrWhiteSpace(data[j]))
                    {
                        row[j] = DBNull.Value;
                    }
                    else
                    {
                        row[j] = data[j];
                    }
                }
                table.Rows.Add(row);
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = bulkCopyTimeout;
                bulkCopy.BatchSize = batchSize;

                AutoMapColumns(bulkCopy, table);

                try
                {
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
        public static void AutoMapColumns(SqlBulkCopy sbc, DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName); //Map the columns of a DataTable to the columns of a SqlBulkCopy object
            }
        }
    }
}
