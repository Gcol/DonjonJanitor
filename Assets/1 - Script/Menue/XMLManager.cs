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
    public AllStat allStat;

    void Awake()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Stat/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Stat/");
        }
    }
    
    public void SaveStat(List<StatEntry> scoresToSave)
    {
        allStat.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(AllStat));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Stat/PlayerStat.xml", FileMode.Create);
        serializer.Serialize(stream, allStat);
        stream.Close();
    }
    public List<StatEntry> LoadStat()
    {
        if (File.Exists(Application.persistentDataPath + "/Stat/PlayerStat.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AllStat));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Stat/PlayerStat.xml", FileMode.Open);
            allStat = serializer.Deserialize(stream) as AllStat;
            stream.Close();
        }
        return allStat.list;
    }

    public void UpdateStat(List<StatEntry> allCurrentStat, string entryName, int xpGain)
    {
        StatEntry currentStat = allCurrentStat.Find((x) => x.type == entryName);

        if (currentStat == null)
        {
            currentStat = new StatEntry { type = entryName, level = 0, xp = 0 };
            allCurrentStat.Add(currentStat);

        }

        currentStat.xp += xpGain;
        while (currentStat.xp >= (currentStat.level + 1 * 100))
        {
            currentStat.xp -= currentStat.level + 1 * 100;
            currentStat.level++;
        }
    }
}
[System.Serializable]
public class AllStat
{
    public List<StatEntry> list = new List<StatEntry>();
}