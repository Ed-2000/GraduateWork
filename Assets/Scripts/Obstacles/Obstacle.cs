using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<int> Keys { get => _keys; }
    public DuplicateObstacle Duplicate { get => duplicate; set => duplicate = value; }

    //масиви елементів з яких складається перешкода (заповнюються через редактор)
    [SerializeField] private GameObject[] _obstacleElements;
    [SerializeField] private GameObject[] _dopObstacleElements;

    //перешкода, яку буде продубльовано для збереження ілюзії неперервності
    private DuplicateObstacle duplicate;

    //кількості різних об'єктів 
    private int _totalNumberOfObstacleElement;
    private int _numberOfObstacleElement;
    private float _timeToChangeObstacle;
    private bool _IsStopped;

    //списки чиї елементи нададуть форму першкоді
    private List<GameObject> _activeObstacleElement;
    private List<GameObject> _notActiveObstacleElement;

    //список-ключів, який знадобиться при зіткненні з гравцем 
    private List<int> _keys;

    //метод, який спрацьовує при завантаженні сцени
    private void Start()
    {
        _totalNumberOfObstacleElement = _obstacleElements.Length;
        _activeObstacleElement = new List<GameObject>();
        _notActiveObstacleElement = new List<GameObject>();
        _keys = new List<int>();
        _timeToChangeObstacle = 2.5f;
        _IsStopped = false;

        CreateObstacleShape();
    }

    private void OnEnable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle += Stop;
        CollisionWithObstacle.OnDidNotOvercameObstacleWithList += DestroyActiveElements;
    }

    private void OnDisable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle -= Stop;
        CollisionWithObstacle.OnDidNotOvercameObstacleWithList -= DestroyActiveElements;
    }

    //метод, що надає перешкоді певної форми
    private void CreateObstacleShape()
    {
        if (_IsStopped)
        {
            return;
        }

        SomeMath.ChangeListActivity(_activeObstacleElement, false);
        SomeMath.ChangeListActivity(_notActiveObstacleElement, true);
        _activeObstacleElement.Clear();
        _notActiveObstacleElement.Clear();

        //генеруємо випадкове число - кількість елементів перешкоди, які буде активовано
        _numberOfObstacleElement = Random.Range(1, _totalNumberOfObstacleElement);

        //записуємо випадкові індекси у список _keys
        _keys = SomeMath.CreateRandomUniqueIndexes(_numberOfObstacleElement, 0, _totalNumberOfObstacleElement);

        //дублює елементи цієї перешкоди у спеціальну перешкоду, якщо тaкова є
        if (Duplicate != null)
        {
            Duplicate.DuplicateForKey(_keys);
        }

        //заповнюємо список елементів перешкоди відповідними індексам елементами перешкоди, щоб активувати їх
        for (int i = 0; i < _keys.Count; i++)
        {
            _activeObstacleElement.Add(_obstacleElements[_keys[i]]);
            _notActiveObstacleElement.Add(_dopObstacleElements[_keys[i]]);
        }

        //активуємо елементи перешкоди
        SomeMath.ChangeListActivity(_activeObstacleElement, true);
        SomeMath.ChangeListActivity(_notActiveObstacleElement, false);
    }

    //метод, який спрацьовує коли гравець входить в трігер
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Invoke("CreateObstacleShape", _timeToChangeObstacle);
        }
    }

    //метод, що вимикає всі активні елементи тіла перешкоди
    private void DestroyActiveElements(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            _obstacleElements[list[i]].SetActive(false);
            _dopObstacleElements[list[i]].SetActive(true);
        }
    }

    private void Stop()
    {
        _IsStopped = true;
    }
}