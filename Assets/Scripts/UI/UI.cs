using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

public class UI : MonoBehaviour
{
    private static GameObject _deadPanel;

    private void Start()
    {
        _deadPanel = GameObject.FindGameObjectWithTag("DeadPanel");
        if (_deadPanel.activeSelf)
        {
            _deadPanel.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public static void ActiveDeadMenu()
    {
        _deadPanel.SetActive(true);
    }
}
