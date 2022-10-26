using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    private Image _image = null;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ShowDeathScreen(float seconds, Action func)
    {
        StopAllCoroutines();
        StartCoroutine(ShowDeathScreenCoroutine(seconds, func));
    }

    IEnumerator ShowDeathScreenCoroutine(float seconds, Action func)
    {
        // Freeze time
        Time.timeScale = 0;
        Player.isGamePaused = true;

        FindObjectOfType<AudioManager>().Play("Death");

        // flash in
        float flashInDuration = seconds / 2;
        for (float t = 0; t <= flashInDuration; t += Time.unscaledDeltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0.0f, 1.0f, t / flashInDuration);
            _image.color = colorThisFrame;

            yield return null;
        }

        for (float t = 0; t <= 0.6f; t += Time.unscaledDeltaTime)
        {
            yield return null;
        }

        func();
        
        // Unfreeze time
        Time.timeScale = 1;
        Player.isGamePaused = false;

        // flash out
        float flashOutDuration = seconds / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.unscaledDeltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(1.0f, 0.0f, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        FindObjectOfType<AudioManager>().Stop("Death");

        _image.color = new Color(1, 1, 1, 0);

    }
}
