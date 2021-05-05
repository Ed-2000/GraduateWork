using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public delegate void PlayerSpeed();
    public static event PlayerSpeed OnMaxSpeed;

    public static float Speed
    {
        get => _speed;
        set
        {
            if (value >= 0 && value < _speedLimit)
            {
                _speed = value;
            }
            else if (_speed != _speedLimit && value >= _speedLimit)
            {
                _speed = _speedLimit;
                OnMaxSpeed?.Invoke();
            }
        }
    }

    public static List<int> Keys { get { return _keys; } }
    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreСhanged?.Invoke();
        }
    }

    public static int HighScore
    {
        get => _highScore;
        set
        {
            if (value > _highScore)
            {
                _highScore = value;
            }
        }
    }

    public delegate void PlayerScore();
    public static event PlayerScore OnScoreСhanged;

    [SerializeField] private static float _speed;

    private static int _score;
    private static int _highScore;
    //список-ключів
    private static List<int> _keys;
    private static float _speedLimit;

    private void Start()
    {
        _speed = 7f;
        _score = 0;
        _speedLimit = 17.5f;
        _keys = new List<int>();
    }

    private void OnEnable()
    {
        PlayerButton.OnButtonKey += GiveKey;
        CollisionWithObstacle.OnOvercameObstacle += AddSpeed;
        CollisionWithObstacle.OnDidNotOvercameObstacle += Stop;
        CollisionWithObstacle.OnDidNotOvercameObstacle += ChangeHighScore;
    }

    private void OnDisable()
    {
        PlayerButton.OnButtonKey -= GiveKey;
        CollisionWithObstacle.OnOvercameObstacle -= AddSpeed;
        CollisionWithObstacle.OnDidNotOvercameObstacle -= Stop;
        CollisionWithObstacle.OnDidNotOvercameObstacle -= ChangeHighScore;
    }

    //метод, що приймає ключ і додає чи видаляє його зі списку ключів
    private void GiveKey(int key)
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

    private void AddSpeed()
    {
        Speed += Speed * 0.025f;
    }

    private void Stop()
    {
        Speed = 0;
    }

    private void ChangeHighScore()
    {
        HighScore = Score;
    }
}