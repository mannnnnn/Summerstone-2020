using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Mastermind;

public class ChoiceButton : MonoBehaviour
{
    public MastermindColor mastermindColor { get; private set; }
    Image image;
    Button button;
    Mastermind mastermind;
    public bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        mastermind = Chimera.GetInstance().mastermindScreen.GetComponent<Mastermind>();
        image = transform.Find("Image").GetComponent<Image>();
        button = GetComponent<Button>();
        SetMastermindColor(mastermind.colors[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMastermindColor(MastermindColor c)
    {
        mastermindColor = c;
        image.sprite = c.image;
    }


    // go to next color
    public void Next()
    {
        if (!locked)
        {
            MastermindColor c = mastermind.colors[(mastermindColor.index + 1) % mastermind.colors.Count];
            SetMastermindColor(c);
        }
    }

    public void Lock()
    {
        locked = true;
        button.interactable = false;
    }
}
