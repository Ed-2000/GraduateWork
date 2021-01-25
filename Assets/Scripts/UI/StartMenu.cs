using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public delegate void ClickOnButton();
    public static event ClickOnButton OnButtonClick;

    public void PlayOrigin()
    {
        OnButtonClick?.Invoke();
        SceneManager.LoadScene(1);
    }

    public void PlayDarkMode()
    {
        OnButtonClick?.Invoke();
        SceneManager.LoadScene(2);
    }
    

    public void OpenSettingsPanel()
    {
        OnButtonClick?.Invoke();
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        OnButtonClick?.Invoke();
        Application.Quit();
    }
}
