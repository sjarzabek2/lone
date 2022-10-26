using UnityEngine;

public class Player : MonoBehaviour
{
    // progress variables
    ///<summary>1st paper read</summary>
    public static bool progress_1 = false;
    ///<summary>gun pickup</summary>
    public static bool progress_2 = false;
    ///<summary>1st Orb destroyed</summary>
    public static bool progress_3 = false;
    ///<summary>lever handle pickup</summary>
    public static bool progress_4 = false;
    ///<summary>lever handle use</summary>
    public static bool progress_5 = false;
    ///<summary>lever puzzle solved</summary>
    public static bool progress_6 = false;
    ///<summary>2nd orb destroyed</summary>
    public static bool progress_7 = false;
    ///<summary>3rd orb seen moving</summary>
    public static bool progress_8 = false;
    ///<summary>3rd orb destroyed</summary>
    public static bool progress_9 = false;



    public GameObject interactionNotification;

    private Vector2 movement;
    private int Direction;
    private bool IsMoving;

    [HideInInspector]
    public Vector2 RespawnPoint;
    public Animator animator;

    private DeathScreen _deathScreen = null;
    private FlashImage _flashImage = null;
    private AudioManager _audioManager = null;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    [Header("Shooting")]
    public float BulletSpeed;
    public float RandomDirectionRange;
    public GameObject BulletPrefab = null;

    public static bool isGamePaused = false;

    void Awake()
    {
        _deathScreen = FindObjectOfType<DeathScreen>();
        _flashImage = FindObjectOfType<FlashImage>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {

        _flashImage.StartFadeOut(2, Color.black);

        progress_1 = false;
        progress_2 = false;
        progress_3 = false;
        progress_4 = false;
        progress_5 = false;
        progress_6 = false;
        progress_7 = false;
        progress_8 = false;
        progress_9 = false;

        Player.isGamePaused = false;
    }

    void Update()
    {
        if (!isGamePaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // setting direction of movement
            if (movement.x == 0 && movement.y == 1)
                Direction = 1;
            else if (movement.x == 1 && movement.y == 1)
                Direction = 2;
            else if (movement.x == 1 && movement.y == 0)
                Direction = 3;
            else if (movement.x == 1 && movement.y == -1)
                Direction = 4;
            else if (movement.x == 0 && movement.y == -1)
                Direction = 5;
            else if (movement.x == -1 && movement.y == -1)
                Direction = 6;
            else if (movement.x == -1 && movement.y == 0)
                Direction = 7;
            else if (movement.x == -1 && movement.y == 1)
                Direction = 8;

            // checking if the player moves (for animations and sound)
            if (movement.x != 0 || movement.y != 0)
            {
                IsMoving = true;
                if (_audioManager.isPlaying("Step") == false)
                    _audioManager.Play("Step");
            }
            else
            {
                IsMoving = false;
                _audioManager.Stop("Step");
            }
            
            animator.SetBool("IsMoving", IsMoving);
            animator.SetInteger("Direction", Direction);

            // Shooting
            animator.SetBool("IsShooting", Shoot());
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed);
    }

    public void NotifyPlayer()
    {
        interactionNotification.SetActive(true);
        
    }
    public void DeNotifyPlayer()
    {
        interactionNotification.SetActive(false);
    }

    private bool Shoot()
    {
        if (Input.GetButtonDown("Fire1") && progress_2 == true)
        {
            FindObjectOfType<AudioManager>().Play("Gunshot");

            Transform firePoint = GameObject.Find("FirePoint" + Direction).transform;
            GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            float randomFactor = UnityEngine.Random.Range(-RandomDirectionRange, RandomDirectionRange);
            Vector3 finalShootingDirection = firePoint.up;
            finalShootingDirection.x += randomFactor;
            finalShootingDirection.y += randomFactor;
            rb.AddForce(finalShootingDirection * BulletSpeed, ForceMode2D.Impulse);
            return true;
        }
        else
            return false;
    }

    public static void Die()
    {
        Player player = FindObjectOfType<Player>();
        // Show death screen and respawn
        player._deathScreen.ShowDeathScreen(1, player.Respawn);
    }

    private void Respawn()
    {
        gameObject.transform.position = RespawnPoint;
    }



}