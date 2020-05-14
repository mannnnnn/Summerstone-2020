using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Pulls from Assets/Scripts/Dialogue/FactionRequests.csv,
// and displays the faction messages for that week.
// To edit the messages, edit FactionRequests.csv.
public class FactionChoiceScreen : MonoBehaviour
{
    public GameObject flyUpPanel;
    public Text flyUpPanelText;

    public Image summerStone;
    public Sprite regularStone;
    public Sprite brokenStone;

    public GameObject wolfOrb;
    public GameObject deerOrb;
    public GameObject sunOrb;
    public GameObject nightOrb;
    public GameObject barkOrb;
    public GameObject oxOrb;

    public Sprite[] factionSprites;
    public Image portrait;
    public GameObject nextBtn;

    // Start is called before the first frame update
    void OnEnable()
    {
        float week = Chimera.GetInstance().week;
        flyUpPanel.SetActive(false);
        nextBtn.SetActive(false);

        if (week == 1)
        {
            sunOrb.SetActive(false);
            nightOrb.SetActive(false);
            barkOrb.SetActive(false);
            oxOrb.SetActive(false);
        } else if(week == 2)
        {
            sunOrb.SetActive(true);
            nightOrb.SetActive(true);
            barkOrb.SetActive(false);
            oxOrb.SetActive(false);
        } else if (week == 8)
        {
            summerStone.sprite = brokenStone;
            sunOrb.SetActive(false);
            nightOrb.SetActive(false);
        } else if (week == 10)
        {
            deerOrb.SetActive(false);
            sunOrb.SetActive(false);
            nightOrb.SetActive(false);
            barkOrb.SetActive(false);
            oxOrb.SetActive(false);
        }
         else {
            summerStone.sprite = regularStone;
            wolfOrb.SetActive(true);
            deerOrb.SetActive(true);
            sunOrb.SetActive(true);
            nightOrb.SetActive(true);
            barkOrb.SetActive(true);
            oxOrb.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        flyUpPanel.SetActive(false);
    }

    // displays fly up screen
    public void ClickFactionButton(string faction)
    {
        flyUpPanel.SetActive(false);
        int week = Chimera.GetInstance().week;
        Dictionary<string, string> reqs = FactionRequests.GetInstance().GetFactionRequests(week);
        ShowFlyUp(reqs[faction], faction);
        Chimera.GetInstance().faction = faction;
    }

    void ShowFlyUp(string message, string faction)
    {
        flyUpPanel.SetActive(true);


        switch (faction)
        {
            case "Oxx": portrait.sprite = factionSprites[0]; break;
            case "Day": portrait.sprite = factionSprites[1]; break;
            case "Wolf": portrait.sprite = factionSprites[2]; break;
            case "Deer": portrait.sprite = factionSprites[3]; break;
            case "Night": portrait.sprite = factionSprites[4]; break;
            case "Bark": portrait.sprite = factionSprites[5]; break;
            default: break;
        }


        flyUpPanelText.text = message;
        nextBtn.SetActive(true);
    }
}
