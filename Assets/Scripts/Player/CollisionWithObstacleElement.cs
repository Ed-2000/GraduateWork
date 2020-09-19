using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObstacleElement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OENotForTouch"))
        {
            //викликати подію Dead
            GetComponent<BodyDestroyer>().ReplaceBodyWithDestroyBody();
        }
        else if (other.CompareTag("OEToTouch"))
        {

        }
    }
}
