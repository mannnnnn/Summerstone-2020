using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Serialization;

public static class DialogUtils
{
    // checks if a statement is a keyword statement
    public static bool CheckStatement(object statement, string keyword)
    {
        // statement must be a dict and contain the keyword
        return statement is Dictionary<object, object>
            && ((Dictionary<object, object>)statement).ContainsKey(keyword);
    }

    // extract single string arg for the keyword statement
    public static string GetStatementArg(object statement, string keyword)
    {
        Dictionary<object, object> s = statement as Dictionary<object, object>;
        if (s == null || !s.ContainsKey(keyword))
        {
            return null;
        }
        string arg = s[keyword] as string;
        if (arg == null)
        {
            return null;
        }
        return arg;
    }

    // extract dictionary of args for the keyword statement
    public static Dictionary<string, object> GetStatementArgs(object statement, string keyword)
    {
        Dictionary<object, object> s = statement as Dictionary<object, object>;
        if (s == null || !s.ContainsKey(keyword))
        {
            return null;
        }
        Dictionary<object, object> args = s[keyword] as Dictionary<object, object>;
        if (args == null)
        {
            return null;
        }
        return CastArgs(args);
    }

    // converts Dictionary<object, object> to Dictionary<string, object> for ease of use
    public static Dictionary<string, object> CastArgs(Dictionary<object, object> args)
    {
        Dictionary<string, object> castedArgs = new Dictionary<string, object>();
        foreach (object o in args.Keys)
        {
            castedArgs[(string)o] = args[o];
        }
        return castedArgs;
    }

    // used for error output
    private static Serializer serializer = new Serializer();
    public static string Reserialize(object o)
    {
        return serializer.Serialize(o);
    }
}