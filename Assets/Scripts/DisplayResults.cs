using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Mastermind;

public class DisplayResults : MonoBehaviour
{
    // Start is called before the first frame update
    public DisplayButton[] displayButtons;

    public void switchDisplays(MastermindResult result){
      int currentCount = 0;
      for(int i = 0; i< result.red; i++){
        DisplayButton displayButtonA = displayButtons[i];
        displayButtonA.GetComponent<DisplayButton>().displayRed();
        currentCount++;
      }
      for (int j = 0; j<result.white; j++){
        DisplayButton displayButtonB = displayButtons[result.red + j];
        displayButtonB.GetComponent<DisplayButton>().displayWhite();
        currentCount++;
      }
    }
}
