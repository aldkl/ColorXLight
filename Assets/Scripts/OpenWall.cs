using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : MonoBehaviour
{

    public bool EnableMove;
    public bool Enable;
    public int Count;
    public GameManager.E_Color OpColor;

    void Start()
    {
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Enable)//���� �ȿö�
        {
            Count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Instance.PColors[i] == OpColor)
                {
                    Count++;
                }
            }
            if(Count > 0)//0���� ũ��
            {
                StartCoroutine(MoveOn());
            }
        }
        else // �ö��ִ� �����̸�
        {
            Count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Instance.PColors[i] == OpColor)
                {
                    Count++;
                }
            }
            if (Count == 0)
            {
                StartCoroutine(MoveOFF());
            }
        }
    }

    public IEnumerator MoveOn()
    {
        while (!EnableMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(0, 4.5f, 0), 4.5f * Time.deltaTime);
            yield return new WaitForSeconds(1f);
            EnableMove = true;
            Enable = true;//�ö󰬴ٰ� ��
        }
    }

    public IEnumerator MoveOFF()
    {
        while (EnableMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(0, 4.5f, 0), 4.5f * Time.deltaTime);
            yield return new WaitForSeconds(1f);
            EnableMove = false;
            Enable = false;//�ö󰬴ٰ� ��
        }
    }
}
