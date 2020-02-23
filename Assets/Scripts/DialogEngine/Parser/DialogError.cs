using System;

public class DialogError : Exception
{
    public DialogError(string message) : base(message)
    {
    }
}