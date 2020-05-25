using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowYourFakeParent : MonoBehaviour
{
    public GameObject fakeParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != fakeParent.transform.position)
        {
            transform.position = new Vector3(fakeParent.transform.position.x+25, fakeParent.transform.position.y+20, fakeParent.transform.position.z);
        }   
    }
}
