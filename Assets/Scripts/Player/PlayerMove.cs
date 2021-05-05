using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3         _moveVector;
    private Transform      _transform;

    private void Start()
    {
        _moveVector = Vector3.forward;
        _transform = transform;
    }

    private void Update()
    {
        //забезпечує рух гравця вперед 
        _transform.Translate(_moveVector * PlayerData.Speed * Time.deltaTime);
    }
}