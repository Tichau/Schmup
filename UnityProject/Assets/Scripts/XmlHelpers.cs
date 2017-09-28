using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using UnityEngine;

public class XmlHelpers
{
    /// <summary>
    /// Create a C# object from a XML text asset.
    /// </summary>
    /// <typeparam name="T">The type of object you want to create.</typeparam>
    /// <param name="textAsset">The XML text asset where the object is serialized.</param>
    /// <returns>The deserialized C# object.</returns>
    public static T DeserializeFromXML<T>(TextAsset textAsset)
    {
        if (textAsset == null)
        {
            throw new ArgumentNullException("textAsset");
        }

        try
        {
            using (TextReader textStream = new StringReader(textAsset.text))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T data = (T)serializer.Deserialize(textStream);
                return data;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError("Asset of type '" + typeof(T) + "' failed to be deserialized. The following exception was raised:\n " + exception.Message);
        }

        return default(T);
    }

    /// <summary>
    /// Create a database from a XML text asset.
    /// </summary>
    /// <typeparam name="T">The type of objects of the database.</typeparam>
    /// <param name="textAsset">The XML text asset where the database is serialized.</param>
    /// <returns>A list of deserialized C# objects.</returns>
    public static List<T> DeserializeDatabaseFromXML<T>(TextAsset textAsset)
    {
        if (textAsset == null)
        {
            throw new ArgumentNullException("textAsset");
        }

        try
        {
            using (TextReader textStream = new StringReader(textAsset.text))
            {
                XmlRootAttribute xRoot = new XmlRootAttribute
                {
                    ElementName = "Datatable"
                };

                XmlSerializer serializer = new XmlSerializer(typeof(List<T>), xRoot);
                List<T> data = serializer.Deserialize(textStream) as List<T>;
                return data;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError("The database of type '" + typeof(T) + "' failed to load the assets. The following exception was raised:\n " + exception.Message);
        }

        return null;
    }

    /// <summary>
    /// Create an XML file from a C# object.
    /// </summary>
    /// <typeparam name="T">The type of object you want to serialize.</typeparam>
    /// <param name="path">The path of the XML file.</param>
    /// <param name="objectToSerialize">The object you want to serialize.</param>
    public static void SerializeToXML<T>(string path, T objectToSerialize)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException("path");
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter stream = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                serializer.Serialize(stream, objectToSerialize);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError("Asset of type '" + typeof(T) + "' failed to be serialized. The following exception was raised:\n " + exception.Message);
        }        
    }
}
