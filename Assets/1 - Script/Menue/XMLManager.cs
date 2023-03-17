using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class StatEntry
{
    public string type;
    public int level;
    public int xp;
}

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;
    public Leaderboard leaderboard;
    void Awake()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Stat/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Stat/");
        }
    }
    
    public void SaveStat(List<StatEntry> scoresToSave)
    {
        leaderboard.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Stat/PlayerStat.xml", FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }
    public List<StatEntry> LoadStat()
    {
        if (File.Exists(Application.persistentDataPath + "/Stat/PlayerStat.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Stat/PlayerStat.xml", FileMode.Open);
            leaderboard = serializer.Deserialize(stream) as Leaderboard;
            stream.Close();
        }
        return leaderboard.list;
    }
}
[System.Serializable]
public class Leaderboard
{
    public List<StatEntry> list = new List<StatEntry>();
}