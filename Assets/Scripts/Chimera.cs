﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Chimera : MonoBehaviour
{
    [SerializeField]
    public enum MainGameState
    {
        Week, //Camera State 1
        FactionPick,
        WeekResult, //Camera State 2
        Mastermind,
        Weekend, //Camera State 3
        CardPick,
        WeekendResult //Camera State 4
    }
    private int gameStateSize = 6; //c# has a dumb way of handling this.

    public MainGameState currState = MainGameState.Week;
    [NonSerialized]
    public int week = 1;

    public GameObject chatScreen;
    public GameObject factionScreen;
    public GameObject resultsScreen;
    public GameObject cardChooserScreen;
    public GameObject mastermindScreen;
    public Animator MainCameraAnimator;

    public Material[] skyboxes = new Material[4];

    public WeekImageSwapper weekImageSwapper;

    public List<StoneCard> cards = new List<StoneCard>();

    // currently selected player input
    public Card card { get; set; }
    public string faction { get; set; }

    static System.Random random = new System.Random();

    Dictionary<string, int> factionChoices = new Dictionary<string, int>();
    void Awake()
    {
        // TODO: load data if it exists
        foreach (Faction f in Enum.GetValues(typeof(Faction)))
        {
            factionChoices[f.ToString()] = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        showGameStateScreenUI(currState);
        updateSkybox();
    }

    public static Chimera GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera").GetComponent<Chimera>();
    }

    public void nextGameState()
    {
        Save();
        // finish current game state
        switch (currState)
        {
            case MainGameState.Week:
                break;
            case MainGameState.FactionPick:
                // store faction choice
                if (!factionChoices.ContainsKey(faction))
                {
                    throw new InvalidOperationException($"Faction {faction} does not exist.");
                }
                factionChoices[faction]++;
                // add runes
                List<Card> newRunes = new List<Card>()
                {
                    Spellbook.RandomFromPool(faction),
                    Spellbook.RandomFromPool(faction),
                };
                resultsScreen.GetComponent<ResultScreen>().Set($"Week {week}", WeekResults.GetInstance().GetWeekResult(week, faction),
                    newRunes, new List<Card>() { });
                cards.AddRange(cardChooserScreen.GetComponent<RuneChoiceScreen>().AddRunes(newRunes));
                break;
            case MainGameState.WeekResult:
                break;
            case MainGameState.Mastermind:
                break;
            case MainGameState.Weekend:
                break;
            case MainGameState.CardPick:
                string result = "Fail";
                float chance = RuneStats.GetInstance().GetRuneChance(week, card);
                if (chance == Mathf.Infinity)
                {
                    result = "Perfect";
                }
                if (random.NextDouble() < chance)
                {
                    result = "Win";
                }
                // spend runes
                resultsScreen.GetComponent<ResultScreen>().Set($"Weekend {week}", WeekendResults.GetInstance().GetWeekendResult(week, result),
                new List<Card>() { },
                new List<Card>() { card });
                // TODO: process result
                break;
            case MainGameState.WeekendResult:
                break;
        }
        // go to next game state
        int nextState = (int)currState + 1;
        if(nextState > gameStateSize)
        {
            week++;
            nextState = 0;
        }
        currState = (MainGameState)nextState;
        showGameStateScreenUI(currState);
    }

    public void showGameStateScreenUI(MainGameState current)
    {
        // disable all
        chatScreen.SetActive(false);
        factionScreen.SetActive(false);
        cardChooserScreen.SetActive(false);
        mastermindScreen.SetActive(false);
        resultsScreen.SetActive(false);
        // enable active one
        GameObject screen = null;
        switch (current)
        {
            case MainGameState.Weekend:
                weekImageSwapper.updateImage(false, week);
                screen = chatScreen;
                break;
            case MainGameState.Week:
                weekImageSwapper.updateImage(true, week);
                screen = chatScreen;
                break;
            case MainGameState.FactionPick:
                screen = factionScreen;
                break;
            case MainGameState.CardPick:
                screen = cardChooserScreen;
                break;
            case MainGameState.Mastermind:
                screen = mastermindScreen;
                break;
            case MainGameState.WeekResult:
            case MainGameState.WeekendResult:
            default:
                screen = resultsScreen;
                break;
        }
        screen.SetActive(true);
        if (shouldMoveCamera())
        {
            updateSkybox();
            MainCameraAnimator.SetTrigger("MoveCamera");
        }
    }

    private bool shouldMoveCamera()
    {
        if (currState == MainGameState.Week) return true;
        
        if (currState == MainGameState.WeekResult) return true;

        if (currState == MainGameState.Weekend) return true;

        if (currState == MainGameState.WeekendResult) return true;

        return false;
    }

    private void updateSkybox()
    {
        int nextSkybox = 0;
        switch (currState)
        {
            case MainGameState.Week: nextSkybox = 0; break;
            case MainGameState.WeekResult: nextSkybox = 1; break;
            case MainGameState.Weekend: nextSkybox = 2; break;
            case MainGameState.WeekendResult: 
            default: nextSkybox = 3; break;
        }
        RenderSettings.skybox = skyboxes[nextSkybox];
    }

    // save to playerprefs
    public void Save()
    {
        PlayerPrefs.SetInt("save", 1);
        string cards = string.Join(";", this.cards.Select(x => x.ToSaveString()));
        PlayerPrefs.SetString("cards", cards);
        PlayerPrefs.SetInt("week", week);
        foreach (Faction f in Enum.GetValues(typeof(Faction)))
        {
            PlayerPrefs.SetInt($"factionChoices{f}", factionChoices[f.ToString()]);
        }
        PlayerPrefs.SetString("state", currState.ToString());
        PlayerPrefs.SetString("faction", faction);
        PlayerPrefs.SetString("card", "");
        if (card != null)
        {
            PlayerPrefs.SetString("card", card.ToSaveString());
        }
        PlayerPrefs.Save();
    }
    // load from playerprefs
    public void Load()
    {
        if (!CanLoad)
        {
            throw new InvalidOperationException("There is no save data.");
        }
        // set stone cards
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i].gameObject);
            cards.RemoveAt(i);
            i--;
        }
        string[] cardStrs = PlayerPrefs.GetString("cards").Split(';');
        if (cardStrs[0] != "")
        {
            List<Card> dummies = new List<Card>();
            for (int i = 0; i < cardStrs.Length; i++)
            {
                dummies.Add(new Card(Card.Variant.Time));
            }
            cards.AddRange(cardChooserScreen.GetComponent<RuneChoiceScreen>().AddRunes(dummies));
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].FromSaveString(cardStrs[i]);
            }
        }
        // set game state
        week = PlayerPrefs.GetInt("week");
        currState = (MainGameState)Enum.Parse(typeof(MainGameState), PlayerPrefs.GetString("state"));
        // set faction choices
        foreach (Faction f in Enum.GetValues(typeof(Faction)))
        {
            factionChoices[f.ToString()] = PlayerPrefs.GetInt($"factionChoices{f}");
        }
        // set selected card or faction
        faction = PlayerPrefs.GetString("faction");
        card = null;
        if (PlayerPrefs.GetString("card") != "")
        {
            card = new Card(Card.Variant.Time, 0f);
            card.FromSaveString(PlayerPrefs.GetString("card"));
        }
    }
    // check for loadability
    public bool CanLoad => PlayerPrefs.HasKey("save");

    // Update is called once per frame
    void Update()
    {
        // attempt load
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
        {
            Load();
            nextGameState();
        }
        // print all saved values
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"save: {PlayerPrefs.GetInt("save")}");
            Debug.Log($"cards: {PlayerPrefs.GetString("cards")}");
            Debug.Log($"week: {PlayerPrefs.GetInt("week")}");
            string s = "Choices: ";
            foreach (Faction f in Enum.GetValues(typeof(Faction)))
            {
                string k = $"factionChoices{f}";
                s += $"{f}: {PlayerPrefs.GetInt(k)}, ";
            }
            Debug.Log(s);
            Debug.Log($"state: {PlayerPrefs.GetString("state")}");
            Debug.Log($"faction: {PlayerPrefs.GetString("faction")}");
            Debug.Log($"card: {PlayerPrefs.GetString("card")}");
        }
    }
}



