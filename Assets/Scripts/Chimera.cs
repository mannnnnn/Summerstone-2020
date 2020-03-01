using System;
using System.Collections;
using System.Collections.Generic;
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
    private int gameStateSize = 5; //c# has a dumb way of handling this.

    public MainGameState currState = MainGameState.Week;
    [NonSerialized]
    public int week = 1;

    private GameObject currScreen;

    public GameObject chatScreen;
    public GameObject factionScreen;
    public GameObject resultsScreen;
    public GameObject cardChooserScreen;
    public GameObject mastermindScreen;
    public Animator MainCameraAnimator;

    public Material[] skyboxes = new Material[4];

    public WeekImageSwapper weekImageSwapper;

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
        if (chatScreen.activeSelf)
        {
            weekImageSwapper.updateImage(currState == MainGameState.Week, week);
            currScreen = chatScreen;
        }
        if (factionScreen.activeSelf) currScreen = factionScreen;
        if (resultsScreen.activeSelf)
        {
            currScreen = resultsScreen;
        }
        if (cardChooserScreen.activeSelf) currScreen = cardChooserScreen;
        if (mastermindScreen.activeSelf) currScreen = mastermindScreen;

        updateSkybox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Chimera GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera").GetComponent<Chimera>();
    }

    public void nextGameState()
    {
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
                cardChooserScreen.GetComponent<RuneChoiceScreen>().AddRunes(newRunes);
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
        showGameStateScreenUI();
    }

    public void showGameStateScreenUI()
    {
        GameObject nextScreen = null;
        switch (currState)
        {
            case MainGameState.Weekend:
                weekImageSwapper.updateImage(false, week);
                if (!chatScreen.activeSelf) nextScreen = chatScreen; break;
            case MainGameState.Week:
                weekImageSwapper.updateImage(true, week);
                if (!chatScreen.activeSelf) nextScreen = chatScreen; break;
            case MainGameState.FactionPick: if (!factionScreen.activeSelf) nextScreen = factionScreen; break;
            case MainGameState.CardPick: if (!cardChooserScreen.activeSelf) nextScreen = cardChooserScreen; break;
            case MainGameState.Mastermind: if (!mastermindScreen.activeSelf) nextScreen = mastermindScreen; break;
            case MainGameState.WeekResult:
            case MainGameState.WeekendResult:
            default: if (!resultsScreen.activeSelf) nextScreen = resultsScreen; break;
        }

        currScreen.SetActive(false);
        nextScreen.SetActive(true);
        currScreen = nextScreen;


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

}



