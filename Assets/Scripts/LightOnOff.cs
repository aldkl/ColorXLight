using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{

    Animator LAni;

    // Start is called before the first frame update
    void Start()
    {
        LAni = GetComponent<Animator>();
        //LAni.enabled = false;
    }

    public void OnLight()
    {
        if(LAni.GetBool("On"))
        {
            LAni.SetBool("On", false);
        }
        else
        {
            LAni.SetBool("On", true);
        }
    }

    public bool GetOn()
    {
        return LAni.GetBool("On");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
