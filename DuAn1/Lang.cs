using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;

namespace DuAn1
{
    [XmlRoot]
    public class PLanglist
    {
        private Dictionary<string,LangItem> langxml;

        public PLanglist()
        {
            langxml = new Dictionary<string, LangItem>();
        }


        public void Load(Object file)
        {
            ListtoDict(LangXml.Load(file).Trans,ref langxml);
        }

        public LangItem this[string key]
        {

            get
            {
                LangItem value = null;
                if (langxml.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
             
            }
        }

        public static void ListtoDict(List<LangItem> listitem, ref Dictionary<string, LangItem> dict)
        {
            if (listitem == null) return;

            foreach (LangItem e in listitem)
            {
                LangItem value;

                if (dict.TryGetValue(e.Key, out value))
                {
                    value.Traduction = e.Traduction;
                }
                else
                {
                    dict.Add(e.Key,e);
                }
            }
            
        }


    }

    public class LangXml
    {
        [XmlElement("Name")]
        public string Langname;

        [XmlArray("Trans"),XmlArrayItem("Item",typeof(LangItem))]
        public List<LangItem> Trans = new List<LangItem>();

        public static LangXml Load(Object file)
        {
            TextReader reader;

            try
            {
                Stream str = file as Stream;

                if (str != null)
                {
                    reader = new StreamReader(str);
                }
                else
                {
                    reader = new StreamReader(file.ToString());
                }
            }
            catch (Exception e)
            {
               System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }

            LangXml value = null;
            XmlSerializer serializer = new XmlSerializer(typeof(LangXml));

            try
            {
                value = (LangXml)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                
            }
            finally
            {
                reader.Close();
            }
           
            return value;

        }


        public void CreateFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LangXml));
            TextWriter writer = new StreamWriter(file);

            serializer.Serialize(writer, this);
            writer.Close();
        }

    }

    public class LangItem : LViewModel
    {
        private string _key;
        private string _traduction;

        [XmlAttribute("Key")]
        public string Key
        {
            get => _key;
            set
            {
                _key = value; 
                NotifyPropertyChanged("Key");
            }
        }

        [XmlAttribute("Traduction")]
        public string Traduction
        {
            get => _traduction;
            set
            {
                _traduction = value;
                NotifyPropertyChanged("Traduction");
            }
        }
    }

    public class LViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propname));
            }
        }
    }
}
