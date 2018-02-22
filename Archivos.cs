using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace Archivos
{
    
    public interface IArchivo<T>
    {
        bool Guardar(string archivo, T datos);
        bool Leer(string archivo, out T datos);
    }  
    

     public class Texto : IArchivo<string>
    {
        public bool Guardar(string archivo, string datos)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(archivo, true))
                {
                    file.WriteLine(datos);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Leer(string archivo, out string datos)
        {
            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(archivo))
                {
                    datos = file.ReadToEnd();
                }

                return true;
            }
            catch (Exception)
            {
                datos = "";
                return false;
            }
        }
    }
    
    
    public class Xml<V> : IArchivo<V>
    {
        public bool Guardar(string archivo, V datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(V));
                //TextWriter writer = new StreamWriter(archivo);
                XmlTextWriter writer = new XmlTextWriter(archivo, Encoding.UTF8);
                serializer.Serialize(writer, datos);
                writer.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool Leer(string archivo, out V datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(V));
                //TextReader writer = new StreamReader(archivo);
                XmlTextReader writer = new XmlTextReader(archivo);
                datos = (V)serializer.Deserialize(writer);
                writer.Close();

                return true;
            }
            catch (Exception)
            {
                datos = default(V);
                return false;
            }
        }
    }

     public class Binario<T> : IArchivo<T>
    {
        public bool Guardar(string archivo, T datos)
        {
            Stream stream = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = new FileStream(archivo, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, datos);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if(!ReferenceEquals(stream, null))
                    stream.Close();
            }
        }
        public bool Leer(string archivo, out T datos)
        {
            Stream stream = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = new FileStream(archivo, FileMode.Open, FileAccess.Read, FileShare.Read);
                datos = (T)formatter.Deserialize(stream);
                stream.Close();

                return true;
            }
            catch (Exception)
            {
                datos = default(T);
                return false;
            }
            finally
            {
                if (!ReferenceEquals(stream, null))
                    stream.Close();
            }
        }
    }
}
