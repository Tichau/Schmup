using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using UnityEngine;

public class XmlHelpers
{
    public static List<T> LoadFromTextAsset<T>(TextAsset textAsset, System.Type[] extraTypes = null)
    {
        if (textAsset == null)
        {
            throw new ArgumentNullException("textAsset");
        }

        System.IO.TextReader textStream = null;

        try
        {
            textStream = new System.IO.StringReader(textAsset.text);

            XmlRootAttribute xRoot = new XmlRootAttribute
            {
                ElementName = "Datatable"
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), xRoot);
            List<T> data = serializer.Deserialize(textStream) as List<T>;

            textStream.Close();

            return data;
        }
        catch (System.Exception exception)
        {
            Debug.LogError("The database of type '" + typeof(T) + "' failed to load the asset. The following exception was raised:\n " + exception.Message);
        }
        finally
        {
            if (textStream != null)
            {
                textStream.Close();
            }
        }

        return null;
    }

    public static void SaveToXML<T>(string path, T objectToSerialize) where T : class
    {
        if (string.IsNullOrEmpty(path))
            return;
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamWriter stream = new StreamWriter(path, false, new UTF8Encoding(false)))
        {
            Debug.Log(stream.ToString());

            serializer.Serialize(stream, objectToSerialize);
            stream.Close();
        }
    }

}
