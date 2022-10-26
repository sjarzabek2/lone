using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        FlashImage flashImage = FindObjectOfType<FlashImage>();
        flashImage.StartFadeOut(2, Color.black);
    }
    public void StartGame()
    {
        FlashImage flashImage = FindObjectOfType<FlashImage>();
        flashImage.StartFlash(3, 255, Color.black, 1, StartGameDelegate);
    }

    public void ExitGame()
    {
        FlashImage flashImage = FindObjectOfType<FlashImage>();
        flashImage.StartFlash(3, 255, Color.black, 1, ExitGameDelegate);
    }

    void StartGameDelegate()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void ExitGameDelegate()
    {
        Application.Quit();
    }
}
