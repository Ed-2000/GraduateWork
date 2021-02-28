using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _button; 
    [SerializeField] private AudioSource _collisionEfects; 

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

        //для стартового меню 
        StartMenu.OnButtonClick += ActivateButtonClickSound;
    }

    private void OnDisable()
    {
        PlayerButton.OnButtonClick -= ActivateButtonClickSound;
        PlayerButton.OnButtonDestroyed -= ActivateDestroyedSound;
        CollisionWithObstacle.OnDidNotOvercameObstacle -= ActivateLoseSound;
        CollisionWithObstacle.OnOvercameObstacle -= ActivateVictorySound;

        //для стартового меню 
        StartMenu.OnButtonClick -= ActivateButtonClickSound;
    }

    private void ActivateButtonClickSound()
    {
        _button.PlayOneShot(_buttonClickSound);
    }

    private void ActivateLoseSound()
    {
        _collisionEfects.PlayOneShot(_loseSound);
    }

    private void ActivateDestroyedSound()
    {
        _collisionEfects.PlayOneShot(_buttonDestroySound);
    }

    private void ActivateVictorySound()
    {
        _collisionEfects.PlayOneShot(_victorySound);
    }
}
