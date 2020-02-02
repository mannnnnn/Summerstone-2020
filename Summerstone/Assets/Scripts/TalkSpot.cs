using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkSpot : MonoBehaviour {

	public string package; //The name of the script group you want to call 
	private GameObject ChimeraController; //The object which contains the TextParser class
	public string textFileName;

	private TextParser parser;
	// Use this for initialization
	void Start () {
		ChimeraController = GameObject.FindGameObjectsWithTag("chimeraController")[0];
		parser = ChimeraController.GetComponent<TextParser>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("space")){
			    
				if(package != null && parser != null && parser.letsTalk == false){
					if(textFileName!= null && textFileName != ""){
						parser.changeTextFile(textFileName);
					}
                    parser.currPackage = package;
					parser.letsTalk = true;
					parser.moveOn = true;
					parser.playNextLine();
				}
            
		}
			
	
	}


	
}
