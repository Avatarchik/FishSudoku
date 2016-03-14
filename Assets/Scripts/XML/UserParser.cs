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