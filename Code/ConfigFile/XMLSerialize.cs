using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Stahli2Robots
{
    public class XMLSerialize
    {
        public XMLSerialize()
        {
        }

        public void Serialize(object obj, string filePath, string fileName, bool displyError)
        {
            TextWriter writer = null;
            try
            {
                VerifyDirectory(filePath);
                if (fileName == null)
                {
                    filePath += obj.GetType().ToString();
                }
                else
                {
                    filePath += fileName;
                }

                if (!filePath.Contains(".xml"))
                {
                    filePath += ".xml";
                }

                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, obj);
                writer.Close();
            }
            catch (Exception exc)
            {
                string msg = "Exception: " + exc.Message;
                if (exc.InnerException != null)
                {
                    msg += ":\n" + exc.InnerException.Message;
                }

                if (displyError)
                {
                    MessageBox.Show(msg, "File Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        public object DeSerialize(object obj, string filePath, string fileName, bool displyError)
        {
            TextReader reader = null;
            try
            {
                XmlAttributeOverrides at = new XmlAttributeOverrides();
                if (fileName == null)
                {
                    filePath += obj.GetType().ToString();
                }
                else
                {
                    filePath += fileName;
                }

                if (!filePath.Contains(".xml"))
                {
                    filePath += ".xml";
                }

                reader = new StreamReader(filePath);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                object retObject = serializer.Deserialize(reader);
                reader.Close();
                return retObject;
            }
            catch (Exception exc)
            {
                string msg = "Exception: " + exc.Message;
                if (exc.InnerException != null)
                {
                    msg += ":\n" + exc.InnerException.Message;
                }

                if (displyError)
                {
                    MessageBox.Show(msg, "File Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                if (reader != null)
                {
                    reader.Close();
                }

                return obj;
            }
        }

        private void VerifyDirectory(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch
            {
            }
        }
    }
}
