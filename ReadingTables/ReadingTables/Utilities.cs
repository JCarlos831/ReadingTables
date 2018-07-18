using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using System.Collections;

namespace ReadingTables
{
    public class Utilities
    {
        static List<TableDataCollection> tableDataCollections = new List<TableDataCollection>();

        public static void ReadTable(IWebElement table)
        {
            // Get all the columns from the table
            var columns = table.FindElements(By.TagName("th"));

            // Get all the rows from the table
            var rows = table.FindElements(By.TagName("tr"));

            // Create row index
            int rowIndex = 0;

            foreach (var row in rows)
            {
                int colIndex = 0;

                var colDatas = row.FindElements(By.TagName("td"));

                foreach (var colValues in colDatas)
                {
                    tableDataCollections.Add(new TableDataCollection
                    {
                        RowNumber = rowIndex,
                        ColumnName = columns[colIndex].Text != "" ? null :
                                     columns[colIndex].Text : colIndex.ToString(),
                        ColumnValue = colValues.Text,
                        ColumnSpecialValues = colValues.Text = "" 
                    });

                    // Move to next column

                    colIndex++;
                }

                rowIndex++;

            }
        }

        public static string ReadCell(string columnName, int rowNumber)
        {
            var data = (from e in tableDataCollections
                        where e.ColumnName == columnName && e.RowNumber == rowNumber
                        select e.ColumnValue).SingleOrDefault();

            return data;
        }

        public static void PerformActionOnCell(string columnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            foreach (int rowNumber in GetDynamicRowNumber(refColumnName, refColumnValue))
            {
                var cell = (from e in tableDataCollections
                            where e.ColumnName == columnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValues).SingleOrDefault();

                // Need to operate on those controls
                if(controlToOperate != null && cell != null)
                {
                    var returnedControl = (from c in cell
                                           where c.GetAttribute("value") == controlToOperate
                                           select c).SingleOrDefault();
                    // ToDo: Currently only clikc is upported, future is not taken care of here
                    returnedControl?.Click();
                }
                else
                {
                    cell?.First().Click();
                }
        }

        private static IEnumerable GetDynamicRowNumber(string columnName, string columnValue)
        {
            // Dyanmic Row
            foreach (var table in tableDataCollections)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                    yield return table.RowNumber;
            }
        }
    }

    public class TableDataCollection
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }

        public IEnumerable<IWebElement> ColumnSpecialValues{ get; set; }
    }
}
