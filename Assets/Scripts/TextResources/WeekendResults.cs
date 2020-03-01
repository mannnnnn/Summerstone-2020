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

    Dictionary<string, string> results = new Dictionary<string, string>();

    void Awake()
    {
        foreach (string line in Resources.Load<TextAsset>("Text/WeekendResults").text.Split('\n'))
        {
            var values = line.Split(new[] { ',' }, 2);
            results[values[0]] = values[1];
        }
    }

    public string GetWeekendResult(int week, string result)
    {
        string key = $"Weekend{week}{result}";
        if (results.ContainsKey(key))
        {
            return results[key];
        }
        throw new DialogError($"Weekend Results file has no entries for Week {week} with Result {result}.");
    }
}
