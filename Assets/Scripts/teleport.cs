using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject Portal;
    public static int StartTeleport = 0;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && teleport.StartTeleport == 1)
        {
            teleport.StartTeleport = 0;
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.Player.transform.position = new Vector3(Portal.transform.position.x, Portal.transform.position.y, Portal.transform.position.z);
    }

}
