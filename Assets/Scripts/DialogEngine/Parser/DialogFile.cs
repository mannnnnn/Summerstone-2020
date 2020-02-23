using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

// loads dialog from a text file.
// dialog files are YAML files that are formatted for dialog.
public class DialogFile : IEnumerable<object>
{
    DialogLabel main;

    public Dictionary<string, DialogCharacter> Characters => main.characters;

    public DialogFile(string path, Dictionary<string, bool> flags)
    {
        // read text from a file
        StringReader stringReader = new StringReader(new StreamReader(path).ReadToEnd());
        // parse to runnable block
        List<object> block = new Deserializer().Deserialize<List<object>>(stringReader);
        main = new DialogLabel("main", block);
        main.flags = flags;
    }

    public IEnumerator<object> GetEnumerator()
    {
        return main.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
