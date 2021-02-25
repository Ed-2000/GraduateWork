using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _blocks;

    [SerializeField] private bool _IsResize = false;

    [SerializeField] private float _resizeLimit = 2f;

    [SerializeField] private bool _IsXMove = false;
    [SerializeField] private bool _IsYMove = false;
    [SerializeField] private bool _IsZMove = false;

    [SerializeField] private float _moveLimitX = 2f;
    [SerializeField] private float _moveLimitY = 2f;
    [SerializeField] private float _moveLimitZ = 2f;


    private void Start()
    {
        foreach (var block in _blocks)
        {
            Vector3 newPosition = block.transform.position;
            Vector3 newScale = block.transform.localScale;

            if (_IsResize)
            {
                float newRibLength = Random.Range(newScale.x - _resizeLimit, newScale.x + _resizeLimit);
                newScale.x = newRibLength;
                newScale.y = newRibLength;
                newScale.z = newRibLength;
            }

            if (_IsXMove)
            {
                newPosition.x = Random.Range(newPosition.x - _moveLimitX, newPosition.x + _moveLimitX);
            }

            if (_IsYMove)
            {
                newPosition.y = Random.Range(newPosition.y - _moveLimitY, newPosition.y + _moveLimitY);
            }

            if (_IsZMove)
            {
                newPosition.z = Random.Range(newPosition.z - _moveLimitZ, newPosition.z + _moveLimitZ);
            }
            else
            {
                newPosition.z = Random.Range(newPosition.z - 0.1f, newPosition.z + 0.1f);
            }

            block.transform.position = newPosition;
            block.transform.localScale = newScale;
            block.isStatic = true;
        }
    }
}