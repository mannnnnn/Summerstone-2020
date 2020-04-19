using System;
using System.Collections.Generic;
using UnityEngine;

public class RuneStats : MonoBehaviour
{
    public static RuneStats GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<RuneStats>();
    }

    Dictionary<int, RuneChance> chances = new Dictionary<int, RuneChance>();
    class RuneChance
    {
        public Card.Variant perfect;
        public Card.Type boost;
        public Dictionary<Card.Variant, float> chance;
    }
    string ToRuneName(string s)
    {
        s = s.Replace(" ", "").Replace("-", "");
        if (s == s.ToUpper())
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }
        return s;
    }

    void Awake()
    {
        string[] lines = Resources.Load<TextAsset>("Text/RuneStats").text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            chances[int.Parse(values[0].Trim())] = new RuneChance()
            {
                perfect = (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[1].Trim())),
                chance = new Dictionary<Card.Variant, float>()
                {
                    { (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[1].Trim())), 1.0f },
                    { (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[2].Trim())), 0.8f },
                    { (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[3].Trim())), 0.6f },
                    { (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[4].Trim())), 0.4f },
                    { (Card.Variant)Enum.Parse(typeof(Card.Variant), ToRuneName(values[5].Trim())), 0.2f },
                },
                boost = (Card.Type)Enum.Parse(typeof(Card.Type), ToRuneName(values[6].Trim())),
            };
        }
    }

    public float GetRuneChance(int week, Card card)
    {
        if (!chances.ContainsKey(week))
        {
            throw new DialogError($"Rune Stats file has no entries for Week {week}.");
        }
        float chance = 0f;
        RuneChance stats = chances[week];
        if (stats.perfect == card.variant)
        {
            chance = Mathf.Infinity;
        }
        if (stats.chance.ContainsKey(card.variant))
        {
            chance = Mathf.Max(chance, stats.chance[card.variant]);
        }
        if (stats.boost == card.type)
        {
            chance += 0.25f;
        }
        if (card.type == Card.Type.Luck)
        {
            chance = Mathf.Max(chance, 0.1f);
        }
        if (card.type == Card.Type.Patience)
        {
            chance += 0.05f * card.age;
        }
        return chance;
    }
}
