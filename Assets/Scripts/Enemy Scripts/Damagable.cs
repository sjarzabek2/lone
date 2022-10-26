using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int HP;

    public void Kill()
    {
        Destroy(gameObject);
    }
}
