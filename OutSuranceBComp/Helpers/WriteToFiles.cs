using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OutSuranceBComp.Helpers
{
    public class WriteToFiles
    {
        public void WriteResultToTextFile(List<string> list, List<string> list2, string whichFile)
        {
            ConstructFile(list, list2, whichFile);
        }

        private static void ConstructFile(List<string> list, List<string> list2, string whichFile)
        {
            string path = ConfigurationManager.AppSettings["ResultsFolder"].ToString();

            //check for path exist if not create it
            try
            {
                // ... If the directory doesn't exist, create it.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
                // Fail silently.
            }

            using (FileStream fs = File.Create(path+""+whichFile+".txt"))
            {
                TextWriter t = new StreamWriter(fs);
                t.WriteLine("Date : " + DateTime.Now);
                t.WriteLine("________________________START______________________________");

                foreach (var values in list)
                {
                    t.WriteLine(values);
                }

                if (whichFile != "CreateOrderedListOfAddressesAsc")
                {
                    if (list2 != null)
                    {
                        t.WriteLine("________________________Names ______________________________");
                        foreach (var values in list2)
                        {
                            t.WriteLine(values);
                        }
                    }
                }

                t.WriteLine("________________________END________________________________");
                t.Close();
            }
        }
    }
}
