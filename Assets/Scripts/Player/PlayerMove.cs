using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3         _moveVector;
    private PlayerData      _data;

    private void Start()
    {
        _moveVector = Vector3.forward;
        _data = new PlayerData();
    }

    private void Update()
    {    
        //забезпечує рух гравця вперед 
        transform.Translate(_moveVector * _data.Speed * Time.deltaTime);
    }
}