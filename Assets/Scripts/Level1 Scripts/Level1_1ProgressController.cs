using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1_1ProgressController : MonoBehaviour
{
    public GameObject DancingOrbs;
    public GameObject SpaceshipFixed;
    public GameObject SpaceshipWreck;

    private Animator _animator;
    private Coroutine _currentCoroutine;
    void Start()
    {
        if (Player.progress_9 == true)
        {
            DancingOrbs.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Player.progress_9 == true && collider.CompareTag("Player"))
        {
            _animator = DancingOrbs.GetComponent<Animator>();
            Debug.Log("Start");
            _currentCoroutine = StartCoroutine(EndGameCoroutine());
        }
    }

    void FixSpaceship()
    {
        Destroy(DancingOrbs);
        Instantiate(SpaceshipFixed, SpaceshipWreck.transform.position, Quaternion.identity);
        Destroy(SpaceshipWreck);
    }

    void GoToEndScreen()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator EndGameCoroutine()
    {
        FindObjectOfType<AudioManager>().Stop("Step");
        FindObjectOfType<AudioManager>().Play("SpaceshipFix");

        Player player = FindObjectOfType<Player>();
        Player.isGamePaused = true;

        // Freeze player in place
        player.animator.SetBool("IsMoving", false);
        player.animator.SetBool("IsShooting", false);
        player.animator.SetInteger("Direction", 5);
        player.moveSpeed = 0;

        // spin faster every frame
        for (float t = 1; t <= 5; t += Time.deltaTime)
        {
            _animator.speed = t;
            yield return null;
        }

        // flash the image white
        FlashImage flashImage = FindObjectOfType<FlashImage>();
        flashImage.StartFlash(3, 255, Color.white, 1, FixSpaceship);

        yield return new WaitForSeconds(6);

        // flash out - game over
        flashImage.StartFlash(4, 255, Color.black, 1, GoToEndScreen);
    }
}
