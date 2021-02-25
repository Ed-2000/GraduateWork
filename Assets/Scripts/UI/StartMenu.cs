using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{ 
    public delegate void ClickOnButton();
    public static event ClickOnButton OnButtonClick;
    public delegate void SceneLoader(int sceneNumber);
    public static event SceneLoader LoadScene;

    public void Play()
    {
        OnButtonClick?.Invoke();
        LoadScene?.Invoke(1);
    }    

    public void OpenSettingsPanel()
    {
        OnButtonClick?.Invoke();
    }

    public void Quit()
    {
        OnButtonClick?.Invoke();
        Application.Quit();
    }
}