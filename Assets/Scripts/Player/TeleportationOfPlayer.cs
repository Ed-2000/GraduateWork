using UnityEngine;

public class TeleportationOfPlayer : MonoBehaviour
{
    public delegate void Teleport();
    public static event Teleport OnPlayerTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = 0;

            transform.position = newPosition;

            OnPlayerTeleport?.Invoke();
        }
    }
}