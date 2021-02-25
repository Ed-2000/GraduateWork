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
    [SerializeField] private bool _isRotate;

    private static List<GameObject> _usedRooms;

    private void Awake()
    {
        _usedRooms = new List<GameObject>();

        GameObject newRoom;

        _startPosition.z = -_distanceBetweenRooms * _numberOfRoomsToStart;
        Vector3 newPosition = _startPosition;

        List<int> roomIndexes = SomeMath.CreateRandomUniqueIndexes(_numberOfRooms - _numberOfDuplicatedRooms, 0, _rooms.Length);
        List<int> roomRotationCoefficients = SomeMath.CreateRandomIndexes(_numberOfRooms - _numberOfDuplicatedRooms, 0, 4);

        for (int i = 0; i < _numberOfRooms - _numberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(_rooms[roomIndexes[i]]);
            if (_isRotate)
            {
                newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, roomRotationCoefficients[i] * 90)); //0, 90, 180, 270
            }
            _usedRooms.Add(newRoom);
        }
        for (int i = 0; i < _numberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(_usedRooms[i + _numberOfRoomsToStart]);
            if (_isRotate)
            {
                newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, roomRotationCoefficients[i + _numberOfRoomsToStart] * 90)); //0, 90, 180, 270
            }
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
        for (int i = 0; i < _usedRooms.Count - _numberOfDuplicatedRooms; i++)
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
        Room.OnRoomExit += ActivateRoom;
    }

    private void OnDisable()
    {
        Room.OnRoomExit -= ActivateRoom;
    }

    private void ActivateRoom(int currentRoomNumber)
    {
        if (currentRoomNumber < _usedRooms.Count - _numberOfDuplicatedRooms)
        {
            _usedRooms[currentRoomNumber].SetActive(false);
        }

        if (currentRoomNumber >= _usedRooms.Count - _numberOfRoomsVisibleToPlayer - _numberOfDuplicatedRooms)
        {
            currentRoomNumber = currentRoomNumber - (_usedRooms.Count - _numberOfDuplicatedRooms) + _numberOfRoomsToStart;
        }

        int numberOfRoomYouWantToActivate = currentRoomNumber + _numberOfRoomsVisibleToPlayer;
        _usedRooms[numberOfRoomYouWantToActivate].SetActive(true);
    }
}