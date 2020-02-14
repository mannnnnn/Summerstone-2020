using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;


//The dialog structure holds all the information for one line of dialogue. 
public class DialogPieces : MonoBehaviour
{
    public string package;
    public string line;
    public string speaker;
    private Sprite emote;
    private Sprite portrait;
    public float xPosition; //in what location the portrait appears
    public float emoteYPos; //at what level the emote appears (can be set to private later)
	
    //setters
    public void setLine(string line)
    {
        this.line = line;
    }
    public Sprite getPortrait()
    {
       return this.portrait;
    }
    public void setSpeaker(string speaker, Sprite mysteryPortrait, Sprite mwPortrait, Sprite prPortrait, Sprite ttPortrait, Sprite elPortrait)
    {
        switch(speaker.Trim()){
            case ("AW"): this.speaker = "Arwell"; break;
            case ("TR"): this.speaker = "Terrin"; break;
            case ("MW"): this.speaker = "Maywood"; break;
            case ("PR"): this.speaker = "Periakka"; break;
            case ("TM"): this.speaker = "Talmut"; break;
            case ("TT"): this.speaker = "Titus"; break;
            case ("EL"): this.speaker = "Elliot"; break;
            case ("ON"): this.speaker = "Onara"; break;
            case ("WD"): this.speaker = "Wednt"; break;
            case ("BK"): this.speaker = "Barkeep"; break;
            default: this.speaker = "???"; break;
        }
	switch(speaker.Trim()){
            case ("AW"): this.portrait = mysteryPortrait; break;
            case ("TR"): this.portrait = mysteryPortrait; break;
            case ("MW"): this.portrait = mwPortrait; break;
            case ("PR"): this.portrait = prPortrait; break;
            case ("TM"): this.portrait = mysteryPortrait; break;
	        case ("TT"): this.portrait = ttPortrait; break;
	        case ("EL"): this.portrait = elPortrait; break;
	        case ("WD"): this.portrait = mysteryPortrait; break;
            case ("ON"): this.portrait = mysteryPortrait; break;
            case ("BK"): this.portrait = mysteryPortrait; break;
            default: this.portrait = mysteryPortrait; break;
			
        }
    }
    public void setEmote(string emote)
    {
	    switch(emote.Trim()){
            case ("heart"): this.emote = null; break; //heart
	        case ("surprise"): this.emote = null; break; //exclamation point
	        case ("anger"): this.emote = null; break; //grudge mark
	        case ("depressed"): this.emote = null; break; //purple lines
	        case ("joy"): this.emote = null; break; //yellow + orange lines
            default: this.emote = null; break;
        }
    }

    public void setPackage(string package)
    {
        this.package = package;
    }

    //getters
    public string getLine()
    {
        return line;
    }

    public string getSpeaker()
    {
        return speaker;
    }

    public Sprite getEmote()
    {
        return emote;
    }

    public string getPackage()
    {
        return package;
    }
}
