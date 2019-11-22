using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ParseLOC(@"UNA:+.? '
                            UNB + UNOC:3 + 2021000969 + 4441963198 + 180525:1225 + 3VAL2MJV6EH9IX + KMSV7HMD + CUSDECU - IE++1++1'
                            UNH + EDIFACT + CUSDEC:D: 96B: UN:145050'
                            BGM + ZEM:::EX + 09SEE7JPUV5HC06IC6 + Z'
                            LOC + 17 + IT044100'
                            LOC + 18 + SOL'
                            LOC + 35 + SE'
                            LOC + 36 + TZ'
                            LOC + 116 + SE003033'
                            DTM + 9:20090527:102'
                            DTM + 268:20090626:102'
                            DTM + 182:20090527:102'
                            ");
                XmlDocument doc = new XmlDocument();
                doc.Load("../../../XMLFile1.xml");
                GetRefCodRefText(doc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string[] ParseLOC(string stringWithLOC)
        {
            try
            {

                List<string> arrayWithoutLOC = new List<string>();
                string[] arraySplitElements = stringWithLOC.Split("'");
                foreach (string element in arraySplitElements)
                {
                    if (!element.Contains("LOC"))
                    {
                        TackSecondThird(arrayWithoutLOC, element);
                    }
                }

                return arrayWithoutLOC.ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void TackSecondThird(List<string> arrayWithoutLOC, string element)
        {
            string[] elementSplitByPlus = element.Split("+");
            if (elementSplitByPlus.Length > 1)
            {
                arrayWithoutLOC.Add(elementSplitByPlus[1]);
            }
            if (elementSplitByPlus.Length > 2)
            {
                arrayWithoutLOC.Add(elementSplitByPlus[2]);
            }
        }
        public static Dictionary<string, string> GetRefCodRefText(XmlDocument doc)
        {
            string MWB = doc.SelectSingleNode("//Reference[@RefCode='MWB']").InnerText;
            string CAR = doc.SelectSingleNode("//Reference[@RefCode='CAR']").InnerText;
            string TRV = doc.SelectSingleNode("//Reference[@RefCode='TRV']").InnerText;
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("MWB", MWB);
            values.Add("CAR", CAR);
            values.Add("TRV", TRV);
            return values;

        }
    }
}
