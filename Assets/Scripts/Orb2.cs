using UnityEngine;

public class Orb2 : MonoBehaviour
{
    private Animator _animator;
    private Damagable _damagable;
    private FlashImage _flashImage;

    public static Orb2 instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            gameObject.SetActive(false);
            return;
        }

        DontDestroyOnLoad(gameObject);
        if (Player.progress_7 == true)
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
        if (Player.progress_7 != true)
        {
            FindObjectOfType<AudioManager>().Play("OrbDestroyed");
            _flashImage.StartFlash(1f, 255, new Color(107/255, 18/255, 18/255), .1f, MovePlayer);
            Player.progress_7 = true;
        }
    }

    void MovePlayer()
    {
        Vector2 newLocation = new Vector2(-9f, 120f);
        Player player = FindObjectOfType<Player>();
        player.transform.position = newLocation;
        player.RespawnPoint = newLocation;
    }

}