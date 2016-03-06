﻿using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("root")]
public class XmlParser
{
    [XmlArray("levels")]
    [XmlArrayItem("level")]
    public List<Levels> allLevels = new List<Levels>();

    public static XmlParser Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XmlParser));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as XmlParser;
        }
    }

    public static XmlParser LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(XmlParser));
        return serializer.Deserialize(new StringReader(text)) as XmlParser;

    }
}

public class Levels
{
    [XmlAttribute("id")]
    public int levelId;

    [XmlElement("element")]
    public List<int> elementsMatrix;
}
