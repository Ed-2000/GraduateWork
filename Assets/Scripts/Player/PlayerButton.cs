using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerButton : MonoBehaviour, IPointerClickHandler
{
    public delegate void ButtonKey(int key);
    public static event ButtonKey OnButtonKey;

    public delegate void ClickOnButton();
    public static event ClickOnButton OnButtonClick;

    public delegate void DestroyedButton();
    public static event DestroyedButton OnButtonDestroyed;

    //елемент, який буде активовано(деактивовано) при натисканні
    [SerializeField] private GameObject _bodyElementObject;
    [SerializeField] private GameObject _destroyBodyElementObject;
    
    //ключ-значення кнопки на яку було натиснуо
    [SerializeField] private int _buttonKey;

    private Animator _animator;

    private void Start()
    {
        _animator = _bodyElementObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CollisionWithObstacle.OnOvercameObstacle += DeactivateAllElements;
        CollisionWithObstacle.OnDidNotOvercameObstacleWithList += DestroyActiveElements;
        RevertButton.OnRevert += OnRevertReaction;
    }

    private void OnDisable()
    {
        CollisionWithObstacle.OnOvercameObstacle -= DeactivateAllElements;
        CollisionWithObstacle.OnDidNotOvercameObstacleWithList -= DestroyActiveElements;
        RevertButton.OnRevert -= OnRevertReaction;
    }

    //метод, що реалізує реакцію на натискання
    public void OnPointerClick(PointerEventData eventData)
    {
        SmoothAppearanceOfTheObject();
        OnButtonKey?.Invoke(_buttonKey);
        OnButtonClick?.Invoke();
    }

    private void OnRevertReaction()
    {
        SmoothAppearanceOfTheObject();
        OnButtonKey?.Invoke(_buttonKey);
        OnButtonClick?.Invoke();
    }

    //активує анімацію 
    public void SmoothAppearanceOfTheObject()
    {
        bool stateOfAnimation = _animator.GetBool("IsIncrease");
        _animator.SetBool("IsIncrease", !stateOfAnimation);
    }

    //метод, що "вимикає" всі елементи тіла гравця
    private void DeactivateAllElements()
    {
        if (_animator.GetBool("IsIncrease"))
        {
            _animator.SetBool("IsIncrease", false);
            OnButtonKey?.Invoke(_buttonKey);
        }
    }

    //метод, що "ламає" всі активні елементи тіла гравця
    private void DestroyActiveElements(List<int> list)
    {
        if (SomeMath.WhetherItIsContained(list, _buttonKey))
        {
            _bodyElementObject.SetActive(false);
            _destroyBodyElementObject.SetActive(true);
            OnButtonDestroyed?.Invoke();
        }
    }
}