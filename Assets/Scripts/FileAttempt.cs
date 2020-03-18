using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileAttempt: MonoBehaviour
{
    // Start is called before the first frame update
    public int redChoice;
    public int whiteChoice;
    public GameObject prefab;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Set(GameObject a){
      prefab = a;
      Debug.Log("SHOULDVE CAME HERE");

    }
    public void addIn(){
      redChoice = 1;
    }

    void OnDestroy(){
      Debug.Log("aafafafaafaf");
      Debug.Log("teaaa");
    }

}
