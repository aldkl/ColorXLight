using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnd : MonoBehaviour
{
    public LightOnOff[] Light_;

    public int Count = 0;

    void Update()
    {
        CheckLight();
    }

    public void CheckLight()
    {
        Count = 0;
        for(int i = 0; i < Light_.Length; i++)
        {
            if (Light_[i].GetOn())
            {
                Count++;
            }
        }
        if(Count == 4)
        {
                StartCoroutine(MoveOn());
                MovePlatform();
        }
    }

    public IEnumerator MoveOn()
    {
        yield return new WaitForSeconds(1f);
        this.enabled = false;
    }
    public void MovePlatform()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(0, 5, 0), 5f * Time.deltaTime);
    }
}
