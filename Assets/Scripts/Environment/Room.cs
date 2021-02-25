using UnityEngine;

public class Room : MonoBehaviour
{
    public delegate void RoomTriger(int roomNumber);
    public static event RoomTriger OnRoomExit;

    private int _roomNumber;
    public int RoomNumber { get => _roomNumber; set => _roomNumber = value; }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            OnRoomExit?.Invoke(_roomNumber);
        }
    }
}