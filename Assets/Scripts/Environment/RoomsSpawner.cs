using System.Collections.Generic;
using UnityEngine;

public class RoomsSpawner : MonoBehaviour
{
    [SerializeField] private int _numberOfRoomsVisibleToPlayer;
    [SerializeField] private GameObject _teleport;
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _distanceBetweenRooms;
    [SerializeField] private int _numberOfRooms;
    [SerializeField] private int _numberOfDuplicatedRooms;
    [SerializeField] private int _numberOfRoomsToStart;

    private static List<GameObject> _usedRooms;

    private void Awake()
    {
        _usedRooms = new List<GameObject>();

        GameObject newRoom;

        _startPosition.z = -_distanceBetweenRooms * _numberOfRoomsToStart;
        Vector3 newPosition = _startPosition;

        List<int> roomIndexes = SomeMath.CreateRandomUniqueIndexes(_numberOfRooms - _numberOfDuplicatedRooms, 0, _rooms.Length);

        for (int i = 0; i < _numberOfRooms - _numberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(_rooms[roomIndexes[i]]);
            _usedRooms.Add(newRoom);
        }
        for (int i = 0; i < _numberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(_usedRooms[i + _numberOfRoomsToStart]);
            _usedRooms.Add(newRoom);
        }

        for (int i = 0; i < _usedRooms.Count; i++)
        {
            newPosition.z = i * _distanceBetweenRooms;            
            _usedRooms[i].transform.position = _startPosition + newPosition;
            _usedRooms[i].transform.parent = gameObject.transform;

            _usedRooms[i].AddComponent<Room>();
            _usedRooms[i].GetComponent<Room>().RoomNumber = i;
        }

        if (_teleport != null)
        {
            newPosition = _teleport.transform.position;
            newPosition.z = _usedRooms[_numberOfRooms - _numberOfDuplicatedRooms].transform.position.z + 0.5f;
            _teleport.transform.position = newPosition;
        }

        //ObjectPool
        for (int i = 0; i < _usedRooms.Count; i++)
        {
            _usedRooms[i].SetActive(false);
        }

        for (int i = 0; i < _numberOfRoomsVisibleToPlayer; i++)
        {
            _usedRooms[i].SetActive(true);
        }
    }
    
    private void OnEnable()
    {
        Room.OnRoomExit += ActivateNextRoom;
        TeleportationOfPlayer.OnPlayerTeleport += DeactivateAllDuplicateRooms;
    }

    private void OnDisable()
    {
        Room.OnRoomExit -= ActivateNextRoom;
        TeleportationOfPlayer.OnPlayerTeleport -= DeactivateAllDuplicateRooms;
    }

    private void ActivateNextRoom(int currentRoomNumber)
    {
        int duplicateRoomNumberToActivate;
        _usedRooms[currentRoomNumber].SetActive(false);

        if (currentRoomNumber >= _usedRooms.Count - _numberOfRoomsVisibleToPlayer - _numberOfDuplicatedRooms)
        {
            duplicateRoomNumberToActivate = currentRoomNumber + _numberOfRoomsVisibleToPlayer;
            if (duplicateRoomNumberToActivate < _numberOfRooms)
            {
                _usedRooms[currentRoomNumber + _numberOfRoomsVisibleToPlayer].SetActive(true);
            }

            currentRoomNumber = currentRoomNumber - (_usedRooms.Count - _numberOfDuplicatedRooms) + _numberOfRoomsToStart;
        }

        int numberOfRoomYouWantToActivate = currentRoomNumber + _numberOfRoomsVisibleToPlayer;
        _usedRooms[numberOfRoomYouWantToActivate].SetActive(true);
    }

    private void DeactivateAllDuplicateRooms()
    {
        for (int i = 1; i <= _numberOfDuplicatedRooms; i++)
        {
            _usedRooms[_numberOfRooms - i].SetActive(false);
        }
    }
}