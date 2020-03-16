using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

                break;
            // stab sound effect and red flash
            case "Week1_stab":

                break;
        }
    }

    IEnumerator Week1Intro()
    {
        yield break;
    }

    IEnumerator Week1Stab()
    {
        yield break;
    }
}
