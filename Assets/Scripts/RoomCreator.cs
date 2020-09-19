using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _blocks;
    [SerializeField] private bool _IsWall;

    private void Start()
    {
        foreach (var block in _blocks)
        {
            Vector3 newPosition = block.transform.position;

            if (_IsWall)
            {
                newPosition.x = Random.Range(newPosition.x - 2, newPosition.x + 2);
            }
            else
            {
                newPosition.y = Random.Range(newPosition.y - 2, newPosition.y + 2);
            }

            newPosition.x = Mathf.Round(newPosition.x);
            block.transform.position = newPosition;
        }
    }
}