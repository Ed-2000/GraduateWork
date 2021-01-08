using System.Collections.Generic;
using UnityEngine;

public class RoomsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _teleport;
    [SerializeField] private GameObject[] _Rooms;
    [SerializeField] private Vector3 _StartPosition;
    [SerializeField] private float _DistanceBetweenRooms;
    [SerializeField] private int _NumberOfRooms;
    [SerializeField] private int _NumberOfDuplicatedRooms;
    [SerializeField] private int _NumberOfRoomsToStart;

    private void Start()
    {
        List<GameObject> usedRooms = new List<GameObject>();

        GameObject newRoom;
        Vector3 newPosition = _StartPosition;

        List<int> roomIndexes = SomeMath.CreateRandomUniqueIndexes(_NumberOfRooms - _NumberOfDuplicatedRooms, 0, _Rooms.Length);
        List<int> roomRotationCoefficients = SomeMath.CreateRandomIndexes(_NumberOfRooms - _NumberOfDuplicatedRooms, 0, 4);

        for (int i = 0; i < _NumberOfRooms - _NumberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(_Rooms[roomIndexes[i]]);
            newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, roomRotationCoefficients[i] * 90)); //0, 90, 180, 270
            usedRooms.Add(newRoom);
        }
        for (int i = 0; i < _NumberOfDuplicatedRooms; i++)
        {
            newRoom = Instantiate(usedRooms[i + _NumberOfRoomsToStart]);
            newRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, roomRotationCoefficients[i + _NumberOfRoomsToStart] * 90)); //0, 90, 180, 270
            usedRooms.Add(newRoom);
        }

        for (int i = 0; i < usedRooms.Count; i++)
        {
            newPosition.z = i * _DistanceBetweenRooms;            
            usedRooms[i].transform.position = _StartPosition + newPosition;
            usedRooms[i].transform.parent = gameObject.transform;
        }

        newPosition = _teleport.transform.position;
        newPosition.z = usedRooms[_NumberOfRooms - _NumberOfDuplicatedRooms].transform.position.z;
        _teleport.transform.position = newPosition;
    }
}