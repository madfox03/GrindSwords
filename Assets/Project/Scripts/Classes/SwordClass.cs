using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;

[System.Serializable]
[XmlType("sword")]
public class SwordClass
{
    [XmlAttribute("sword_name")]
    public string swordname;

    [XmlAttribute("sword_lvl")]
    public int swordlvl;
    
    [XmlElement("sword_sharpening")]
    public List<SharpeningClass> sharpening = new List<SharpeningClass>();

    [XmlAttribute("sword_prefab")]
    public GameObject swordprefab;
}

[System.Serializable]
[XmlType("swords")]
public class SwordCollection
{
    [XmlElement("sword")]
    public SwordClass[] Swords;

    public static SwordCollection Load()
    {
        return LoadFromTextAsset("Data/swords");
    }

    public static SwordCollection LoadFromTextAsset(string path)
    {
        TextAsset loadtext = Resources.Load<TextAsset>(path);
        var serializer = new XmlSerializer(typeof(SwordCollection), new XmlRootAttribute("swords"));
        return serializer.Deserialize(new StringReader(loadtext.text)) as SwordCollection;
    }

    public override string ToString()
    {
        string result = "";
        foreach (SwordClass sword in Swords)
        {
            result += sword.ToString() + "\n";
        }
        return result;
    }

    public SwordClass GetByLvL(int lvl)
    {
        return Array.Find(Swords, x => x.swordlvl.CompareTo(lvl) == 0);
    }

    public SwordClass GetByName(string name)
    {
        return Array.Find(Swords, x => x.swordname.CompareTo(name) == 0);
    }
}