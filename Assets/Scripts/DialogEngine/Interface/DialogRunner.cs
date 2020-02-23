using System.Collections;
using System.Collections.Generic;
using static DialogUtils;
using UnityEngine;
using System.Linq;

public class DialogRunner : MonoBehaviour
{
    public static DialogRunner GetInstance()
    {
        return GameObject.FindGameObjectWithTag("DialogRunner")?.GetComponent<DialogRunner>();
    }

    // single source of truth for dialog flags
    public Dictionary<string, bool> Flags = new Dictionary<string, bool>();
    // currently running dialog
    Coroutine dialog;

    // true if dialog is currently active
    public static bool Active => GetInstance().dialog != null;

    // will advance dialog next frame
    bool advance = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Request a dialog advance.
    // Multiple requests in one frame will not advance dialog more than one.
    // Should not request advance unless the dialog UI is actually ready to advance.
    // For example, if a choice menu is up, don't call advance until the player has
    // picked a choice and the choice menu has disappeared.
    public static void RequestAdvance()
    {
        if (Active)
        {
            GetInstance().advance = true;
        }
    }

    // should never try to run dialog when one is already in progress.
    // check DialogRunner.Active before trying to run dialog.
    // for now, also throw an error
    public static void Run(IDialogUI ui, string path)
    {
        DialogRunner runner = GetInstance();
        if (runner.dialog != null)
        {
            throw new DialogError("Another dialog session is already open.");
        }
        runner.dialog = runner.StartCoroutine(runner.RunDialog(ui, path));
    }
    // should never have to be used
    public void StopDialog()
    {
        if (dialog != null)
        {
            StopCoroutine(dialog);
            dialog = null;
        }
    }
    // dialog coroutine
    IEnumerator RunDialog(IDialogUI ui, string path)
    {
        DialogFile f = new DialogFile(path, Flags);
        // wait a tick so that we update ourselves as active
        yield return null;
        // run dialog
        foreach (object o in f)
        {
            // check for events
            if (CheckStatement(o, "event"))
            {
                DialogEvents events = DialogEvents.GetInstance();
                if (events == null)
                {
                    throw new DialogError("DialogEvents GameObject does not exist or does not have 'DialogEvents' tag.");
                }
                events.HandleEvent(o);
            }
            // output to DialogUI
            else if (o is string)
            {
                ui.DisplayText(null, (string)o);
            }
            else if (CheckStatement(o, "show"))
            {
                Dictionary<string, object> args = GetStatementArgs(o, "show");
                if (args == null)
                {
                    throw new DialogError($"Invalid 'show' statement. 'show' statement must contain a map.\nYAML: {Reserialize(o)}");
                }
                if (!args.ContainsKey("id"))
                {
                    throw new DialogError($"Invalid 'show' statement. 'show' statement must contain an 'id' field.\nYAML: {Reserialize(o)}");
                }
                if (!args.ContainsKey("image"))
                {
                    throw new DialogError($"Invalid 'show' statement. 'show' statement must contain an 'image' field.\nYAML: {Reserialize(o)}");
                }
                ui.ShowCharacter((string)args["id"], (string)args["image"], new DialogImageOptions(o));
            }
            else if (CheckStatement(o, "hide"))
            {
                Dictionary<string, object> args = GetStatementArgs(o, "hide");
                if (args == null)
                {
                    throw new DialogError($"Invalid 'hide' statement. 'hide' statement must contain a map.\nYAML: {Reserialize(o)}");
                }
                if (!args.ContainsKey("id"))
                {
                    throw new DialogError($"Invalid 'hide' statement. 'hide' statement must contain an 'id' field.\nYAML: {Reserialize(o)}");
                }
                ui.HideCharacter((string)args["id"]);
            }
            // 1 item in dictionary means it is dialog such as "E: Very good code."
            else if (o is Dictionary<object, object> && ((Dictionary<object, object>)o).Count == 1)
            {
                Dictionary<string, object> args = CastArgs((Dictionary<object, object>)o);
                string chr = args.Keys.First();
                if (!f.Characters.ContainsKey(chr))
                {
                    throw new DialogError($"Invalid dialog statement. Character ID '{chr}' is not defined.\nYAML: {Reserialize(o)}");
                }
                object val = args[args.Keys.First()];
                // unknown statement type (error)
                if (!(val is string))
                {
                    throw new DialogError($"Unknown statement type: {Reserialize(o)}");
                }
                ui.DisplayText(f.Characters[chr].name, (string)val);
            }
            // unknown statement type (error)
            else
            {
                throw new DialogError($"Unknown statement type: {Reserialize(o)}");
            }
            // wait until RequestAdvance has been called
            while (!advance)
            {
                yield return null;
            }
            advance = false;
        }
        // at end of dialog, mark that dialog is finished
        ui.Finish();
        dialog = null;
    }
}
