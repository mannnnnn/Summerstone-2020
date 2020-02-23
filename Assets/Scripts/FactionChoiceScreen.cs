using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionChoiceScreen : MonoBehaviour
{
    public GameObject flyUpPanel;
    public Text flyUpPanelText;

    public string Faction { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
        // TODO: fill in week
        int week = 1;
        Dictionary<string, string> reqs = FactionRequests.GetInstance().GetFactionRequests(week);
        ShowFlyUp(reqs[faction]);
        Faction = faction;
    }

    void ShowFlyUp(string message)
    {
        flyUpPanel.SetActive(true);
        flyUpPanelText.text = message;
    }
}
