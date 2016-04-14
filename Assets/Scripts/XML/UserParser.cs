using System;
using System.IO;
using System.Text;
using System.Xml;
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

    [XmlElement("timeToFillHint")]
    public DateTime timeToFillHint;

    [XmlElement("timeToEndUnlimitedLifes")]
    public DateTime timeToEndUnlimitedLifes;

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
        using (var stream = new FileStream(path, FileMode.Open))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static UserParser Load(string path)
    {
        var serializer = new XmlSerializer(typeof(UserParser));
        using (var stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            return serializer.Deserialize(stream) as UserParser;
        }
    }

    public static UserParser LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(UserParser));
        return serializer.Deserialize(new StringReader(text)) as UserParser;
    }

    //Create new Xml File
    public void CreateNew(string path)
    {
        pearlCount = 300;
        lifeCount = 5;
        maxLifeCount = 5;
        hintCount = 3;
        timeToStartLifesTimer = DateTime.Now;
        timeToFillHint = DateTime.Now;
        timeToEndUnlimitedLifes = DateTime.Now;
        unlimitedForAllDay = 0;
        currentLevel4 = 1;
        currentLevel5 = 1;
        currentLevel6 = 1;
        currentLevel7 = 1;
        currentLevel8 = 1;
        currentLevel9 = 1;

        XmlSerializer serializer = new XmlSerializer(typeof(UserParser));
        FileStream stream = new FileStream(path, FileMode.Create);
        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        serializer.Serialize(writer, this);
        stream.Close();
    }
}