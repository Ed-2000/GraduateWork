using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationOfPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = 0;

            transform.position = newPosition;
        }
    }
}