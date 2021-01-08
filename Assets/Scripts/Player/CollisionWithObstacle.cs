using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObstacle : MonoBehaviour
{
    [SerializeField] GameObject[]   _playerBodyElements;

    private void OnTriggerEnter(Collider other)
    {
        //не виконувати далі, якщо на об'єкті other немає скрипта Obstacle
        if (other.GetComponent<Obstacle>() == null)
        {
            return;
        }

        //
        List<int> playerKeys = PlayerKey.Keys;
        List<int> obstacleKeys = PrepareForComparison(other.GetComponent<Obstacle>().Keys);
        
        //спрацює якщо ключі збігаються
        if (ComparisonLists(playerKeys, obstacleKeys))
        {
            for (int i = 0; i < obstacleKeys.Count; i++)
            {
                SomeMath.ChangePlayerBodyElementActivity(obstacleKeys[i]);
            }
        }
        //спрацює якщо ключі не збігаються
        else
        {
            PlayerData.Speed = 0;
            UI.ActiveDeadMenu();
        }
    }

    //метод, що перевіряє списки на ідентичність елементів
    private bool ComparisonLists(List<int> list1, List<int> list2)
    {
        bool res = true;

        if (list1.Count != list2.Count)
        {
            res = false;
        }
        else
        {
            list1.Sort();
            list2.Sort();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    res = false;
                }
            }
        }

        return res;
    }

    //метод, що готує список активних елементів перешкоди чи гравця до порівняння
    private List<int> PrepareForComparison(List<int> originalKey)
    {
        List<int> newKey = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            newKey.Add(i);
        }

        for (int i = 0; i < originalKey.Count; i++)
        {
            newKey.Remove(originalKey[i]);
        }

        return newKey;
    }
}