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
        int week = Chimera.GetInstance().week + 1;
        Dictionary<string, string> reqs = FactionRequests.GetInstance().GetFactionRequests(week);
        ShowFlyUp(reqs[faction]);
        Chimera.GetInstance().faction = faction;
    }

    void ShowFlyUp(string message)
    {
        flyUpPanel.SetActive(true);
        flyUpPanelText.text = message;
    }
}
