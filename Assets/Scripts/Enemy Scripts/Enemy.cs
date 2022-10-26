using UnityEngine;

public class Enemy : Damagable
{
    private int MaxHP;
    private Path MovePath;
    void Start()
    {
        MaxHP = HP;
        MovePath = GetComponent<Path>();
    }

    void Update()
    {
        Animator animator = GetComponent<Animator>();
        animator.speed = 1f + 0.2f * (MaxHP - HP);
        Color oldColor = GetComponent<SpriteRenderer>().color;
        Color newColor = new Color(oldColor.r, oldColor.g - 1 / (MaxHP - HP + 1), oldColor.b - 1 / (MaxHP - HP + 1), oldColor.a);
        gameObject.GetComponent<SpriteRenderer>().color = newColor;

        if (HP <= 0)
        {
            FindObjectOfType<AudioManager>().Play("GlassShatter");
            Kill();
        }

        if (MovePath != null)
            MovePath.Move();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.collider.CompareTag("Player"))
            Player.Die();
    }
}
