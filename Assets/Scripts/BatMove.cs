using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{

    Vector2 Len;

    float z;

    Rigidbody2D BatRb2D;

    public float moveSpeed = 3f;
    public BoxCollider2D BatBox;
    public bool follow;
    Vector2 PlayerPos;
    void Start()
    {
        BatRb2D = GetComponent<Rigidbody2D>();
        BatBox = GetComponent<BoxCollider2D>();
        follow = false;
        BatBox.enabled = false;
    }


    void Update()
    {
        
        if(follow)
        {
            TargetPlayer();
            Move();
        }
    }

    private void TargetPlayer()
    {
        Len = GameManager.Instance.Player.transform.position - transform.position;
        z = Mathf.Atan2(Len.x, Len.y);
        //transform.rotation = Quaternion.Euler(0, 0, z);

    }

    private void Move()
    {
        
        if (Len.x < 0)//¿À¸¥ÂÊ
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        PlayerPos = new Vector2(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y + 1.4f);

        transform.position = Vector2.MoveTowards(transform.position, PlayerPos, moveSpeed * Time.deltaTime);
    }

    public IEnumerator MoveOn()
    {
        yield return new WaitForSeconds(2f);
        BatBox.enabled = true;
        follow = true;
    }
}
