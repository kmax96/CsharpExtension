using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System
{
    public static class stringExtension
    {
        /// <summary>
        /// rsq = replace single quote
        /// Replace single quote to 2 single quote Same as .Replace("'","''"),
        ///
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>str.Replace("'","''")</returns>
        public static string rsq(this string str)
        {
            return str.Replace("'", "''");
        }

        /// <summary>
        /// String to int with Round (
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>int</returns>
        public static int toInt(this string str)
        {
            if (IsNumeric(str))
            {
                decimal d = decimal.Parse(str);
                int i = Decimal.ToInt32(Math.Round(d));
                return i;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// Same as Convert.ToInt32(
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>int</returns>
        public static decimal toDecimal(this string str)
        {
            if (IsNumeric(str))
            {
                decimal d = decimal.Parse(str);

                return d;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// check string is IsNumeric or not
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>bool</returns>
        public static bool IsNumeric(this string str)
        {
            float output;
            return float.TryParse(str, out output);
        }


        public static bool IsYear(this string str)
        {
            Text.RegularExpressions.Regex rgx = new Text.RegularExpressions.Regex("^[0-9]{4}$"); 

            return rgx.IsMatch(str);
        }


        public static bool ToBoolean(this string str)
        {
            if (!IsNullOrEmpty(str))
            {
                return Convert.ToBoolean(str);
            }
            else
            {
                return false;
            }
        }

        public static bool IsNullOrEmpty(this string str)
        {

            if (str != null && !str.Equals(DBNull.Value) && str != "" && str.Trim() != "")
            {

                return false;

            }
            return true;
        }

        public static DateTime ToDateTime(this string str, string dateFormat)
        {
            return DateTime.ParseExact(str, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static bool IsDateTime(this string str, string dateFormat)
        {
            DateTime output = DateTime.Now;

            return DateTime.TryParseExact(str, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out output);

        }


        public static bool IsDateTime(this string str)
        {
            DateTime output = DateTime.Now;

            return DateTime.TryParse(str, out output);

        }

        public static string Substring(this string str, string sybmol, Common.Direction direction)
        {
            return Substring(str, sybmol, direction, Common.IndexOf.last);
        }

         
        public static string Substring(this string str, string sybmol, Common.Direction direction, Common.IndexOf idxof)
        {
            if (!str.Contains(sybmol))
            {
                return str;
            } 

            int idx = str.LastIndexOf(sybmol);
            if (idxof == Common.IndexOf.first)
            {
                idx = str.IndexOf(sybmol);
            }

            if (direction == Common.Direction.after)
            {
                return str.Substring(idx + 1);
            }
            else {
                return str.Substring(0,idx);
            }         
        
        }  

        public static string limitTo(this string str, int endindex)
        {
            if (str.Length > endindex)
            {
                str = str.Substring(0,endindex) +"..";
            }
            return str;
        }

    }

    public static class EnumExtension
    {
        public static int ToInt(this Enum str)
        {
            return str.toInt();
            //return Convert.ToInt32(str);
        }
    }

 

    public static class objectExtension
    {
        public static string ToStr(this object values)
        {
            if (values != null)
            {
                return values.ToString();
            }
            return null;
        }

        public static string ToStr(this object values, string fromChar, string toChar)
        {
            if (values != null)
            {
                return values.ToString().Replace(fromChar,toChar);
            }
            return null;
        }



        public static ViewDataDictionary ToViewDataDictionary(this object values)
        {
            var dictionary = new ViewDataDictionary();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(values))
            {
                dictionary.Add(property.Name, property.GetValue(values));
            }
            return dictionary;
        }

        public static int toInt(this object o)
        {

            if (o == null)
            {
                return -1;
            }


            if (o.GetType().BaseType.ToString() == "System.Enum")
            {
                return (int)o;
            }
            else if (o is bool)
            {
                if (o.ToBoolean())
                {
                    return 1;
                }
                else
                    return 0;
            }
            else
            {

                if (IsNumeric(o))
                {
                    decimal d = decimal.Parse(o.ToString());
                    int i = Decimal.ToInt32(Math.Round(d));
                    return i;
                }
                else
                {
                    return -1;
                }
            }
            //return -1; 
        }



        public static bool IsNumeric(this object o)
        {
            if (o == null)
                return false;
            float output;
            return float.TryParse(o.ToString(), out output);
        }


        public static double toDouble(this object o)
        {
            if (IsNumeric(o))
            {
                double d = double.Parse(o.ToString());

                return d;
            }
            else
            {
                return -1;
            }
        }


        public static decimal toDecimal(this object o)
        {
            if (IsNumeric(o))
            {
                decimal d = decimal.Parse(o.ToString());

                return d;
            }
            else
            {
                return -1;
            }
        }

        public static bool IsNullOrEmpty(this object o)
        {

            if (o != null && !o.Equals(DBNull.Value) && o.ToString() != "" && o.ToString().Trim() != "")
            {

                return false;

            }
            return true;
        }


        public static bool ToBoolean(this object o)
        {
            if (!IsNullOrEmpty(o))
            {
                return Convert.ToBoolean(o);
            }
            else
            {
                return false;
            }
        }

        public static DateTime? ToDateTime(this object o)
        {
            if (o.IsNullOrEmpty())
            {
                return null;
            }
            return Convert.ToDateTime(o.ToString());
        }


        public static DateTime? ToDateTime(this object o, string dateFormat)
        {
            if (o == null)
            {
                return null;
            }

            return DateTime.ParseExact(o.ToString(), dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static bool IsDateTime(this object o, string dateFormat)
        {
            DateTime output = DateTime.Now;

            return DateTime.TryParseExact(o.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out output);
        }

        public static bool IsDateTime(this object o)
        {
            if (o == null)
            {
                return false;
            }


            DateTime output = DateTime.Now;

            return DateTime.TryParse(o.ToString(), out output);

        } 
    } 
}