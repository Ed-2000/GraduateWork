using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _destroyedBody;

    public void ReplaceBodyWithDestroyBody()
    {
        _body.SetActive(false);
        _destroyedBody.SetActive(true);
    }
}