using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dialog engine calls these methods to direct the dialog UI
// for example:
//
// - Good code.
// - E: Very good code.
// - show:
//     id: E
//     image: evans_finger_guns
//     side: right
//     anim: fade
//
// leads to the following:
//
// DisplayText(null, "Good code.");
// DisplayText("Evans", "Very good code.");
// ShowCharacter("E", "evans_finger_guns", new DialogImageOptions { side = Side.RIGHT, animation = Animation.FADE });
public interface IDialogUI
{
    // name can be null for no name
    void DisplayText(string name, string text);

    // display image for a character
    void ShowCharacter(string id, string image, DialogImageOptions options);

    // hide image for a character
    void HideCharacter(string id);

    // dialog is done
    void Finish();
}
