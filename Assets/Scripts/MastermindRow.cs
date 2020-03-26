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
    public Button displayButton;

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
            displayButton.GetComponent<DisplayButton>().displaySuccess();
            Debug.Log("Success!");
        }
        else if (mastermind.Failure())
        {
            displayButton.GetComponent<DisplayButton>().displayFailure();
            Debug.Log("Failure!");
        }
        // TODO: display result
        else
        {
            if(result.red> 0){
              displayButton.GetComponent<DisplayButton>().displayCorrectColorSpace();
            }
            else if (result.white > 0){
              displayButton.GetComponent<DisplayButton>().displayCorrectColor();
            }
            else{
              displayButton.GetComponent<DisplayButton>().displayIncorrect();
            }
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
