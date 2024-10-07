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
        //플랫폼 위치 변경(goal point로 움직임)
        platform.position = Vector3.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
        //다음 point와 근접했는지 체크
        if (Vector3.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            //근접했다면 다음 goal point로 변경
            //마지막 goal point에 도착했는지 확인 후, 처음 point로 리셋
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    
    }
}
