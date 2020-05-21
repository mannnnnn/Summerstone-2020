using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;

public class Endings : MonoBehaviour
{
    public static Endings GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<Endings>();
    }

    Dictionary<string, string> endings = new Dictionary<string, string>();

    void Awake()
    {
        string[] lines = Resources.Load<TextAsset>("Text/Endings").text.Split('\n');
        var factions = lines[0].Split(',');
        var text = lines[1].Split(',');
        for (int i = 0; i < factions.Length; i++)
        {
            if (factions[i].Trim() != "")
            {
                endings[factions[i].Trim()] = text[i].Trim();
            }
        }
    }

    public string GetEnding(string faction)
    {
        string key = $"{faction} Ending";
        if (endings.ContainsKey(key))
        {
            return endings[key];
        }
        throw new DialogError($"Endings file has no entry for the {faction} Ending.");
    }
}
