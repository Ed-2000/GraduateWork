using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    public void ActiveButton()
    {
        _button.SetActive(true);
    }

    public void DeactiveButton()
    {
        _button.SetActive(false);
    }
}
