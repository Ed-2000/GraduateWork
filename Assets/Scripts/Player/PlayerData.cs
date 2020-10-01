using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static float Speed
    {
        get => _speed;
        set
        {
            if (value >= 0 && value < _speedLimit)
            {
                _speed = value;
            }
        }
    }

    [SerializeField] private static float _speed;

    private static float _speedLimit = 15f;

    private void Start()
    {
        _speed = 7.5f;
    }
}