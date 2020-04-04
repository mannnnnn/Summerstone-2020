using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public Type type = Type.Mortal;
    public SubType subType = SubType.Work;
    public Variant variant = Variant.Build;
    public float age = 0;

    public Card(Variant variant, float age = 0f)
    {
        this.variant = variant;
        subType = Spellbook.getCardSubType(variant);
        type = Spellbook.getCardType(subType);
        this.age = age;
    }

    public Card Copy()
    {
        return new Card(variant, age);
    }

    public static System.Random random = new System.Random();
    public static Card Random()
    {
        Variant variant = (Variant)random.Next(0, 31);
        return new Card(variant);
    }

    public string ToSaveString()
    {
        return $"{variant}:{age}";
    }
    public void FromSaveString(string s)
    {
        var values = s.Split(':');
        variant = (Variant)Enum.Parse(typeof(Variant), values[0]);
        subType = Spellbook.getCardSubType(variant);
        type = Spellbook.getCardType(subType);
        age = float.Parse(values[1]);
    }

    public override string ToString()
    {
        return $"[{type}->{subType}->{variant}, Age {age}]";
    }

    [SerializeField]
    public enum Type
    {
        Mortal,
        Magic,
        Patience,
        Luck
    }

    [SerializeField]
    public enum SubType
    {
        Work,
        BandTogether,
        Alter,
        RiseAbove,
        Understand,
        Master,
        Guess,
        Friends,
    }

    [SerializeField]
    public enum Variant
    {
        Build,
        Delegate,
        Assist,
        Fight,
        Legion,
        Workforce,
        SearchParty,
        Inclusion,
        Reality,
        Time,
        Outcomes,
        Creature,
        Sorcery,
        Leader,
        AllKnowing,
        Mystery,
        Seek,
        Examine,
        Remember,
        Observe,
        Weaponry,
        Stealth,
        Words,
        Tricks,
        History,
        Rumors,
        Readings,
        Experience,
        Canines,
        Vermin,
        Commoners,
        Leaders,
    }
}

public class Spellbook : MonoBehaviour
{
    public static Spellbook GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera").GetComponent<Spellbook>();
    }

    //rune images
    public Sprite work1;
    public Sprite work2;
    public Sprite work3;
    public Sprite work4;
    public Sprite work5;
    public Sprite work6;
    public Sprite work7;
    public Sprite work8;

    public Sprite magic1;
    public Sprite magic2;
    public Sprite magic3;
    public Sprite magic4;
    public Sprite magic5;
    public Sprite magic6;
    public Sprite magic7;
    public Sprite magic8;

    public Sprite time1;
    public Sprite time2;
    public Sprite time3;
    public Sprite time4;
    public Sprite time5;
    public Sprite time6;
    public Sprite time7;
    public Sprite time8;

    public Sprite luck1;
    public Sprite luck2;
    public Sprite luck3;
    public Sprite luck4;
    public Sprite luck5;
    public Sprite luck6;
    public Sprite luck7;
    public Sprite luck8;

    // set up faction pools
    static Dictionary<Faction, List<Card.Variant>> pools = new Dictionary<Faction, List<Card.Variant>>();
    static System.Random random = new System.Random();
    static Spellbook()
    {
        foreach (Faction f in Enum.GetValues(typeof(Faction)))
        {
            pools[f] = new List<Card.Variant>();
        }
        foreach (Card.Variant v in Enum.GetValues(typeof(Card.Variant)))
        {
            switch (getCardType(getCardSubType(v)))
            {
                case Card.Type.Mortal:
                    pools[Faction.Wolf].Add(v);
                    pools[Faction.Bark].Add(v);
                    break;
                case Card.Type.Magic:
                    pools[Faction.Deer].Add(v);
                    pools[Faction.Oxx].Add(v);
                    break;
                case Card.Type.Patience:
                    pools[Faction.Night].Add(v);
                    pools[Faction.Oxx].Add(v);
                    break;
                case Card.Type.Luck:
                    pools[Faction.Day].Add(v);
                    pools[Faction.Bark].Add(v);
                    break;
            }
            pools[Faction.Neutral].Add(v);
        }
    }

    // get a random card from a faction pool
    public static Card RandomFromPool(string faction)
    {
        Faction f = (Faction)Enum.Parse(typeof(Faction), faction);
        if (!pools.ContainsKey(f))
        {
            throw new InvalidOperationException($"There is no card pool for Faction {faction}");
        }
        return new Card(pools[f][random.Next(pools[f].Count)]);
    }


    public static Card RandomCard()
    {

        Card.Variant[] enums = (Card.Variant[])Enum.GetValues(typeof(Card.Variant));
        return new Card(enums[random.Next(enums.Length-1)]);
    }

    public static Card.Type getCardType(Card.SubType type)
    {
        switch (type)
        {
            case Card.SubType.Work:
            case Card.SubType.BandTogether:
                return Card.Type.Mortal;

            case Card.SubType.Alter:
            case Card.SubType.RiseAbove:
                return Card.Type.Magic;

            case Card.SubType.Understand:
            case Card.SubType.Master:
                return Card.Type.Patience;

            case Card.SubType.Guess:
            case Card.SubType.Friends:
                return Card.Type.Luck;

            default:
                Debug.Log("Could not retrieve card type.");
                return Card.Type.Mortal;
        }
    }

    public static List<Card.SubType> getCardSubtypes(Card.Type type)
    {
        List<Card.SubType> typeList = new List<Card.SubType>();

        switch (type)
        {
            case Card.Type.Mortal:
                typeList.Add(Card.SubType.Work);
                typeList.Add(Card.SubType.BandTogether);
                break;
            case Card.Type.Magic:
                typeList.Add(Card.SubType.Alter);
                typeList.Add(Card.SubType.RiseAbove);
                break;
            case Card.Type.Patience:
                typeList.Add(Card.SubType.Understand);
                typeList.Add(Card.SubType.Master);
                break;
            case Card.Type.Luck:
                typeList.Add(Card.SubType.Guess);
                typeList.Add(Card.SubType.Friends);
                break;
        }

        if (typeList.Count <= 0)
        {
            Debug.Log("Could not retrieve card subtypes.");
        }

        return typeList;
    }

    public static List<Card.Variant> getCardVariants(Card.SubType type)
    {
        List<Card.Variant> typeList = new List<Card.Variant>();

        switch (type)
        {
            case Card.SubType.Work:
                typeList.Add(Card.Variant.Build);
                typeList.Add(Card.Variant.Delegate);
                typeList.Add(Card.Variant.Assist);
                typeList.Add(Card.Variant.Fight);
                break;

            case Card.SubType.BandTogether:
                typeList.Add(Card.Variant.Legion);
                typeList.Add(Card.Variant.Workforce);
                typeList.Add(Card.Variant.SearchParty);
                typeList.Add(Card.Variant.Inclusion);
                break;

            case Card.SubType.Alter:
                typeList.Add(Card.Variant.Reality);
                typeList.Add(Card.Variant.Time);
                typeList.Add(Card.Variant.Outcomes);
                typeList.Add(Card.Variant.Creature);
                break;

            case Card.SubType.RiseAbove:
                typeList.Add(Card.Variant.Sorcery);
                typeList.Add(Card.Variant.Leader);
                typeList.Add(Card.Variant.AllKnowing);
                typeList.Add(Card.Variant.Mystery);
                break;

            case Card.SubType.Understand:
                typeList.Add(Card.Variant.Seek);
                typeList.Add(Card.Variant.Examine);
                typeList.Add(Card.Variant.Remember);
                typeList.Add(Card.Variant.Observe);
                break;

            case Card.SubType.Master:
                typeList.Add(Card.Variant.Weaponry);
                typeList.Add(Card.Variant.Stealth);
                typeList.Add(Card.Variant.Words);
                typeList.Add(Card.Variant.Tricks);
                break;

            case Card.SubType.Guess:
                typeList.Add(Card.Variant.History);
                typeList.Add(Card.Variant.Rumors);
                typeList.Add(Card.Variant.Readings);
                typeList.Add(Card.Variant.Experience);
                break;

            case Card.SubType.Friends:
                typeList.Add(Card.Variant.Canines);
                typeList.Add(Card.Variant.Vermin);
                typeList.Add(Card.Variant.Commoners);
                typeList.Add(Card.Variant.Leaders);
                break;
        }

        if (typeList.Count <= 0)
        {
            Debug.Log("Could not retrieve card variants.");
        }

        return typeList;
    }

    public static Card.SubType getCardSubType(Card.Variant type)
    {
        switch (type)
        {
            case Card.Variant.Build:
            case Card.Variant.Delegate:
            case Card.Variant.Assist:
            case Card.Variant.Fight:
                return Card.SubType.Work;

            case Card.Variant.Legion:
            case Card.Variant.Workforce:
            case Card.Variant.SearchParty:
            case Card.Variant.Inclusion:
                return Card.SubType.BandTogether;

            case Card.Variant.Reality:
            case Card.Variant.Time:
            case Card.Variant.Outcomes:
            case Card.Variant.Creature:
                return Card.SubType.Alter;

            case Card.Variant.Sorcery:
            case Card.Variant.Leader:
            case Card.Variant.AllKnowing:
            case Card.Variant.Mystery:
                return Card.SubType.RiseAbove;

            case Card.Variant.Seek:
            case Card.Variant.Examine:
            case Card.Variant.Remember:
            case Card.Variant.Observe:
                return Card.SubType.Understand;

            case Card.Variant.Weaponry:
            case Card.Variant.Stealth:
            case Card.Variant.Words:
            case Card.Variant.Tricks:
                return Card.SubType.Master;

            case Card.Variant.History:
            case Card.Variant.Rumors:
            case Card.Variant.Readings:
            case Card.Variant.Experience:
                return Card.SubType.Guess;

            case Card.Variant.Canines:
            case Card.Variant.Vermin:
            case Card.Variant.Commoners:
            case Card.Variant.Leaders:
                return Card.SubType.Friends;

            default:
                Debug.Log("Could not retrieve card subtype.");
                return Card.SubType.Work;
        }
    }

    public Sprite getCardArt(Card.Variant type)
    {
        switch (type)
        {
            case Card.Variant.Build: return work1;
            case Card.Variant.Delegate: return work2;
            case Card.Variant.Assist: return work3;
            case Card.Variant.Fight: return work4;
            case Card.Variant.Legion: return work5;
            case Card.Variant.Workforce: return work6;
            case Card.Variant.SearchParty: return work7;
            case Card.Variant.Inclusion: return work8;
            case Card.Variant.Reality: return magic1;
            case Card.Variant.Time: return magic2;
            case Card.Variant.Outcomes: return magic3;
            case Card.Variant.Creature: return magic4;
            case Card.Variant.Sorcery: return magic5;
            case Card.Variant.Leader: return magic6;
            case Card.Variant.AllKnowing: return magic7;
            case Card.Variant.Mystery: return magic8;
            case Card.Variant.Seek: return time1;
            case Card.Variant.Examine: return time2;
            case Card.Variant.Remember: return time3;
            case Card.Variant.Observe: return time4;
            case Card.Variant.Weaponry: return time5;
            case Card.Variant.Stealth: return time6;
            case Card.Variant.Words: return time7;
            case Card.Variant.Tricks: return time8;
            case Card.Variant.History: return luck1;
            case Card.Variant.Rumors: return luck2;
            case Card.Variant.Readings: return luck3;
            case Card.Variant.Experience: return luck4;
            case Card.Variant.Canines: return luck5;
            case Card.Variant.Vermin: return luck6;
            case Card.Variant.Commoners: return luck7;
            case Card.Variant.Leaders: return luck8;
            default: return work1;
        }
    }
}
