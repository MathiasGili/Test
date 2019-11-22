###Developer Technical Questions.  

DEMO: App/netcoreapp2.2/win-x64/test.exe

It was decided to do the first and second exercises in the same project.    
1. The first problem is solved with the method ParseLOC  
    a.First parse out the all the LOC segments     
        i. split the segment by '    
        i. parse out the all the LOC segments     
    b.Second populate an array with the 2nd and 3rd element of each segment with the method TackSecondThird    
        i.split the segment by "+"   
        ii.take the 2nd and 3rd element of each segment   



```csharp
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
```
2. The first problem is solved with the method GetRefCodRefText  
    a. First read the file XML from XMLFile1  
    B. GetRefCodRefText recive the XML file  
    c. GetRefCodRefTex return a Dictionary with Keyvaluepair (Key - RefCode, Value - RefText)     

```csharp
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
```