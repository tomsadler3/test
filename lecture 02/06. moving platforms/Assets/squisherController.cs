using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squisherController : MonoBehaviour
{
    public float minScale;
    public float maxScale;
    public float scaleTime;

    bool isScalingUp;
    float currentTime;

    
    void Start ()
    {
        currentTime = 0;
        isScalingUp = true;
	}
		
	void Update ()
    {
        var temp = transform.localScale;

        temp.y = Mathf.Lerp(minScale, maxScale, currentTime / scaleTime);
        transform.localScale = temp;

        if (isScalingUp == true)
        {
            currentTime += Time.deltaTime;

            if(currentTime > scaleTime)
            {
                currentTime = scaleTime;
                isScalingUp = false;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;

            if(currentTime < 0)
            {
                currentTime = 0;
                isScalingUp = true;
            }
        }
	}
}
