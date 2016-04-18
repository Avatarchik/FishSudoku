using UnityEngine;
using System.IO;

public class CreateXml : MonoBehaviour
{
    private string path;
    void Start()
    {
        if(Application.isEditor)
        {
            path = Path.Combine(Application.persistentDataPath, "UserInfo.xml");
        }
        else
        {
            path = Path.Combine(Application.persistentDataPath, "UserInfo.xml");
        }

        if (!File.Exists(path))
        {
            UserParser usp = new UserParser();
            usp.CreateNew(path);

            Debug.Log("Create UserInfo.xml");
        }
        else
        {
            Debug.Log(Application.persistentDataPath);
        }
    }
}
