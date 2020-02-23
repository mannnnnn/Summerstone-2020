using System.Collections.Generic;
using UnityEngine;
using static DialogUtils;

// stores a character's ID + name pair.
// example: A character named "John", who in the dialog YAML has the id "J".
public class DialogCharacter
{
    public string id;
    public string name;
    public DialogCharacter(string id, string name)
    {
        this.id = id;
        this.name = name;
    }

    // load a DialogCharacter from an object.
    // may return null if not a valid DialogLabel.
    public static DialogCharacter Load(object obj)
    {
        Dictionary<string, object> args = GetStatementArgs(obj, "character");
        if (args == null)
        {
            throw new DialogError($"DialogCharacter Error: character does not contain any fields.\nYAML: {Reserialize(obj)}");
        }
        if (!args.ContainsKey("id"))
        {
            throw new DialogError($"DialogCharacter Error: character does not contain an 'id' field.\nYAML: {Reserialize(obj)}");
        }
        if (!args.ContainsKey("name"))
        {
            throw new DialogError($"DialogCharacter Error: character does not contain a 'name' field.\nYAML: {Reserialize(obj)}");
        }
        return new DialogCharacter((string)args["id"], (string)args["name"]);
    }

    public override string ToString()
    {
        return $"DialogCharacter {name} ({id})";
    }
}
