using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{
    public List<MastermindColor> colors = new List<MastermindColor>();

    // stuff for the first attempt (creating prefabs with a list)
    public GameObject testPrefab;
    public int heightModifier;
    public List<FileAttempt2> rows = new List<FileAttempt2>();

    //second attempt, creating prefabs for a row;
    public GameObject testRow;
    public int rowModifier;

    public int rowNumber;
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

    [Serializable]
    public class FileAttempt2
    {
        // Start is called before the first frame update
        public int redChoice;
        public int whiteChoice;
        public List<GameObject> choices = new List<GameObject>();
        public void Set(GameObject a){
          choices.Add(a);
        }
        public void selectRed(){
          redChoice = 1;
        }
        public void selectWhite(){
          whiteChoice = 1;
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
        heightModifier = 1;
        rowModifier = 1;
        for (int i = 0; i<9; i++){
          FileAttempt2 first = new FileAttempt2();
          rows.Add(first);
        }
        rowNumber = 0;

        for(int i = 0; i < 9; i++){
            var mytest = Instantiate(testRow, gameObject.transform.position, Quaternion.identity);
            mytest.transform.parent = gameObject.transform;
            rowModifier = rowModifier + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //This is creating a list of prefab rows, with set buttons.

    public void rowStart(){
        for(int i = 0; i < 9; i++){
          var mytest = Instantiate(testRow, new Vector3(300 + (i * 225), 1850 - (rowModifier * 200),0), Quaternion.identity);
          mytest.transform.parent = gameObject.transform;
          rowModifier = rowModifier + 1;
        }
    }


    // This is creating a list, and setting created prefab buttons to them style
    public void initialStart(){
        for(int i = 0; i < 4; i++){
          var mytest = Instantiate(testPrefab, new Vector3(300 + (i * 225), 1850,0), Quaternion.identity);
          mytest.transform.parent = gameObject.transform;
          //adding to the rows knowledge of prefabs (?)
          rows[rowNumber].Set(mytest);
        }
        // Moving to next list (total rows is 10);
        rowNumber = rowNumber + 1;
    }

    //Temporarily generated per click on the start button on top
    public void postAttempt(){
      for(int i = 0; i < 4; i++){
        var mytest = Instantiate(testPrefab, new Vector3(300 + (i * 225), 1850 - (heightModifier * 200),0), Quaternion.identity);
        mytest.transform.parent = gameObject.transform;
        rows[rowNumber].Set(mytest);
      }
      rowNumber = rowNumber + 1;
      heightModifier += 1;
    }



    //Original Code
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
