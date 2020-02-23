using System.Collections.Generic;
using UnityEngine;
using static DialogUtils;

// runs dialog, handles dialog control flow
// such as jump statements, return, exit, etc.
public class DialogLabelEnumerator : IEnumerator<object>
{
    // next statement to execute
    // will throw InvalidOperationError if accessed while MoveNext returns false
    public object Current => stack.Peek().label.block[stack.Peek().index];

    // root dialog label
    private DialogLabel root;

    // call stack
    // current instruction position is at stack.Peek().index
    Stack<DialogStackEntry> stack;
    class DialogStackEntry
    {
        public DialogLabel label;
        public int index;
        public DialogStackEntry(DialogLabel label, int index)
        {
            this.label = label;
            this.index = index;
        }
    }

    public DialogLabelEnumerator(DialogLabel label)
    {
        root = label;
        stack = new Stack<DialogStackEntry>();
        Reset();
    }

    // advance dialog state until we reach
    // a dialog statement not related to control flow
    private void RunAll()
    {
        while (Run()) { }
    }

    // advance dialog state by one if it is a control flow statement.
    // otherwise, do nothing and return false.
    private bool Run()
    {
        // if stack is empty, we are done
        if (stack.Count == 0)
        {
            return false;
        }
        int idx = stack.Peek().index;
        // if current index is past end of block, pop from stack
        if (stack.Peek().index >= stack.Peek().label.block.Count)
        {
            Pop();
            return true;
        }
        // handle all control flow statements
        // labels: calls and exiting
        if (CheckStatement(Current, "exit"))
        {
            stack.Clear();
            return true;
        }
        else if (CheckStatement(Current, "return"))
        {
            Pop();
            return true;
        }
        else if (CheckStatement(Current, "jump"))
        {
            string arg = GetStatementArg(Current, "jump");
            DialogLabel next = FindLabel(arg, stack.Peek().label);
            if (next == null)
            {
                throw new DialogError($"Label '{arg}' does not exist in this context.\nYAML: {Reserialize(Current)}");
            }
            Push(next);
            return true;
        }
        // pass: does nothing
        else if (CheckStatement(Current, "pass"))
        {
            stack.Peek().index++;
            return true;
        }
        // conditionals: if statement and variables
        else if (CheckStatement(Current, "if"))
        {
            // load if statement block
            DialogLabel l = DialogLabel.Load(Current, "if");
            // if flag is true, run block
            if (FindFlag(l.id, stack.Peek().label))
            {
                Push(l);
            }
            else
            {
                stack.Peek().index++;
            }
            return true;
        }
        else if (CheckStatement(Current, "ifnot"))
        {
            // load ifnot statement block
            DialogLabel l = DialogLabel.Load(Current, "ifnot");
            // if flag is false, run block
            if (!FindFlag(l.id, stack.Peek().label))
            {
                Push(l);
            }
            else
            {
                stack.Peek().index++;
            }
            return true;
        }
        else if (CheckStatement(Current, "set"))
        {
            string arg = GetStatementArg(Current, "set");
            root.flags[arg] = true;
            stack.Peek().index++;
            return true;
        }
        else if (CheckStatement(Current, "unset"))
        {
            string arg = GetStatementArg(Current, "unset");
            root.flags[arg] = false;
            stack.Peek().index++;
            return true;
        }
        // if not a control flow statement, we are done
        return false;
    }

    private void Push(DialogLabel label)
    {
        stack.Peek().index++;
        stack.Push(new DialogStackEntry(label, 0));
    }
    private void Pop()
    {
        stack.Pop();
    }

    // traverse the label parents to resolve the label name
    private DialogLabel FindLabel(string name, DialogLabel label)
    {
        DialogLabel current = label;
        do
        {
            if (current.labels.ContainsKey(name))
            {
                return current.labels[name];
            }
            current = current.parent;
        }
        while (current != null);
        return null;
    }
    // traverse the label parents to resolve the flag name
    private bool FindFlag(string name, DialogLabel label)
    {
        DialogLabel current = label;
        do
        {
            if (current.flags.ContainsKey(name))
            {
                return current.flags[name];
            }
            current = current.parent;
        }
        while (current != null);
        // if flag doesn't exist, it has value false
        return false;
    }

    public bool MoveNext()
    {
        // advance
        stack.Peek().index++;
        RunAll();
        // if stack is empty, we are done, return false
        return stack.Count > 0;
    }

    public void Reset()
    {
        stack.Clear();
        stack.Push(new DialogStackEntry(root, 0));
        Run();
    }

    public void Dispose() { }
}
