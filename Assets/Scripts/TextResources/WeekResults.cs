﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;

public class WeekResults : MonoBehaviour
{
    public static WeekResults GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<WeekResults>();
    }

    Dictionary<string, string> results = new Dictionary<string, string>();

    void Awake()
    {
        foreach (string line in Resources.Load<TextAsset>("Text/WeekResults").text.Split('\n'))
        {
            var values = line.Split(new[] { ',' }, 2);
            results[values[0]] = values[1];
        }
    }

    public string GetWeekResult(int week, string faction)
    {
        string key = $"{faction}Result{week}";
        if (results.ContainsKey(key))
        {
            return results[key];
        }
        throw new DialogError($"Week Results file has no entries for Faction {faction} and Week {week}.");
    }
}
