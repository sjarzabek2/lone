using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public GameObject popUpBox;
    public TMP_Text popUpText;
    private bool isVisible = false;

    void Update()
    {
        if (isVisible)
            if (Input.GetKeyDown(KeyCode.Space))
                StopPopUp();
    }
    public void StartPopUp(string text, float popSpeed = 0.4f)
    {
        StopAllCoroutines();
        StartCoroutine(Pop(text, popSpeed));
    }
    public void StopPopUp(float hideSpeed = 0.4f)
    {
        StartCoroutine(Hide(hideSpeed));
    }

    public IEnumerator Pop(string text, float popSpeed)
    {
        popUpText.text = text;
        Image image = popUpBox.GetComponent<Image>();
        // fade in
        for (float t = 0; t <= popSpeed; t += Time.deltaTime)
        {
            Color newColorBox = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0.0f, 0.8f, t / popSpeed));
            Color newColorText = new Color(popUpText.color.r, popUpText.color.g, popUpText.color.b, Mathf.Lerp(0.0f, 0.8f, t / popSpeed));

            image.color = newColorBox;
            popUpText.color = newColorText;
            yield return null;
        }
        isVisible = true;

        if (isVisible)
            yield return new WaitForSecondsRealtime(3);

        if (isVisible)
            StopPopUp();
    }

    public IEnumerator Hide(float hideSpeed)
    {
        isVisible = false;
        Image image = popUpBox.GetComponent<Image>();
        // fade out
        for (float t = 0; t <= hideSpeed; t += Time.deltaTime)
        {
            Color newColorBox = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0.8f, 0.0f, t / hideSpeed));
            Color newColorText = new Color(popUpText.color.r, popUpText.color.g, popUpText.color.b, Mathf.Lerp(0.8f, 0.0f, t / hideSpeed));

            image.color = newColorBox;
            popUpText.color = newColorText;
            yield return null;
        }
    }
}
