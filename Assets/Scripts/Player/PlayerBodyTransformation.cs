using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBodyTransformation : MonoBehaviour
{
    [SerializeField] private Color _buttonColorNew;

    private Color _buttonColorDefault;
    private Graphic _buttonGraphic;

    private void Start()
    {
        _buttonGraphic = gameObject.GetComponent<Graphic>();
        _buttonColorDefault = _buttonGraphic.color;
    }

    public void ChangeaActivity(GameObject GO)
    {
        GO.SetActive(!GO.activeSelf);

        if (GO.activeSelf)
        {
            _buttonGraphic.color = _buttonColorDefault;
        }
        else
        {
            _buttonGraphic.color = _buttonColorNew;
        }
    }
}
