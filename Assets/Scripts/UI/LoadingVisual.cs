using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingVisual : MonoBehaviour
{
    [SerializeField] private Animator _loadScreenAnimator;

    private int _sceneNumber;
    private AsyncOperation _asyncLoad;

    private void OnEnable()
    {
        StartMenu.LoadScene += Load;
        UI.LoadScene += Load;
    }

    private void OnDisable()
    {
        StartMenu.LoadScene -= Load;
        UI.LoadScene -= Load;
    }

    private void Load(int sceneNumber)
    {
        _sceneNumber = sceneNumber;
        LoadScreenActivator();
    }

    private void StartLoading()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        
        _asyncLoad = SceneManager.LoadSceneAsync(_sceneNumber);
        _asyncLoad.allowSceneActivation = false;

        while (true)
        {
            if (_asyncLoad.progress >= 0.9f)
            {
                LoadScreenDeactivator();
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void LoadScreenActivator()
    {
        _loadScreenAnimator.SetTrigger("StartLoading");
    }

    private void LoadScreenDeactivator()  
    {
        _loadScreenAnimator.SetTrigger("EndLoading");
    }

    private void ActivatedScene()
    {
        _asyncLoad.allowSceneActivation = true;
    }
}