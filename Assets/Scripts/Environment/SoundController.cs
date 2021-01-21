using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _buttonClickSound; 
    [SerializeField] private AudioClip _buttonDestroySound; 
    [SerializeField] private AudioClip _loseSound; 
    [SerializeField] private AudioClip _victorySound;

    private void OnEnable()
    {
        PlayerButton.OnButtonClick += ActivateButtonClickSound;
        PlayerButton.OnButtonDestroyed += ActivateDestroyedSound;
        CollisionWithObstacle.OnDidNotOvercameObstacle += ActivateLoseSound;
        CollisionWithObstacle.OnOvercameObstacle += ActivateVictorySound;
    }

    private void OnDisable()
    {
        PlayerButton.OnButtonClick -= ActivateButtonClickSound;
        PlayerButton.OnButtonDestroyed -= ActivateDestroyedSound;
        CollisionWithObstacle.OnDidNotOvercameObstacle -= ActivateLoseSound;
        CollisionWithObstacle.OnOvercameObstacle -= ActivateVictorySound;
    }

    private void ActivateButtonClickSound()
    {
        _audioSource.PlayOneShot(_buttonClickSound);
    }

    private void ActivateLoseSound()
    {
        _audioSource.PlayOneShot(_loseSound);
    }

    private void ActivateDestroyedSound()
    {
        _audioSource.PlayOneShot(_buttonDestroySound);
    }

    private void ActivateVictorySound()
    {
        _audioSource.PlayOneShot(_victorySound);
    }
}
