using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

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

        //manual split
        List<string> endingStrings = new List<string>();
        int j = 1;
        while ((j = lines[1].IndexOf("\",")) > 0)
        {
            string temp = lines[1].Substring(0, j);
            lines[1] = lines[1].Substring(j + 3);
            endingStrings.Add(temp);
        }

        endingStrings.Add(lines[1].Substring(0, lines[1].Length - 2));

        // var text = ((String)lines[1]).Split("\",", StringSplitOptions.None);
        for (int i = 0; i < factions.Length; i++)
        {
            if (factions[i].Trim() != "")
            {
                endings[factions[i].Trim()] = endingStrings[i].Trim();
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
