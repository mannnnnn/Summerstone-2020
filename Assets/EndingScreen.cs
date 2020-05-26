using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Call the Set method in this class from Chimera.nextGameState before showing this screen
// to specify its contents.
public class EndingScreen : MonoBehaviour
{
    public Text result;
    Image endingCard;
    public GameObject clearSave;

    public Sprite[] endingCardImages;
    int cardNum = 0;

    public GameObject helpPanel;
    public GameObject helpBtn;
    bool helpShowing = false;

    public void toggleHelp()
    {

        if (helpShowing)
        {
            helpPanel.GetComponent<Animator>().SetTrigger("Out");
        }
        else if (helpPanel.active)
        {
            helpPanel.GetComponent<Animator>().SetTrigger("In");
        }

        if (!helpPanel.active)
        {
            helpPanel.SetActive(true);
        }

        helpShowing = !helpShowing;
    }

    public void Set(string result, int cardNum)
    {
        helpBtn.SetActive(true);
        this.result.text = result;
        this.cardNum = cardNum;
       // endingCard.sprite = endingCardImages[cardNum];
    }

    public void OnDisable()
    {
        clearSave.SetActive(false);
        helpBtn.SetActive(false);
    }

    public void OnEnable()
    {
        clearSave.SetActive(true);
        helpBtn.SetActive(true);
    }

}
