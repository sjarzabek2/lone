using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletHitVFX;
    [Range(.8f, 1.2f)]
    public float soundPitch;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Damagable damagableObject = collision.gameObject.GetComponent<Damagable>();

        if (damagableObject != null)
        {
            damagableObject.HP--;
        }

        GameObject effect = Instantiate(bulletHitVFX, transform.position, Quaternion.identity);
        Sound bulletSound = FindObjectOfType<AudioManager>().GetSound("BulletImpact");

        Random.InitState((int)Time.deltaTime);
        bulletSound.source.pitch = soundPitch + Random.Range(-.1f, .1f);
        bulletSound.source.Play();
        Destroy(effect, 0.4f);
        Destroy(gameObject);
    }
}
