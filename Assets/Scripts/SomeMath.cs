using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMath : MonoBehaviour
{
    //метод, що перевіряє чи міститься в списку list число number
    public static bool WhetherItIsContained(List<int> list, int number)
    {
        bool res = false;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == number)
            {
                res = true;
            }
        }

        return res;
    }

    //метод, що створює список випадкових індексів 
    public static List<int> CreateRandomIndexes(int numberOfIndexes, int min, int max)
    {
        if (min < max)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < numberOfIndexes; i++)
            {
                list.Add(Random.Range(min, max));
            }

            return list;
        }
        else
        {
            return null;
        }
    }

    //метод, що створює список випадкових і неповторних (в межах списку) індексів 
    public static List<int> CreateRandomUniqueIndexes(int numberOfIndexes, int min, int max)
    {
        if (numberOfIndexes <= Mathf.Abs(max - min) && min < max)
        {
            List<int> list = new List<int>();

            for (int i = min; i < max; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < Mathf.Abs(max - min) * 2; i++)
            {
                var number = Random.Range(0, list.Count);
                list.Add(list[number]);
                list.RemoveAt(number);
            }

            list.RemoveRange(numberOfIndexes, list.Count - numberOfIndexes);

            return list;
        }
        else
        {
            return null;
        }
    }

    //метод, що активує чи деактивує об'єкти з переданого списку
    public static void ChangeListActivity(List<GameObject> obstacles, bool stateOfActivity)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(stateOfActivity);
        }
    }

    //метод, що додає ключ до списку ключів гравця
    public static void ChangePlayerBodyElementActivity(int key)
    {
        PlayerKey.GiveKey(key);
    }
}