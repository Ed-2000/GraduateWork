using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //масиви елементів з яких складається перешкода (заповнюються через редактор)
    [SerializeField] private GameObject[]       _obstacleElementNotForTouch;
    [SerializeField] private GameObject[]       _obstacleElementToTouch;

    //кількості різних об'єктів 
    private int                                 _totalNumberOfObstacleElement;
    private int                                 _numberOfObstacleElementNotForTouch;
    private int                                 _numberOfObstacleElementToTouch;

    //списки чиї елементи нададуть форму першкоді
    private List<GameObject>                    _activeObstacleElementNotForTouch;
    private List<GameObject>                    _activeObstacleElementToTouch;
    
    //метод, який спрацьовує при завантаженні сцени
    private void Start()
    {
        _totalNumberOfObstacleElement = _obstacleElementNotForTouch.Length;

        _activeObstacleElementNotForTouch = new List<GameObject>();
        _activeObstacleElementToTouch = new List<GameObject>();
    }

    //метод, який спрацьовує коли гравець входить в трігер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CreateObstacleShape();
        }
    }

    //метод, який спрацьовує коли гравець виходить з трігера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeListActivity(_activeObstacleElementNotForTouch, false);
            _activeObstacleElementNotForTouch.Clear();

            ChangeListActivity(_activeObstacleElementToTouch, false);
            _activeObstacleElementToTouch.Clear();
        }
    }

    //метод, що надає перешкоді певної форми
    private void CreateObstacleShape()
    {
        //генеруємо випадкове число - кількість елементів перешкоди, яких не варто торкатися
        _numberOfObstacleElementNotForTouch = Random.Range(1, _totalNumberOfObstacleElement);
        _numberOfObstacleElementToTouch = _totalNumberOfObstacleElement - _numberOfObstacleElementNotForTouch;

        //створюємо список у який записуємо випадкові індекси
        List<int> indexes = new List<int>();
        indexes = CreateRandomUniqueIndexes(_numberOfObstacleElementNotForTouch, 0, _totalNumberOfObstacleElement);

        //заповнюємо список елементів перешкоди яких не варто торкатися індексами, щоб активуваи їх
        for (int i = 0; i < _numberOfObstacleElementNotForTouch; i++)
        {
            _activeObstacleElementNotForTouch.Add(_obstacleElementNotForTouch[indexes[i]]);
        }

        //активуємо елементи перешкоди яких не варто торкатися
        ChangeListActivity(_activeObstacleElementNotForTouch, true);

        //записуємо у список індекси, що не ввійшли в попередній список
        indexes = PApaparam(_numberOfObstacleElementToTouch, 0, _totalNumberOfObstacleElement, indexes);

        //заповнюємо список елементів перешкоди яких варто торкнутися індексами, щоб активуваи їх
        for (int i = 0; i < _numberOfObstacleElementToTouch; i++)
        {
            _activeObstacleElementToTouch.Add(_obstacleElementToTouch[indexes[i]]);
        }

        //активуємо елементи перешкоди яких варто торкнутися
        ChangeListActivity(_activeObstacleElementToTouch, true);

    }

    //метод, що перевіряє чи міститься число number в списку list
    private bool WhetherItContains(List<int> list, int number)
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

    //меетод, що створює список випадкових і неповторних (в межах списку) індексів 
    private List<int> CreateRandomUniqueIndexes(int numberOfIndexes, int min, int max)
    {
        if (numberOfIndexes <= Mathf.Abs(max - min) && min < max)
        {
            List<int> list = new List<int>();

            for (int i = min; i < max; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < numberOfIndexes * 2; i++)
            {
                var number = list[Random.Range(0, list.Count)];
                list.Remove(number);
                list.Add(number);
            }

            list.RemoveRange(numberOfIndexes, list.Count - numberOfIndexes);
            
            return list;
        }
        else
        {
            print("ObstacleScript!!!");
            return null;
        }
    }

    //меетод, що створює список випадкових і неповторних (в межах списку) індексів, які не входять у список oldList
    private List<int> PApaparam(int numberOfIndexes, int min, int max, List<int> oldList)
    {
        if (Mathf.Abs(max - min) > oldList.Count && min < max)
        {
            List<int> list = new List<int>();

            for (int i = min; i < max; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < oldList.Count; i++)
            {
                list.Remove(oldList[i]);
            }

            return list;
        }
        else
        {
            print("ObstacleScript!!!");
            return null;
        }
    }

    //метод, що активує чи деактивує об'єкти з переданого списку
    private void ChangeListActivity(List<GameObject> obstacles, bool stateOfActivity)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(stateOfActivity);
        }
    }
}