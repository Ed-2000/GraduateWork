using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _lastObstaclePrefab;
    [SerializeField] private int _numberOfObstacles;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Transform _teleport;

    private Vector3 _finishPosition;
    private List<GameObject> _usedObstacles;

    private void Start()
    {
        _usedObstacles = new List<GameObject>();

        _finishPosition = new Vector3();
        _finishPosition = _startPosition;
        _finishPosition.z = _teleport.position.z + _startPosition.z;

        float distanceBetweenObstacles = (_finishPosition.z - _startPosition.z) / (_numberOfObstacles - 1);

        GameObject newObstacle;

        for (int i = 0; i < _numberOfObstacles - 1; i++)
        {
            newObstacle = Instantiate(_obstaclePrefab);
            newObstacle.transform.position = _startPosition + new Vector3(0, 0, distanceBetweenObstacles * i);

            _usedObstacles.Add(newObstacle);
        }

        newObstacle = Instantiate(_lastObstaclePrefab);
        newObstacle.transform.position = _finishPosition;
        _usedObstacles.Add(newObstacle);

        _usedObstacles[0].GetComponent<Obstacle>()._duplicate = _usedObstacles[_usedObstacles.Count-1].GetComponent<DuplicateObstacle>();

        for (int i = 0; i < _usedObstacles.Count; i++)
        {
            _usedObstacles[i].transform.parent = gameObject.transform;
        }
    }
}
