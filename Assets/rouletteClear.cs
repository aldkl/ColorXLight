using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rouletteClear : MonoBehaviour
{
    public rouletteControl[] Loulette;

    public GameObject Onable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Loulette[0].Rotate % 8 == 2 &&
           Loulette[1].Rotate % 8 == 0 &&
           Loulette[2].Rotate % 8 == 7)
        {
            if (!Loulette[0].EnableMove && !Loulette[1].EnableMove && !Loulette[2].EnableMove)
            {
                Onable.SetActive(true);
            }
        }
        else
        {
            if (Onable.activeSelf)
            {

                Onable.SetActive(false);
            }
        }
    }
}
