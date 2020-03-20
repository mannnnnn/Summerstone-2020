using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int redChoice;
    public int whiteChoice;
    public GameObject parent;

    void Start()
    {
      parent = transform.parent.gameObject;
      Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void colorSelect(){
      Debug.Log("hla" + gameObject.name);
      parent.GetComponent<Mastermind>().postAttempt();
      Debug.Log("try it here ");
      Debug.Log(parent.GetComponent<Mastermind>().goal);
      redChoice = 100;
    }
    void OnDestroy(){
      Debug.Log("aafafafaafaf");
      Debug.Log("teaaa");
      Debug.Log(parent.name);
    }

    public void submit(){
      //parent.GetComponent<Mastermind>().Test();
    }

}
