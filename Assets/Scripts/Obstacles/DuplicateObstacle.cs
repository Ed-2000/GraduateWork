using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateObstacle : MonoBehaviour
{
    //список елементів перешкоди
    [SerializeField] GameObject[]   _obstacleElements;

    //метод, що активує елементи перешкоди за переданим ключем
    public void DuplicateForKey(List<int> keys)
    {
        //деактивуємо всі активні елементи перешкоди, якщо такові є
        for (int i = 0; i < _obstacleElements.Length; i++)
        {
            if (_obstacleElements[i].activeSelf)
            {
                _obstacleElements[i].SetActive(false);
            }
        }

        //активуємо елементи перешкоди за ключем
        for (int i = 0; i < keys.Count; i++)
        {
            _obstacleElements[keys[i]].SetActive(true);
        }
    }
}
