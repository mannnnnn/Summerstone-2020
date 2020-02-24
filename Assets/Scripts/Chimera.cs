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
        Weekend, //Camera State 3
        CardPick,
        WeekendResult //Camera State 4
    }
    private int gameStateSize = 5; //c# has a dumb way of handling this.

    public MainGameState currState = MainGameState.Week;
    public int week = 0;

    private GameObject currScreen;

    public GameObject chatScreen;
    public GameObject factionScreen;
    public GameObject resultsScreen;
    public GameObject cardChooserScreen;
    public Animator MainCameraAnimator;

    public Material[] skyboxes = new Material[4];

    // currently selected player input
    public Card card { get; set; }
    public string faction { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (chatScreen.activeSelf) currScreen = chatScreen;
        if (factionScreen.activeSelf) currScreen = factionScreen;
        if (resultsScreen.activeSelf) currScreen = resultsScreen;
        if (cardChooserScreen.activeSelf) currScreen = cardChooserScreen;

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
        int nextState = (int)currState + 1;
        if(nextState > gameStateSize)
        {
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
            case MainGameState.Week: if (!chatScreen.activeSelf) nextScreen = chatScreen; break;
            case MainGameState.FactionPick: if (!factionScreen.activeSelf) nextScreen = factionScreen; break;
            case MainGameState.CardPick: if (!cardChooserScreen.activeSelf) nextScreen = cardChooserScreen; break;
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



