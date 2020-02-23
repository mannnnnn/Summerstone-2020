using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSprites : MonoBehaviour
{
    [Serializable]
    public struct DialogSprite
    {
        public string name;
        public Sprite sprite;
    }

    public List<DialogSprite> spriteList = new List<DialogSprite>();
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    void Awake()
    {
        // get dialog sprites
        foreach (DialogSprite s in spriteList)
        {
            sprites[s.name] = s.sprite;
        }
    }

    public static DialogSprites GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera").GetComponent<DialogSprites>();
    }

    public Sprite GetByName(string name)
    {
        return sprites[name];
    }
}
