using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public static List<int> Keys { get { return _keys; } }

    //список-ключів
    private static List<int> _keys;

    //метод, що викликається при завантаженні сцени
    private void Start()
    {
        _keys = new List<int>();
    }

    //метод, що приймає ключ і додає чи видаляє його зі списку ключів
    public static void GiveKey(int key)
    {
        if (SomeMath.WhetherItIsContained(_keys, key))
        {
            _keys.Remove(key);
        }
        else
        {
            _keys.Add(key);
        }
    }
}