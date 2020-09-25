using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Clarks.SAP.Connector
{
    class SAPIDocUtils
    {
       

   

        public static string ExtractImpl(XmlDocument document, string path, bool required)
        {
            XmlNode node = document.SelectSingleNode(path);
            if (!required && null == node)
                return String.Empty;
            if (null == node)
                throw new ApplicationException("Property does not exist");
            return node.InnerText;
        }

        public static string Extract(XmlDocument document, string path)
        {
            return ExtractImpl(document, path, true);
        }

        public static string IfExistsExtract(XmlDocument document, string path)
        {
            return ExtractImpl(document, path, false);
        }

        public static int ExtractInt(XmlDocument document, string path)
        {
            string s = Extract(document, path);
            return int.Parse(s);
        }

        public static uint ExtractUInt(XmlDocument document, string path)
        {
            string s = Extract(document, path);
            return uint.Parse(s);
        }

        public static int IfExistsExtractInt(XmlDocument document, string path)
        {
            string s = IfExistsExtract(document, path);
            if (0 == s.Length)
                return 0;
            return int.Parse(s);
        }

        public static long ExtractLong(XmlDocument document, string path)
        {
            string s = Extract(document, path);
            return long.Parse(s);
        }

        public static long IfExistsExtractLong(XmlDocument document, string path)
        {
            string s = IfExistsExtract(document, path);
            if (0 == s.Length)
                return 0;
            return long.Parse(s);
        }

        public static bool ExtractBool(XmlDocument document, string path)
        {
            string s = Extract(document, path);
            return bool.Parse(s);
        }

        public static bool IfExistsExtractBool(XmlDocument document, string path)
        {
            string s = IfExistsExtract(document, path);
            if (0 == s.Length)
                return false;
            return bool.Parse(s);
        }

 
    }
}
