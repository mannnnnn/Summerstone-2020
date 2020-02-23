using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogUtils;

// stores all statements in a dialog block
public class DialogLabel : IEnumerable<object>
{
    public string id { get; private set; }
    public List<object> block { get; private set; }
    public Dictionary<string, DialogCharacter> characters { get; private set; }
    public Dictionary<string, DialogLabel> labels { get; private set; }
    // for nested labels
    public DialogLabel parent { get; private set; }
    // variables used to control 'if' statements
    public Dictionary<string, bool> flags;

    public DialogLabel(string id, List<object> block)
    {
        this.id = id;
        this.block = new List<object>(block);
        characters = new Dictionary<string, DialogCharacter>();
        labels = new Dictionary<string, DialogLabel>();
        flags = new Dictionary<string, bool>();
        // find all characters and labels
        // parse them to the characters list and labels list
        for (int i = 0; i < this.block.Count; i++)
        {
            // check for character statement
            if (CheckStatement(this.block[i], "character"))
            {
                DialogCharacter c = DialogCharacter.Load(this.block[i]);
                if (c == null)
                {
                    throw new DialogError($"Invalid DialogCharacter.\nYAML: {Reserialize(this.block[i])}");
                }
                if (characters.ContainsKey(c.id))
                {
                    throw new DialogError($"Duplicate DialogCharacter '{c.id}'.\nYAML: {Reserialize(this.block[i])}");
                }
                characters[c.id] = c;
                // remove
                this.block.RemoveAt(i);
                i--;
                continue;
            }
            // check for label statement
            if (CheckStatement(this.block[i], "label"))
            {
                DialogLabel l = Load(this.block[i]);
                if (l == null)
                {
                    throw new DialogError($"Invalid DialogLabel.\nYAML: {Reserialize(this.block[i])}");
                }
                if (labels.ContainsKey(l.id))
                {
                    throw new DialogError($"Duplicate DialogLabel '{l.id}'.\nYAML: {Reserialize(this.block[i])}");
                }
                labels[l.id] = l;
                l.parent = this;
                // remove
                this.block.RemoveAt(i);
                i--;
                continue;
            }
        }
    }

    // load a DialogLabel from an object.
    // may return null if not a valid DialogLabel.
    public static DialogLabel Load(object obj, string keyword = "label")
    {
        Dictionary<string, object> args = GetStatementArgs(obj, keyword);
        if (args == null)
        {
            throw new DialogError($"DialogLabel Error: {keyword} statement does not contain any fields.\nYAML: {Reserialize(obj)}");
        }
        if (!args.ContainsKey("id"))
        {
            throw new DialogError($"DialogLabel Error: {keyword} statement does not contain an 'id' field.\nYAML: {Reserialize(obj)}");
        }
        if (!args.ContainsKey("block"))
        {
            throw new DialogError($"DialogLabel Error: {keyword} statement does not contain a 'block' field.\nYAML: {Reserialize(obj)}");
        }
        List<object> block = args["block"] as List<object>;
        if (block == null)
        {
            throw new DialogError($"DialogLabel Error: {keyword} statement 'block' field does not contain a list.\nYAML: {Reserialize(obj)}");
        }
        return new DialogLabel((string)args["id"], block);
    }

    public IEnumerator<object> GetEnumerator()
    {
        return new DialogLabelEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"DialogLabel {id}";
    }
}