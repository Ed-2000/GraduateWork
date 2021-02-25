using UnityEngine;

public class SubmissiveToRhythmWalls : MonoBehaviour
{
    [SerializeField] private GameObject[] _soundPanels;

    private void OnEnable()
    {
        MasterOfRhythm.OnVolumeHasChanged += Resize;
    }

    private void OnDisable()
    {
        MasterOfRhythm.OnVolumeHasChanged -= Resize;
    }

    private void Resize(float volume, bool IsVolumeUp)
    {
        if (IsVolumeUp)
        {
            for (int i = 0; i < volume * _soundPanels.Length; i++)
            {
                _soundPanels[i].SetActive(true);
            }
        }
        else
        {
            for (int i = _soundPanels.Length - 1; i >= volume * _soundPanels.Length; i--)
            {
                _soundPanels[i].SetActive(false);
            }
        }
    }
}