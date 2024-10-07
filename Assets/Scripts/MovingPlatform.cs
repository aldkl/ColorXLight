using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float moveSpeed = 2;


    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //�÷��� ��ġ ����(goal point�� ������)
        platform.position = Vector3.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
        //���� point�� �����ߴ��� üũ
        if (Vector3.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            //�����ߴٸ� ���� goal point�� ����
            //������ goal point�� �����ߴ��� Ȯ�� ��, ó�� point�� ����
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    
    }
}
