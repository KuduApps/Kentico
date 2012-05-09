using System;
using System.Data;
using System.Text;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Settings_Development_Export_Settings : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get export type
        string type = QueryHelper.GetString("type", string.Empty);

        if (!string.IsNullOrEmpty(type))
        {
            // Export insert script for specified table
            MakeResponse(type);
        }
    }


    protected void MakeResponse(string type)
    {
        // Clear all generated HTML code
        Response.Clear();
        string tableName = null;

        // Set used tables
        if (type == "settings")
        {
            tableName = "CMS_SettingsKey";
        }
        else if (type == "categories")
        {
            tableName = "CMS_SettingsCategory";
        }

        // Generate sql script
        string responseText = GenerateScript(tableName);
        if (responseText != null)
        {
            // Add header containing attachment
            Response.AddHeader("content-disposition", "attachment;filename=" + tableName + ".sql");

            // Ensure UTF-8 formating
            Response.ContentEncoding = Encoding.UTF8;

            // Set content type
            Response.ContentType = "text/sql";
            Response.Charset = string.Empty;

            // Disable view state
            EnableViewState = false;

            // Clear content of response
            Response.ClearContent();

            // Start writing sql script to output stream
            Response.Output.Write(responseText);
            Response.Output.Close();

            // End response
            RequestHelper.EndResponse();
        }
    }


    /// <summary>
    /// Generates insert script based on the tableName.
    /// </summary>
    /// <param name="tableName">DB Name of the table</param>
    /// <returns>Insert script for specified table.</returns>
    protected static string GenerateScript(string tableName)
    {
        int indexOfKeyDefaultValue = -1;
        int indexOfKeyValue = -1;

        bool keys = (tableName == "CMS_SettingsKey");
        string script = "SET IDENTITY_INSERT [" + tableName + "] ON;\n";

        if (keys)
        {
            script = "";
        }

        // Get column data
        DataSet columns =
            SqlHelperClass.ExecuteQuery(
                "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'",
                null, QueryTypeEnum.SQLQuery);

        string dataQuery = "SELECT * FROM " + tableName;

        // Export only global settings keys
        if (keys)
        {
            dataQuery += " WHERE ((SiteID IS NULL) AND ([KeyName] NOT LIKE 'test%')) ORDER BY KeyCategoryID ASC, KeyOrder ASC, KeyDisplayName ASC";
        }
        else
        {
            dataQuery += " WHERE [CategoryName] NOT LIKE 'test%' ORDER BY CategoryLevel ASC, CategoryOrder ASC";
        }

        // Get all table data
        DataSet tableData = SqlHelperClass.ExecuteQuery(dataQuery, null, QueryTypeEnum.SQLQuery);

        // Check if sources are not empty
        if (!DataHelper.DataSourceIsEmpty(tableData) && !DataHelper.DataSourceIsEmpty(columns))
        {
            // Generate insert script
            string insertScript = "INSERT INTO [" + tableName + "] (";
            string replaceTemplate = "("; // Replace template
            int i = 0; // Column index in replace template
            int lastCategoryId = 0;
            string dateTimeColumns = null; // Contains all datetime columns to remove

            foreach (DataRow dr in columns.Tables[0].Rows)
            {
                if ((i > 0) || !keys)
                {
                    // Write all columns to insert script
                    string columnName =
                    insertScript += "[" + dr["COLUMN_NAME"] + "], ";

                    // Handle generating the replace template
                    string dataType = dr["DATA_TYPE"].ToString();
                    if ((dataType == "nchar") || (dataType == SqlHelperClass.DATATYPE_TEXT) || (dataType == SqlHelperClass.DATATYPE_LONGTEXT))
                    {
                        // Get index of default value column
                        if ((indexOfKeyDefaultValue < 0) && (dr["COLUMN_NAME"].ToString().ToLower() == "keydefaultvalue"))
                        {
                            indexOfKeyDefaultValue = i;
                        }

                        //Get index of key value
                        if ((indexOfKeyValue < 0) && (dr["COLUMN_NAME"].ToString().ToLower() == "keyvalue"))
                        {
                            indexOfKeyValue = i;
                        }

                        replaceTemplate += "N'{" + i + "}',";
                    }
                    else if (dataType == SqlHelperClass.DATATYPE_GUID)
                    {
                        replaceTemplate += "'{" + i + "}',";
                    }
                    else if (dataType == SqlHelperClass.DATATYPE_INTEGER)
                    {
                        replaceTemplate += "{" + i + "},";
                    }
                    else if (dataType == SqlHelperClass.DATATYPE_DATETIME)
                    {
                        // Handles "last modified" columns
                        if (dr["COLUMN_NAME"].ToString().ToLower().IndexOf("lastmodified") != -1)
                        {
                            dateTimeColumns += dr["COLUMN_NAME"] + ";";
                            replaceTemplate += "GetDate(),";
                            continue;
                        }
                        else
                        {
                            replaceTemplate += "'{" + i + "}',";
                            break;
                        }
                    }
                    else
                    {
                        replaceTemplate += "'{" + i + "}',";
                    }
                }
                i++; // Increment index of handled column
            }

            // End the replace template
            replaceTemplate = replaceTemplate.TrimEnd(',') + ")";

            // Complete insert template
            insertScript = insertScript.Trim().TrimEnd(',') + ") VALUES " + replaceTemplate + "\r\n";

            // Remove last modified columns from datasource
            if (!string.IsNullOrEmpty(dateTimeColumns))
            {
                foreach (string column in dateTimeColumns.Split(';'))
                {
                    if (!string.IsNullOrEmpty(column))
                    {
                        tableData.Tables[0].Columns.Remove(column);
                    }
                }
            }

            foreach (DataRow dr in tableData.Tables[0].Rows)
            {
                if (keys)
                {
                    int categoryId = ValidationHelper.GetInteger(dr["KeyCategoryID"], 0);
                    if (lastCategoryId != categoryId)
                    {
                        script += "\r\n";
                    }
                    lastCategoryId = categoryId;
                }

                // Handle NULL values in single rows
                object[] itemsRow = dr.ItemArray;

                // Get default value instead of current value (current value could be wrong)
                if ((indexOfKeyValue >= 0) && (indexOfKeyDefaultValue >= 0))
                {
                    itemsRow[indexOfKeyValue] = itemsRow[indexOfKeyDefaultValue];
                }

                for (int c = 0; c < itemsRow.Length; c++)
                {
                    // Handle apostrophes in text fields
                    itemsRow[c] = itemsRow[c].ToString().Replace("'", "''");
                    if (string.IsNullOrEmpty(itemsRow[c].ToString()))
                    {
                        itemsRow[c] = "NULL";
                    }
                }

                // Fill insert script and add it to the result script
                script += string.Format(insertScript, itemsRow).Replace("'NULL'", "''").Replace("N''", "NULL");
            }
        }

        // Complete the result script and return it
        if (!keys)
        {
            script += "\r\nSET IDENTITY_INSERT [" + tableName + "] OFF;";
        }

        return script;
    }
}
