using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Mastermind;
using UnityEngine.UI;

public class MastermindRow : MonoBehaviour
{
    public ChoiceButton[] buttons;
    public Button submitButton;
    public DisplayResults displayResults;

    Mastermind mastermind;

    // Start is called before the first frame update
    void Start()
    {
        mastermind = Chimera.GetInstance().mastermindScreen.GetComponent<Mastermind>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // user submits row
    public void Submit()
    {
        // lock buttons
        Lock();

        submitButton.gameObject.SetActive(false);
        // get row colors
        MastermindColor[] guess = buttons.Select(button => button.mastermindColor).ToArray();
        // submit
        MastermindResult result = mastermind.Test(guess);
        // TODO: do something at end of game
        if (!mastermind.Success() && !mastermind.Failure())
        {
            displayResults.GetComponent<DisplayResults>().switchDisplays(result);
            //Debug.Log($"Red: {result.red}, White: {result.white}");
            // create next row
            mastermind.AddRow();
        } else
        {
            displayResults.GetComponent<DisplayResults>().switchDisplays(result);
        }
    }

    public void Lock()
    {
        submitButton.interactable = false;
        buttons.ToList().ForEach(button => button.Lock());
    }
}
