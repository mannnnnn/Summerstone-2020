using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DialogUtils;

// dialog events are sections of code that can be
// run by the dialog system using:
//
// - event:
//     id: introDisplay
//     other_arg_here: value
public class DialogEvents : MonoBehaviour
{
    public static DialogEvents GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera")?.GetComponent<DialogEvents>();
    }

    Dictionary<string, bool> flags;
    // Start is called before the first frame update
    void Start()
    {
        flags = DialogRunner.GetInstance().Flags;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // put logic for dialog events here.
    public void HandleEvent(object o)
    {
        Dictionary<string, object> args = GetStatementArgs(o, "event");
        // invalid event
        if (!args.ContainsKey("id"))
        {
            throw new DialogError($"Event has no 'id' field.\nYAML: {Reserialize(o)}");
        }
        // handle events
        switch(args["id"])
        {
            // Prints out to the Unity debug console.
            // Usage:
            // - event:
            //     id: print
            //     value: some_text_here
            case "print":
                if (args.ContainsKey("value"))
                {
                    Debug.Log(args["value"]);
                }
                break;
            // kicks off the intro animation
            case "Week1_intro":
                StartCoroutine(Week1Intro());
                break;
            // stab sound effect and red flash
            case "Week1_stab":
                StartCoroutine(Week1Stab());
                break;
        }
    }

    IEnumerator Week1Intro()
    {
        Chimera chimera = Chimera.GetInstance();
        Overlay overlay = chimera.overlay.GetComponent<Overlay>();
        DialogUI dialog = chimera.chatScreen.GetComponent<DialogUI>();
        Animator cam = Camera.main.GetComponent<Animator>();
        SoundEffects sfx = SoundEffects.GetInstance();
        // hide dialogue screen
        dialog.SetVisible(false);
        overlay.Set(Color.clear, 0f);
        // camera walking and attack animation
        cam.SetTrigger("CameraAttack");
        sfx.PlaySound("intro");
        yield return new WaitForSeconds(3.5f);
        yield return overlay.Set(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        dialog.SetVisible(true);
        overlay.Set(Color.clear, 2f);
        yield return new WaitForSeconds(0.5f);
        dialog.Next();
        yield break;
    }

    IEnumerator Week1Stab()
    {
        Chimera chimera = Chimera.GetInstance();
        Overlay overlay = chimera.overlay.GetComponent<Overlay>();
        SoundEffects sfx = SoundEffects.GetInstance();
        DialogUI dialog = chimera.chatScreen.GetComponent<DialogUI>();
        sfx.PlaySound("stab");
        yield return new WaitForSeconds(0.1f);
        yield return overlay.Set(Color.red, 0.05f);
        yield return new WaitForSeconds(0.5f);
        yield return overlay.Set(Color.black, 1f);
        dialog.Clear();
        yield return new WaitForSeconds(1f);
        overlay.Set(Color.clear, 2f);
        yield return new WaitForSeconds(0.5f);
        dialog.Next();
        yield break;
    }
}
