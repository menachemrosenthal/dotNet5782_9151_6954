using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DalApi
{
    class XMLTools
    {
        static string dir = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml";

       // static string dir = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml";

        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new(filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception)
            {

                
            }
        }

        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new(typeof(List<T>));
                    FileStream file = new(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else 
                    return new List<T>();
            }
            catch (Exception)
            {

                throw new KeyNotFoundException("the path not exists");
            }
        }


    }
}
