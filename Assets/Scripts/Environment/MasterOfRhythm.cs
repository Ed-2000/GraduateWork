using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterOfRhythm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    //поточна позиція пісні
    private float _songPosition;

    //скільки часу пройшло від початку пісні
    private float _dspSongTime;

    private int _quality = 100;
    private int _sampleCount = 0;
    private float[] _waveFormArray;
    private float[] _samples;

    private float _clipLength;
    private float _samplesInSecond;
    private float _maxVolume;

    private float _lastVolume;
    [SerializeField] private int _numberOfVolumeInfoUpdate;
    [SerializeField] private int _steps;

    public delegate void SoundVolume(float currentVolume, bool IsVolumeUp);
    public static event SoundVolume OnVolumeHasChanged;

    void Start()
    {
        int freq = _audioSource.clip.frequency;
        _sampleCount = freq / _quality;

        _lastVolume = 0;

        _samples = new float[_audioSource.clip.samples * _audioSource.clip.channels];
        _audioSource.clip.GetData(_samples, 0);

        // создаем массив, куда запишем усредненные сэмплы. Из него мы будем рисовать волну
        _waveFormArray = new float[(_samples.Length / _sampleCount)];

        //Дальше проходим по нашему массиву и находим среднее значение в каждой группе сэмплов

        for (int i = 0; i < _waveFormArray.Length; i++)
        {
            _waveFormArray[i] = 0;
            for (int j = 0; j < _sampleCount; j++)
            {
                //Abs тут использован для создания "красивой" и зеркально отраженной волны. См. ниже
                _waveFormArray[i] += Mathf.Abs(_samples[(i * _sampleCount) + j]);
            }

            _waveFormArray[i] /= _sampleCount;
        }

        _maxVolume = Mathf.Max(_waveFormArray);

        for (int i = 0; i < _waveFormArray.Length; i++)
        {
            _waveFormArray[i] = _waveFormArray[i] / _maxVolume;
        }

        _clipLength = _audioSource.clip.length;
        _samplesInSecond = _waveFormArray.Length / _clipLength;

        _dspSongTime = (float)AudioSettings.dspTime;
        _audioSource.Play();

        StartCoroutine(VolumeInfo());
    }

    private IEnumerator VolumeInfo()
    {
        while (true)
        {
            _songPosition = (float)(AudioSettings.dspTime - _dspSongTime);

            if (_songPosition >= _clipLength)
            {
                _dspSongTime = (float)AudioSettings.dspTime;
                _audioSource.Play();
                _songPosition = (float)(AudioSettings.dspTime - _dspSongTime);
            }

            StartCoroutine(VolumeElement(_waveFormArray[(int)(_samplesInSecond * _songPosition)]));

            yield return new WaitForSeconds(1.0f / _numberOfVolumeInfoUpdate);
        }
    }

    private IEnumerator VolumeElement(float volume)
    {
        for (int i = 1; i <= _steps; i++)
        {
            if (volume >=_lastVolume)
            {
                OnVolumeHasChanged?.Invoke(_lastVolume + (volume - _lastVolume) / _steps * i, true);
            }
            else
            {
                OnVolumeHasChanged?.Invoke(_lastVolume - (_lastVolume - volume) / _steps * i, false);
            }

            _lastVolume = volume;

            yield return new WaitForSeconds(1.0f / _numberOfVolumeInfoUpdate / _steps); 
        }
    }
}