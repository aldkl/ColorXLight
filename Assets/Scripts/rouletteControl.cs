using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rouletteControl : Interactable
{
    public int Type;
    public GameObject Roulette;
    public bool EnableMove;
    public int Rotate = 0;
    public float RotateSpeed;
    public float RotateAngle;
    public override void Interact()
    {
        Rotate++;
        EnableMove = true;
    }
    void Update()
    {
        if (EnableMove)
        {
            if((RotateAngle * Rotate) % 360 == 0)
            {
                if(Roulette.transform.GetChild(Type).rotation.eulerAngles.z >= 359.3f)
                {
                    EnableMove = false;
                    Roulette.transform.GetChild(Type).rotation = Quaternion.Euler(0, 0, RotateAngle * Rotate % 360);
                }
                else
                {
                    Roulette.transform.GetChild(Type).Rotate(0, 0, RotateSpeed * Time.deltaTime, Space.Self);
                }
            }
            else if (Roulette.transform.GetChild(Type).rotation.eulerAngles.z >= (RotateAngle * Rotate) % 360)
            {
                EnableMove = false;
                Roulette.transform.GetChild(Type).rotation = Quaternion.Euler(0, 0, RotateAngle * Rotate % 360);
            }
            else
            {
                Roulette.transform.GetChild(Type).Rotate(0, 0, RotateSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
