using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkSpot : MonoBehaviour {

	public string package; //The name of the script group you want to call 
	private GameObject ChimeraController; //The object which contains the TextParser class
	public string textFileName;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("space"))
        {
		}
	}
}
