using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogUtils;

// used to specify specific options when drawing a character.
// add more options as needed.
public class DialogImageOptions
{
    public enum Side
    {
        NULL, RIGHT, LEFT
    }
    public enum Animation
    {
        NULL, FADE, SLIDE_LEFT, SLIDE_RIGHT, SLIDE_UP, SLIDE_DOWN
    }

    // image defaults
    public Side side = Side.RIGHT;
    public Animation animation = Animation.FADE;

    public DialogImageOptions(object o)
    {
        Dictionary<string, object> args = GetStatementArgs(o, "show");
        // parse side argument
        if (args.ContainsKey("side"))
        {
            Enum.TryParse(((string)args["side"]).ToUpper(), out Side val);
            if (val == Side.NULL)
            {
                throw new DialogError($"Invalid image option: 'side: {(string)args["side"]}'.\nYAML: {Reserialize(o)}");
            }
            side = val;
        }
        // parse animation argument
        if (args.ContainsKey("anim"))
        {
            Enum.TryParse(((string)args["anim"]).ToUpper(), out Animation val);
            if (val == Animation.NULL)
            {
                throw new DialogError($"Invalid image option: 'anim: {(string)args["anim"]}'.\nYAML: {Reserialize(o)}");
            }
            animation = val;
        }
    }

    public override string ToString()
    {
        return $"DialogImageOptions {{ side: {side}, anim: {animation} }}";
    }
}
