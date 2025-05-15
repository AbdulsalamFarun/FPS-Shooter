using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection & Shooting")]
    public float detectionRange = 20f;
    public float shootInterval = 1f;
    public float accuracy = 1f;
    public Transform shootPoint;
    public GameObject bulletEffect;

    [Header("Player & Layers")]
    public LayerMask playerMask;
    public LayerMask obstructionMask;

    [Header("Debug")]
    public Color rayColor = Color.red;

    private Transform player;
    private float shootTimer;
    public int playerHitCount = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange && CanSeePlayer())
        {
            LookAtPlayer();

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootRaycast();
                shootTimer = 0.5f;
            }
        }

        Debug.DrawRay(shootPoint.position, GetDirectionToPlayer() * detectionRange, rayColor);
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }


    bool CanSeePlayer()
    {
        Vector3 direction = GetDirectionToPlayer();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, detectionRange, playerMask | obstructionMask))
        {
            return hit.collider.CompareTag("Player"); 
        }
        return false;
    }

    void ShootRaycast()
    {
        Vector3 direction = GetDirectionToPlayer();
        direction += Random.insideUnitSphere * (1f - accuracy);
        direction.Normalize();

        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, detectionRange, playerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerHitCount++;
                Debug.Log("Enemy hit player! Total hits: " + playerHitCount);
            }

            if (bulletEffect != null)
            {
                Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    Vector3 GetDirectionToPlayer()
    {
        return (player.position + Vector3.up - shootPoint.position).normalized;
    }

    void OnDrawGizmos()
    {
        if (shootPoint != null && player != null)
        {
            Gizmos.color = rayColor;
            Gizmos.DrawRay(shootPoint.position, GetDirectionToPlayer() * detectionRange);
        }
    }
}
