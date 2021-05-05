using UnityEngine;

public class StartMenu : MonoBehaviour
{ 
    public delegate void ClickOnButton();
    public static event ClickOnButton OnButtonClick;
    public delegate void SceneLoader(int sceneNumber);
    public static event SceneLoader LoadScene;

    public void Play()
    {
        Click();
        LoadScene?.Invoke(1);
    }    

    public void Click()
    {
        OnButtonClick?.Invoke();
    }

    public void Quit()
    {
        Click();
        Application.Quit();
    }
}