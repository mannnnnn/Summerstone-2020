using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DialogUtils;

public class FactionRequests : MonoBehaviour
{
    enum Faction
    {
        Oxx, Day, Wolf, Deer, Night, Bark
    }

    public static FactionRequests GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<FactionRequests>();
    }

    Dictionary<string, string> requests = new Dictionary<string, string>();

    void Awake()
    {
        using (var reader = new StreamReader("Assets/Scripts/Dialogue/FactionRequests.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(new[] { ',' }, 2);
                requests[values[0]] = values[1];
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
