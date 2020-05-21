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

    public Sprite[] endingCardImages;
    int cardNum = 0;

    public void Set(string result, int cardNum)
    {
        this.result.text = result;
        this.cardNum = cardNum;
       // endingCard.sprite = endingCardImages[cardNum];
    }

}
