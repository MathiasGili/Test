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

                Console.WriteLine(@"1.Taking the following EDIFACT message text, write some code to parse out the all the LOC segments and populate an array with the 2nd and 3rd element of each segment. ");
                Console.WriteLine();
               string EDIFACT = @"UNA:+.? '
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
                            ";
                ParseLOC(EDIFACT);
                Console.WriteLine();
                Console.WriteLine(@"2.Taking the following XML document, write code to extract the RefText values for the following RefCodes:   ‘MWB’, ‘TRV’ and ‘CAR’");
                Console.WriteLine();
                XmlDocument doc = new XmlDocument();
                doc.Load("../../../XMLFile1.xml");
                GetRefCodRefText(doc);
                Console.ReadLine();
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

                List<string> elementSecondThird = new List<string>();
                string[] segments = stringWithLOC.Split("'");
                foreach (string segment in segments)
                {
                    if (!segment.Contains("LOC"))
                    {
                        TackSecondThird(elementSecondThird, segment);
                    }
                }

                foreach (string segmentWithoutLoc in elementSecondThird)
                {
                    Console.WriteLine(segmentWithoutLoc);
                }
                
                return elementSecondThird.ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void TackSecondThird(List<string> segmentsWithoutLOC, string segment)
        {
            string[] elements = segment.Split("+");
            if (elements.Length > 1)
            {
                segmentsWithoutLOC.Add(elements[1]);
            }
            if (elements.Length > 2)
            {
                segmentsWithoutLOC.Add(elements[2]);
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
            foreach (var value in values)
            {
                Console.WriteLine(value.Key + " - " + value.Value); 
            }
            return values;

        }
    }
}
