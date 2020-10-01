using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //надає доступ до _keys (елемент інкапсуляції)
    public List<int> Keys { get => _keys; }

    //масиви елементів з яких складається перешкода (заповнюються через редактор)
    [SerializeField] private GameObject[]       _obstacleElements;

    //перешкода, яку буде продубльовано для збереження ілюзії неперервності
    [SerializeField] private DuplicateObstacle                   _duplicate;    

    //кількості різних об'єктів 
    private int                                 _totalNumberOfObstacleElement;
    private int                                 _numberOfObstacleElement;

    //списки чиї елементи нададуть форму першкоді
    private List<GameObject>                    _activeObstacleElement;

    //список-ключів, який знадобиться при зіткненні з гравцем 
    private List<int>                           _keys;

    //метод, який спрацьовує при завантаженні сцени
    private void Start()
    {
        _totalNumberOfObstacleElement = _obstacleElements.Length;

        _activeObstacleElement = new List<GameObject>();

        _keys = new List<int>();

        CreateObstacleShape();
    }

    //метод, що надає перешкоді певної форми
    private void CreateObstacleShape()
    {
        //генеруємо випадкове число - кількість елементів перешкоди, які буде активовано
        _numberOfObstacleElement = Random.Range(1, _totalNumberOfObstacleElement);

        //записуємо випадкові індекси у список _keys
        _keys = SomeMath.CreateRandomUniqueIndexes(_numberOfObstacleElement, 0, _totalNumberOfObstacleElement);

        //дублює елементи цієї перешкоди у спеціальну перешкоду, якщо твкова є
        if (_duplicate != null)
        {
            _duplicate.DuplicateForKey(_keys);
        }

        //заповнюємо список елементів перешкоди відповідними індексам елементами перешкоди, щоб активуваи їх
        for (int i = 0; i < _keys.Count; i++)
        {
            _activeObstacleElement.Add(_obstacleElements[_keys[i]]);
        }

        //активуємо елементи перешкоди
        SomeMath.ChangeListActivity(_activeObstacleElement, true);
    }

    //метод, який спрацьовує коли гравець виходить з трігера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SomeMath.ChangeListActivity(_activeObstacleElement, false);
            _activeObstacleElement.Clear();

            CreateObstacleShape();
        }
    }    
}