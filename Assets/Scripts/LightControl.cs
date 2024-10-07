using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : Interactable
{
    public LightOnOff[] Light_;

    public override void Interact()
    {
        for(int i = 0; i < Light_.Length; i++)
        {
            Light_[i].OnLight();
        }

    }
}
