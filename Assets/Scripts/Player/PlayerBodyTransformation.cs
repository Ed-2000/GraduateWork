using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerBodyTransformation : MonoBehaviour, IPointerClickHandler
{
    //елемент, який буде активовано(деактивовано) при натисканні
    [SerializeField] private GameObject     _bodyElementObject;

    //ключ-значення кнопки на яку було натиснуо
    [SerializeField] private int            _buttonKey;

    private Animator _animator;

    private void Start()
    {
        _animator = _bodyElementObject.GetComponent<Animator>();
    }

    //метод, що реалізує реакцію на натискання
    public void OnPointerClick(PointerEventData eventData)
    {
        SomeMath.ChangePlayerBodyElementActivity(_buttonKey);
        SmoothAppearanceOfTheObject(_bodyElementObject);
    }

    //активує анімацію 
    public void SmoothAppearanceOfTheObject(GameObject element)
    {
        if (!_animator.GetBool("IsIncrease"))
        {
            _animator.SetBool("IsIncrease", true);
        }
        else
        {
            _animator.SetBool("IsIncrease", false);
        }
    }
}