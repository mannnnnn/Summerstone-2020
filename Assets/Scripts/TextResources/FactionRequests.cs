using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;

public enum Faction
{
    Oxx, Day, Wolf, Deer, Night, Bark, Neutral
}

public class FactionRequests : MonoBehaviour
{
    public static FactionRequests GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<FactionRequests>();
    }

    Dictionary<string, string> requests = new Dictionary<string, string>();

    void Awake()
    {
        foreach (string line in Resources.Load<TextAsset>("Text/FactionRequests").text.Split('\n'))
        {
            var values = line.Split(new[] { ',' }, 2);

            if (values[0] != null && values[0].Trim() != "")
            {
                requests[values[0].Trim()] = values[1].Trim();
            }
        }
    }

    public Dictionary<string, string> GetFactionRequests(int week)
    {
        Dictionary<string, string> req = new Dictionary<string, string>();
        foreach (Faction f in Enum.GetValues(typeof(Faction)))
        {
            string key = $"{f}Request{week}";
            if (requests.ContainsKey(key))
            {
                req[$"{f}"] = requests[key];
            }
        }
        if (req.Count == 0)
        {
            throw new DialogError($"Faction Requests file has no entries for Week {week}.");
        }
        return req;
    }
}
