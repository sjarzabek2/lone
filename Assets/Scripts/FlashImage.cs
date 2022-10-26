using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    Image _image = null;
    Coroutine _currentFlashRoutine = null;

    public bool _isFlashing = false;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// Starts flash coroutine without delay
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="maxAlpha"></param>
    /// <param name="newColor"></param>
    public void StartFlash(float seconds, float maxAlpha, Color newColor)
    {
        _image.color = newColor;

        // Colors in Unity are set between 0-1, not 0-255
        maxAlpha = Mathf.Clamp(maxAlpha, 0.0f, 1.0f);

        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(seconds, maxAlpha));
    }

    public void StartFadeOut(float seconds, Color newColor)
    {
        _image.color = newColor;

        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(FadeOut(seconds, newColor));
    }

    /// <summary>
    /// Starts flash coroutine with <paramref name="waitPeriod"/> delay at <paramref name="maxAlpha"/>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="maxAlpha"></param>
    /// <param name="newColor"></param>
    /// <param name="waitPeriod"></param>
    public void StartFlash(float seconds, float maxAlpha, Color newColor, float waitPeriod)
    {
        _image.color = newColor;

        // Colors in Unity are set between 0-1, not 0-255
        maxAlpha = Mathf.Clamp(maxAlpha, 0.0f, 1.0f);

        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(seconds, maxAlpha, waitPeriod));
    }
    public void StartFlash(float seconds, float maxAlpha, Color newColor, float waitPeriod, Action func)
    {
        _image.color = newColor;

        // Colors in Unity are set between 0-1, not 0-255
        maxAlpha = Mathf.Clamp(maxAlpha, 0.0f, 1.0f);

        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(seconds, maxAlpha, waitPeriod, func));
    }

    /// <summary>
    /// Flash screen in and immediately out
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="maxAlpha"></param>
    /// <returns></returns>
    IEnumerator Flash(float seconds, float maxAlpha)
    {
        // flash in
        float flashInDuration = seconds / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0.0f, maxAlpha, t / flashInDuration);
            _image.color = colorThisFrame;

            yield return null;
        }

        // flash out
        float flashOutDuration = seconds / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0.0f, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);
    }

    /// <summary>
    /// Flash screen in, wait for <paramref name="waitPeriod"/> seconds and then flash out
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="maxAlpha"></param>
    /// <param name="waitPeriod"></param>
    /// <returns></returns>
    IEnumerator Flash(float seconds, float maxAlpha, float waitPeriod)
    {
        // flash in
        float flashInDuration = seconds / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0.0f, maxAlpha, t / flashInDuration);
            _image.color = colorThisFrame;

            yield return null;
        }

        // indication that the screen is max flashed
        _isFlashing = true;

        // wait
        for (float t = 0; t <= waitPeriod; t += Time.deltaTime)
        {
            yield return null;
        }

        _isFlashing = false;

        // flash out
        float flashOutDuration = seconds / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0.0f, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);
    }

    /// <summary>
    /// Flash screen in, wait for <paramref name="waitPeriod"/> seconds, call <paramref name="func"/> and then flash out
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="maxAlpha"></param>
    /// <param name="waitPeriod"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    IEnumerator Flash(float seconds, float maxAlpha, float waitPeriod, Action func)
    {
        // flash in
        float flashInDuration = seconds / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0.0f, maxAlpha, t / flashInDuration);
            _image.color = colorThisFrame;

            yield return null;
        }

        // indication that the screen is max flashed
        _isFlashing = true;

        // wait
        for (float t = 0; t <= waitPeriod; t += Time.deltaTime)
        {
            yield return null;
        }
        func();

        _isFlashing = false;

        // flash out
        float flashOutDuration = seconds / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0.0f, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);
    }

    IEnumerator FadeOut(float seconds, Color newColor)
    {
        Color colorMaxAlpha = newColor;
        colorMaxAlpha.a = 1.0f;
        _image.color = colorMaxAlpha;

        for (float t = 0; t <= seconds; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(1.0f, 0.0f, t / seconds);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);

    }
}
