using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRow : MonoBehaviour
{
    public Text nametag;
    public Text text;
    public Image speaker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(string nametag, string text, Sprite speaker)
    {
        this.nametag.text = nametag;
        this.text.text = text;
        this.speaker.sprite = speaker;
    }
}
