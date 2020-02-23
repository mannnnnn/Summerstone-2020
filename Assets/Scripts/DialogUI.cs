using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : MonoBehaviour, IDialogUI
{
    [Serializable]
    public struct DialogSprite
    {
        public string name;
        public Sprite sprite;
    }
    public List<DialogSprite> spriteList = new List<DialogSprite>();
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    // text scroll animation
    Coroutine textScroll;

    public GameObject chatBoxPrefab;
    public Transform chatBoxParent;
    Chimera chimera;

    // Start is called before the first frame update
    void Start()
    {
        chimera = Chimera.GetInstance();
        Transform canvas = transform.Find("Canvas");
        // get dialog sprites
        foreach (DialogSprite s in spriteList)
        {
            sprites[s.name] = s.sprite;
        }
        Run("Assets/Scripts/Dialogue/TestForParser.yaml");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextPressed)
        {
            if (textScroll == null)
            {
                DialogRunner.RequestAdvance();
            }
            else
            {
                // finish scrolling
                scrollTimer += 9001f;
            }
        }
        nextPressed = false;
    }

    // next button clicked
    bool nextPressed = false;
    public void Next()
    {
        nextPressed = true;
    }

    public void Run(string s)
    {
        // load dialog and run, such as "Assets/Dialog/control_test.yaml"
        DialogRunner.Run(this, s);
    }

    // creates a new dialog box and scrolls up
    public void DisplayText(string name, string text)
    {
        // create new chat box
        GameObject go = Instantiate(chatBoxPrefab, chatBoxParent);
        go.GetComponent<ChatRow>().Set(name, text, sprites[name]);
        // stop old text scroll animation if it is still running
        if (textScroll != null)
        {
            StopCoroutine(textScroll);
            textScroll = null;
        }
        // use speed as Mathf.Infinity to turn off scrolling
        textScroll = StartCoroutine(ScrollText(text, 75f));
    }
    // speed is in characters/second
    float scrollTimer = 0;
    IEnumerator ScrollText(string text, float speed)
    {
        scrollTimer = Time.deltaTime;
        int len = 0;
        // loop until we reach end of text
        do
        {
            scrollTimer += Time.deltaTime;
            len = (int)Math.Min(text.Length, speed * scrollTimer);
            yield return null;
            // textPanel.Show(text.Substring(0, len));
        }
        while (len < text.Length);
        // finish
        textScroll = null;
    }

    public void ShowCharacter(string id, string image, DialogImageOptions options)
    {
        throw new NotImplementedException();
    }

    public void HideCharacter(string id)
    {
        throw new NotImplementedException();
    }

    public void Finish()
    {
        // stop text scroll
        if (textScroll != null)
        {
            StopCoroutine(textScroll);
            textScroll = null;
        }
        chimera.nextGameState();
    }
}
