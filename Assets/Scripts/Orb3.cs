using UnityEngine;

public class Orb3 : MonoBehaviour
{
    private Animator _animator;
    private Damagable _damagable;
    private FlashImage _flashImage;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (Player.progress_9 == true)
        {
            gameObject.SetActive(false);
        }

        _animator = GetComponent<Animator>();
        _damagable = GetComponent<Damagable>();
        _flashImage = FindObjectOfType<FlashImage>();
    }

    void Update()
    {
        _animator.speed = 2 - _damagable.HP / 5;
        if (_damagable.HP <= 0)
            _damagable.Kill();
    }

    void OnDestroy()
    {
        if (Player.progress_9 != true)
        {
            FindObjectOfType<AudioManager>().Play("OrbDestroyed");
            _flashImage.StartFlash(1f, 255, new Color(107/255, 18/255, 18/255), .1f);
            Player.progress_9 = true;
        }
    }


}