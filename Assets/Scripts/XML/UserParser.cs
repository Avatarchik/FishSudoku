using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("root")]
public class UserParser
{
    [XmlElement("pearlCount")]
    public int pearlCount;

    [XmlElement("lifeCount")]
    public int lifeCount;

    [XmlElement("maxLifeCount")]
    public int maxLifeCount;

    [XmlElement("hintCount")]
    public int hintCount;

    [XmlElement("timeToStartLifesTimer")]
    public DateTime timeToStartLifesTimer;

    [XmlElement("unlimitedForAllDay")]
    public int unlimitedForAllDay;

    [XmlElement("CurrentLevel4")]
    public int currentLevel4;

    [XmlElement("CurrentLevel5")]
    public int currentLevel5;

    [XmlElement("CurrentLevel6")]
    public int currentLevel6;

    [XmlElement("CurrentLevel7")]
    public int currentLevel7;

    [XmlElement("CurrentLevel8")]
    public int currentLevel8;

    [XmlElement("CurrentLevel9")]
    public int currentLevel9;

    //Time 

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(UserParser));
        using (var stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static UserParser Load(string path)
    {
        var serializer = new XmlSerializer(typeof(UserParser));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as UserParser;
        }
    }

    public static UserParser LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(UserParser));
        return serializer.Deserialize(new StringReader(text)) as UserParser;
    }
}