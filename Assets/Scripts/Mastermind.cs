using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{
    public List<MastermindColor> colors = new List<MastermindColor>();
    public MastermindColor[] goal = new MastermindColor[4];
    System.Random random = new System.Random();
    int triesRemaining = 0;
    bool success;

    public class MastermindResult
    {
        public int red;
        public int white;
    }

    [Serializable]
    public class MastermindColor
    {
        public string name;
        public Sprite image;
        [NonSerialized]
        public int index;
        public override bool Equals(object obj)
        {
            MastermindColor other = obj as MastermindColor;
            if (other == null)
            {
                return false;
            }
            return other.index == index;
        }
        public override int GetHashCode()
        {
            return index.GetHashCode();
        }
    }

    void Awake()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            colors[i].index = i;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        for (int i = 0; i < goal.Length; i++)
        {
            goal[i] = colors[random.Next(colors.Count)];
        }
        triesRemaining = 9;
    }

    public bool Failure()
    {
        return triesRemaining <= 0 && !Success();
    }

    public bool Success()
    {
        return success;
    }

    public MastermindResult Test(MastermindColor[] guess)
    {
        triesRemaining--;
        MastermindResult result = new MastermindResult() { red = 0, white = 0 };
        Dictionary<MastermindColor, int> nohits = new Dictionary<MastermindColor, int>();
        // count reds
        for (int i = 0; i < goal.Length; i++)
        {
            if (goal[i] == guess[i])
            {
                result.red++;
            }
            else
            {
                if (!nohits.ContainsKey(goal[i]))
                {
                    nohits[goal[i]] = 0;
                }
                nohits[goal[i]]++;
            }
        }
        // count whites
        for (int i = 0; i < guess.Length; i++)
        {
            if (nohits.ContainsKey(guess[i]) && nohits[guess[i]] > 0)
            {
                result.white++;
                nohits[guess[i]]--;
            }
        }
        if (result.red == goal.Length)
        {
            success = true;
        }
        return result;
    }
}
