using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Amov.Planner
{
    [Serializable]
    public  class Set
    {
       public int DeviceId { get; set; }
       public string IP{ get; set; }
       public int Port { get; set; }
       public bool IsConnect { get; set; }

      public bool IsComm { get; set; }
        public string ComPort { get; set; }

    }

    public class Tool
    {
        public static List<Set> ConfSet;
        public Tool()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "set.bin");

                if (!File.Exists(path))
                {
                    ConfSet = new List<Set>();
                }
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(Set[]));
                    ConfSet=((Set[])xmldes.Deserialize(fs)).ToList();
                }
            }
            catch (Exception ex)
            {
                ConfSet = new List<Set>();
            }
        }

     
        public  void Serializer()
        {
            string path = Path.Combine(Application.StartupPath, "set.bin");

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Set[]));
                //序列化对象  
                xml.Serialize(fs, ConfSet.ToArray());
                fs.Position = 0;
                fs.Close();
            }

        }
    }
}
