using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 150f;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;

    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        
        audioManager.PlaySFX(audioManager.Hit);

    }
}
