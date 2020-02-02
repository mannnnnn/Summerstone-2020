using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class TextParser : MonoBehaviour {
    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
    [Header("Essentials")]
    [Range(0,0.05f)]
    public float typeSpeed = 0.025f;
    public string textFile;
    public string currPackage; //let user change
    public GameObject fullBox;
    public Text lineBox;
    public Text speakerBox;
    public int currLineNum = -1;
    public float yBounce = 2;
    public float yPos;
    public AudioSource sfx;
    public Color32 greyColor;
    public Color32 whiteColor;
    private string fullText;
    private List<DialogPieces> talkScript = new List<DialogPieces>();
    private int currStart = 0;
    private int currEnd = -1;
    private int printNum = -1;

    public GameObject spawnParent;
    public GameObject chatBox;
    //1 speakerInfo, 2 the actual text, 3 name

    [Header("Typewriter Sounds")]
    public AudioClip clickNoise1;
    public AudioClip clickNoise2;
    public AudioClip clickNoise3;
	
    [Header("Misc.")]
    private bool showEmote = false;
    private float[] storedEmotePositions = new float[4];
    private List<string> speakers = new List<string>();
    private bool bounceComplete = false;
    private int randInt = 0;

    [Header("Do Not Touch")]
    public Sprite mysteryPortrait;
    public Sprite mwPortrait;
    public Sprite prPortrait;
    public Sprite elPortrait;
    public Sprite ttPortrait;

    public bool moveOn = false;
    public bool letsTalk = false;

    //To be moved later - - - - - - -
    public float aniEnterOffsetY = -1;
    public float aniEnterSpeed = 5;
    public float aniBobOffsetY = 0.4f;
    public float aniBobSpeed = 1;

    private List<GameObject> aniObjects = new List<GameObject>();
    private List<GameObject> aniBobs = new List<GameObject>();
    public List<Vector3> targets = new List<Vector3>();
    public List<Vector3> bobTargets = new List<Vector3>();
    public List<Vector3> bobStarts = new List<Vector3>();
    bool bobUp = true;
    // - - - - - - - - - - - -

    // Use this for initialization
    void Start () {
        if(textFile != null)
        {
           fullText = getFileText();
        }
        fillDialogArray();
    }


    public void changeTextFile(string newFileName){
      //  talkScript.Clear();
        textFile = newFileName;
         if(textFile != null)
        {
          // fullText = getFileText();
        }
        //fillDialogArray();
    }
	
	// Update is called once per frame
	void Update () {
        //check proximity
        if ((Input.GetKeyDown("space") || moveOn) && letsTalk && aniObjects.Count == 0 && aniBobs.Count == 0){
            moveOn = false;
            if ((printNum < 0 || currLineNum < 0)){
                playNextLine();
            }
            else if (printNum < talkScript[currLineNum].getLine().Length){
                printNum = talkScript[currLineNum].getLine().Length-1;
            } else{
                currLineNum++;
                StopAllCoroutines();
                playNextLine();
            }  
        } 
	}

    private string getFileText() {
        string readText = "";
        
        if(Resources.Load<TextAsset>("TextFiles/" + textFile) == null){
            Debug.Log("it's completely null!");
        }
        readText = Resources.Load<TextAsset>("TextFiles/" + textFile).text;
        if(readText == null){
            Debug.Log("It's null text!");
        }

        return readText;
    }

    private void fillDialogArray()
    {
        DialogPieces currDia = gameObject.AddComponent<DialogPieces>();
        string currLine = "";
        string currSpeaker = "";
        string currPackage = "";

        for (int i = 0; currEnd < fullText.Length && i<1000; i++)
        {
            currDia = gameObject.AddComponent<DialogPieces>();
            currLine = "";

            if(fullText.IndexOf("\n", currStart) < 0)
            {
                return;
            }

            if (fullText.IndexOf("Callname:", currStart) >= 0 && fullText.IndexOf("\n", currStart) >= 0 && fullText.IndexOf("Callname:", currStart) < fullText.IndexOf("\n", currStart))
            {
                if(fullText.Substring(fullText.IndexOf(":", currStart)+1, fullText.IndexOf("\n", currStart) - fullText.IndexOf(":", currStart)) == "END"){
                    return;
                }

                //Debug.Log("CALLNAME FOUND: " + fullText.Substring(fullText.IndexOf(":", currStart)+1, fullText.IndexOf("\n", currStart) - fullText.IndexOf(":", currStart)));
                currPackage = fullText.Substring(fullText.IndexOf(":", currStart)+1, fullText.IndexOf("\n", currStart) - fullText.IndexOf(":", currStart));
                currStart = fullText.IndexOf("\n", currStart)+1;
            }


            if (fullText.IndexOf(":", currStart) >= 0)
            {
                currSpeaker = fullText.Substring(currStart, fullText.IndexOf(":", currStart)-currStart);
                currStart = fullText.IndexOf(":", currStart)+1;
            }
          
                currDia.setSpeaker(currSpeaker, mysteryPortrait, mwPortrait, prPortrait, ttPortrait, elPortrait); //carries over if not specified
                currStart = fullText.IndexOf("\"", currStart) + 1;
                currEnd = fullText.IndexOf("\"", currStart); //second quote
                currLine = fullText.Substring(currStart, currEnd - currStart);
                currDia.setLine(currLine);
                currDia.setPackage(currPackage);
            
                talkScript.Add(currDia);


            if (fullText.IndexOf("\n", currStart) >= 0)
            {
                currStart = fullText.IndexOf("\n", currStart) + 1;
                while (!char.IsLetterOrDigit(fullText[currStart]))
                {
                    currStart++;
                }
            }
            else
            {
                return;
            }

        }

       
    }

    private void endShowingText(){
        currLineNum = -1;
        fullBox.SetActive(false);
        speakers = new List<string>();
        letsTalk = false;
    }


    public void playNextLine()
    {
        if(currLineNum >= talkScript.Count){
            endShowingText();
            return;
        }

        //set up w/ package name
        for(int i = 0; i<talkScript.Count && currLineNum == -1 && i<1000; i++){
            if (talkScript[i].getPackage().Trim().Equals(currPackage.Trim())){
                currLineNum = i;
            }
        }

       if(currLineNum < 0){
            Debug.Log("No dialog lines with the selected package name was found.");
            return;
        }

        //check if at end
        if(!talkScript[currLineNum].getPackage().Trim().Equals(currPackage.Trim())){
           endShowingText();
        } 
        else{
            GameObject chatBoxSpawn = Instantiate(chatBox, spawnParent.transform);
            Image portrait = chatBoxSpawn.transform.GetChild(0).GetComponent<Image>();
            Text namePlate = chatBoxSpawn.transform.GetChild(1).GetComponent<Text>();
            Text dialog = chatBoxSpawn.transform.GetChild(2).GetComponent<Text>();

            if(talkScript[currLineNum].getLine() == "flipSprite"){
                moveOn = true;
                //TODO: actually put in the other card
            }
            else{
                portrait.sprite = talkScript[currLineNum].getPortrait();
                namePlate.text = talkScript[currLineNum].getSpeaker();
                StartCoroutine(AnimateText(dialog));   
            }    
        }
    }
        
    
    IEnumerator AnimateText(Text prefabTextbox){
        printNum = 0;
        
        for (printNum = 0; printNum < (talkScript[currLineNum].getLine().Length+1); printNum++)
        {
            prefabTextbox.text = talkScript[currLineNum].getLine().Substring(0, printNum);
            randInt = Random.Range(1, 4);
            switch(randInt){
                case(1): sfx.clip = clickNoise1; break;
                case(2): sfx.clip = clickNoise2; break;
                case(3): sfx.clip = clickNoise3; break;
                default: break;
            }
            sfx.Play(0);
            yield return new WaitForSeconds(typeSpeed);
        }
        
    }

}




