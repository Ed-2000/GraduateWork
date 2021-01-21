using UnityEngine;

public class Score : MonoBehaviour
{
    public const int addToScore = 10;

    private void OnEnable()
    {
        CollisionWithObstacle.OnOvercameObstacle += AddScore;
    }

    private void OnDisable()
    {
        CollisionWithObstacle.OnOvercameObstacle -= AddScore;
    }

    private void AddScore()
    {
        PlayerData.Score += addToScore;
    }
}