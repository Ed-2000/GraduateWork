using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private float _lerpTime;
    [SerializeField] private Color[] _colors;

    private Light _light;
    private int _colorsLength;

    private void Start()
    {
        _light = GetComponent<Light>();
        _colorsLength = _colors.Length;

        StartCoroutine("СolorСycle");
    }

    private IEnumerator СolorСycle()
    {
        float t = 0f;
        int colorIndex = 0;
        float _lerpTimeDT;

        while (true)
        {
            _lerpTimeDT = _lerpTime * Time.deltaTime;

            _light.color = Color.Lerp(_light.color, _colors[colorIndex], _lerpTimeDT);

            t = Mathf.Lerp(t, 1, _lerpTimeDT);
            if (t >= 0.95f)
            {
                t = 0;
                colorIndex++;

                if (colorIndex >= _colorsLength)
                    colorIndex = 0;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}