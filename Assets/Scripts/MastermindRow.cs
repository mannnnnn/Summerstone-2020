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
        // get row colors
        MastermindColor[] guess = buttons.Select(button => button.mastermindColor).ToArray();
        // submit
        MastermindResult result = mastermind.Test(guess);
        Debug.Log("Results " + result);
        // TODO: do something at end of game
        if (mastermind.Success())
        {
            Debug.Log("Success!");
        }
        else if (mastermind.Failure())
        {
            Debug.Log("Failure!");
        }
        // TODO: display result
        else
        {
            displayResults.GetComponent<DisplayResults>().switchDisplays(result);
            Debug.Log($"Red: {result.red}, White: {result.white}");
            // create next row
            mastermind.AddRow();
        }
    }

    public void Lock()
    {
        submitButton.interactable = false;
        buttons.ToList().ForEach(button => button.Lock());
    }
}
