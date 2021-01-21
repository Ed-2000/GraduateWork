using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObstacle : MonoBehaviour
{
    public delegate void PlayerStatus();
    public delegate void PlayerStatusWithList(List<int> list);

    public static event PlayerStatus OnOvercameObstacle;
    public static event PlayerStatus OnDidNotOvercameObstacle;
    public static event PlayerStatusWithList OnDidNotOvercameObstacleWithList;

    [SerializeField] GameObject[]   _playerBodyElements;

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();

        //не виконувати далі, якщо на об'єкті other немає скрипта Obstacle
        if (obstacle == null)
            return;

        //
        List<int> playerKeys = PlayerData.Keys;
        List<int> obstacleKeysOrigin = obstacle.Keys;
        List<int> obstacleKeys = PrepareForComparison(obstacleKeysOrigin);

        //спрацює якщо ключі збігаються
        if (SomeMath.ComparisonLists(playerKeys, obstacleKeys))
        {
            OnOvercameObstacle?.Invoke();
        }
        //спрацює якщо ключі не збігаються
        else
        {
            OnDidNotOvercameObstacle?.Invoke();
            OnDidNotOvercameObstacleWithList?.Invoke(SomeMath.CommonListItems(playerKeys, obstacleKeysOrigin));
        }
    }

    //метод, що готує список активних елементів перешкоди чи гравця до порівняння
    private List<int> PrepareForComparison(List<int> originalKeys)
    {
        List<int> newKeys = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            newKeys.Add(i);
        }

        for (int i = 0; i < originalKeys.Count; i++)
        {
            newKeys.Remove(originalKeys[i]);
        }

        return newKeys;
    }
}