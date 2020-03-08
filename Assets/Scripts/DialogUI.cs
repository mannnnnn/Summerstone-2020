﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call the Run method in this class from Chimera.nextGameState before showing this screen
// to specify this screen's contents.
public class DialogUI : MonoBehaviour, IDialogUI
{
    // text scroll animation
    Coroutine textScroll;

    public GameObject chatBoxPrefab;
    public Transform chatBoxParent;
    Chimera chimera;

    DialogSprites dialogSprites;

    // Start is called before the first frame update
    void Start()
    {
        dialogSprites = DialogSprites.GetInstance();
        chimera = Chimera.GetInstance();
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

    public void Run(string type, int week)
    {
        Run($"Text/{type}/{type}{week}");
    }

    // next button clicked
    bool nextPressed = false;
    public void Next()
    {
        nextPressed = true;
        // if we forgot to start dialog, allow user to advance
        // actually, this should probably throw an error but...
        if (!DialogRunner.Active)
        {
            chimera.nextGameState();
        }
    }

    public void Run(string s)
    {
        // load dialog and run, such as "Assets/Dialog/control_test.yaml"
        DialogRunner.Run(this, s);
        foreach (Transform child in chatBoxParent)
        {
            Destroy(child.gameObject);
        }
    }

    // creates a new dialog box and scrolls up
    public void DisplayText(string name, string text)
    {
        // create new chat box
        GameObject go = Instantiate(chatBoxPrefab, chatBoxParent);
        ChatRow chatbox = go.GetComponent<ChatRow>();
        chatbox.Set(name, text, dialogSprites.GetByName(name));
        // stop old text scroll animation if it is still running
        if (textScroll != null)
        {
            StopCoroutine(textScroll);
            textScroll = null;
        }
        // use speed as Mathf.Infinity to turn off scrolling
        textScroll = StartCoroutine(ScrollText(chatbox, text, 75f));
    }
    // speed is in characters/second
    float scrollTimer = 0;
    IEnumerator ScrollText(ChatRow chatbox, string text, float speed)
    {
        scrollTimer = Time.deltaTime;
        int len = 0;
        // loop until we reach end of text
        do
        {
            scrollTimer += Time.deltaTime;
            len = (int)Math.Min(text.Length, speed * scrollTimer);
            yield return null;
            chatbox.SetText(text.Substring(0, len));
        }
        while (len < text.Length);
        // finish
        textScroll = null;
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

    public void ShowCharacter(string id, string image, DialogImageOptions options)
    {
        throw new NotImplementedException();
    }

    public void HideCharacter(string id)
    {
        throw new NotImplementedException();
    }
}
