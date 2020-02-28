using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{


    public static class DataTableExtension
    {




        public static string toJsonString(this DataTable dt, string colName)
        {
            string json = "";
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<string> rows = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    rows.Add(dr[colName].ToString());



                }

                dt.Dispose();

                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {
                Logging.writeLog(ex);
            }
            return json;
        }




        public static string toJsonString(this DataTable dt)
        {
            string json = "";
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {
                Logging.writeLog(ex);
            }
            return json;
        }

        public static DataRow LastRow(this DataTable dt)
        {
            if (dt.HasData())
            {
                return dt.Rows[dt.Rows.Count - 1];
            }
            return null;
        }

        public static bool HasData(this DataTable dt)
        {
            bool result = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public static void DisposeWithNull(this DataTable dt)
        {
            if (dt != null)
                dt.Dispose();

        }

        #region XML function
        public static void SaveToXML(this DataTable dt, string tableName, string save_fileName)
        {
            dt.TableName = tableName;
            dt.WriteXml(save_fileName, XmlWriteMode.WriteSchema);
            dt.Dispose();
            dt = null;
            save_fileName = null;
            tableName = null;
        }

        public static void LoadDataFromXML(this DataTable dt, string _path)
        {
            dt.ReadXml(_path);
        }
        #endregion

        #region Calculate Function

        public static decimal Avg(this DataTable dt, string colName)
        {
            int ignoreCount = 0;

            if (dt.HasData())
            {
                if (dt.Columns.Contains(colName))
                {

                    decimal t = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[colName].IsNumeric())
                        {
                            t += dr[colName].toDecimal();

                        }
                        else
                        {
                            // t = -1;
                            ignoreCount++;
                        }
                    }
                    if (dt.Rows.Count - ignoreCount > 0)
                    {

                        t = t / (dt.Rows.Count - ignoreCount);
                    }

                    return t;
                }
            }
            return 0;
        }

        public static decimal Sum(this DataTable dt, string colName)
        {
            if (dt.HasData())
            {
                if (dt.Columns.Contains(colName))
                {

                    decimal t = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[colName].IsNumeric())
                        {
                            t += dr[colName].toDecimal();

                        }
                        else
                        {
                            // t = -1;
                        }
                    }

                    return t;
                }
            }
            return 0;
        }

        #endregion
    }


    public static class DataRowExtension
    {

        public static string toJsonString(this DataRow dr)
        {
            string json = "";
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;

                row = new Dictionary<string, object>();
                foreach (DataColumn col in dr.Table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);

                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {
                Logging.writeLog(ex);
            }
            return json;
        }
    }
}