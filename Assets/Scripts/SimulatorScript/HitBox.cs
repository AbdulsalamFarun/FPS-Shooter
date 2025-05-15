using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum HitboxType { Head, Body }
    public HitboxType hitboxType;

    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    public void ApplyHit()
    {
        if (enemyHealth == null) return;

        float damage = 0f;
        switch (hitboxType)
        {
            case HitboxType.Head:
                damage = 155f;
                break;
            case HitboxType.Body:
                damage = 40f;
                break;
        }

        enemyHealth.TakeDamage(damage);
    }
}
