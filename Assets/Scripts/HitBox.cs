using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            GameManager.Instance.Player.HitPlayer();
            collision.transform.parent.GetComponent<Openable>().ColorOn();
        }
    }
}
