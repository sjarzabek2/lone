using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    public string SoundName;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager am = FindObjectOfType<AudioManager>();
            am.StopAll();
            am.Play(SoundName);
        }
    }
}
