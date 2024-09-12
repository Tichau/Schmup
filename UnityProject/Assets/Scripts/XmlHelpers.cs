// <copyright file="XmlHelpers.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public static class XmlHelpers
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
            throw new ArgumentNullException(nameof(textAsset));
        }

        try
        {
            using (TextReader textStream = new StringReader(textAsset.text))
            {
                var serializer = new XmlSerializer(typeof(T));
                var data = (T)serializer.Deserialize(textStream);
                return data;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Asset of type '{typeof(T)}' failed to be deserialized. The following exception was raised:\n {exception}");
        }

        return default;
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
            throw new ArgumentNullException(nameof(path));
        }

        try
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                serializer.Serialize(stream, objectToSerialize);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Asset of type '{typeof(T)}' failed to be serialized. The following exception was raised:\n {exception}");
        }        
    }
}
