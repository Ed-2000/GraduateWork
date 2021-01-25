using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    private void OnEnable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle += DeactiveButton;
    }

    private void OnDisable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle -= DeactiveButton;
    }

    public void ActiveButton()
    {
        _button.SetActive(true);
    }

    public void DeactiveButton()
    {
        _button.SetActive(false);
    }
}