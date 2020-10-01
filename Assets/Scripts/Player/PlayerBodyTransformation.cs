using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class PlayerBodyTransformation : MonoBehaviour, IPointerClickHandler
{
    private float pra = 0.2f;

    //елемент, який буде активовано(деактивовано) при натисканні
    [SerializeField] private GameObject     _bodyElementObject;

    //ключ-значення кнопки на яку було натиснуо
    [SerializeField] private int            _buttonKey;

    //метод, що реалізує реакцію на натискання
    public void OnPointerClick(PointerEventData eventData)
    {
        SomeMath.ChangePlayerBodyElementActivity(_bodyElementObject, _buttonKey);
        SmoothAppearanceOfTheObject(_bodyElementObject);
    }

    public void SmoothAppearanceOfTheObject(GameObject element)
    {
        StartCoroutine(UpScale(element));
    }

    IEnumerator UpScale(GameObject element)
    {
        Vector3 Scale = element.transform.localScale;

        for (float i = 0; i <= 1; i += pra)
        {
            Scale += new Vector3(pra, pra, pra);
            element.transform.localScale = Scale;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator LowScale(GameObject element)
    {
        Vector3 Scale = element.transform.localScale;

        for (float i = 1; i >= 0; i -= 0.1f)
        {
            Scale -= new Vector3(0.1f, 0.1f, 0.1f);
            element.transform.localScale = Scale;
            yield return new WaitForSeconds(0.05f);
        }
    }
}