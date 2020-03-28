using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Mastermind;

public class DisplayButton : MonoBehaviour
{
    public MastermindColor mastermindColor { get; private set; }
    Image image;
    Mastermind mastermind;

    // Start is called before the first frame update
    void Start()
    {
        mastermind = Chimera.GetInstance().mastermindScreen.GetComponent<Mastermind>();
        image = transform.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetResultColor(MastermindColor c)
    {
        image.GetComponent<Animator>().SetTrigger("reveal");
        mastermindColor = c;
        image.sprite = c.image;
    }

    public void displayRed(){
        image.GetComponent<Animator>().SetTrigger("reveal");
        MastermindColor c = mastermind.displayColors[0];
      SetResultColor(c);
    }
    public void displayWhite(){
        image.GetComponent<Animator>().SetTrigger("reveal");
        MastermindColor c = mastermind.displayColors[1];
      SetResultColor(c);
    }
    public void displayNull(){
        image.GetComponent<Animator>().SetTrigger("reveal");
        MastermindColor c = mastermind.displayColors[2];
      SetResultColor(c);
    }
}
