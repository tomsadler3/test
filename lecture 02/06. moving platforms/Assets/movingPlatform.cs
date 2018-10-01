using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;

    public float journeyTime;

    bool isOnOutboundLeg;
    float currentTime;
    
    void Start ()
    {
        isOnOutboundLeg = false;
        currentTime = 0;
    }
	
	void Update ()
    {
        var pos = transform.localPosition;

        var tempPos = Vector2.Lerp(startPos, endPos, currentTime / journeyTime);

        pos.x = tempPos.x;
        pos.y = tempPos.y;

        transform.localPosition = pos;

        if (isOnOutboundLeg == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > journeyTime)
            {
                currentTime = journeyTime;
                isOnOutboundLeg = false;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                currentTime = 0;
                isOnOutboundLeg = true;
            }
        }
    }
}
