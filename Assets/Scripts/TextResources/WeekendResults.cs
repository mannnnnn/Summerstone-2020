using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;

public enum WeekendResult
{
    Fail, Win, Perfect
}

public class WeekendResults : MonoBehaviour
{
    public static WeekendResults GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<WeekendResults>();
    }

    Dictionary<string, string> resultTitles = new Dictionary<string, string>();
    Dictionary<string, string> results = new Dictionary<string, string>();

    void Awake()
    {
        foreach (string line in Resources.Load<TextAsset>("Text/WeekendResults").text.Split('\n'))
        {
            var values = line.Split(new[] { ',' }, 3);


            if (values[0] != null && values[0].Trim() != "")
            {
                resultTitles[values[0]] = values[1];
                results[values[0]] = values[2].Substring(0,values[2].Length-2);
            }

        }
    }

    public string GetWeekendResult(int week, string result)
    {
        string key = $"Week{week}{result}";
        if (results.ContainsKey(key))
        {
            return results[key];
        }
        throw new DialogError($"Weekend Results file has no entries for Week {week} with Result {result}.");
    }

    public string GetWeekendTitleResult(int week, string result)
    {
        string key = $"Week{week}{result}";
        if (results.ContainsKey(key))
        {
            return resultTitles[key];
        }
        throw new DialogError($"Weekend Results file has no entries for Week {week} with Result {result}.");
    }
}
