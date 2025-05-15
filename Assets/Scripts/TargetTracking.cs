using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTracking : MonoBehaviour
{
    public static Action OnTargetHit;
    public float waitTime = 0.5f;
    [SerializeField] float health ,maxHealth = 100f;
    public float moveSpeed = 5f;
    public float directionChangeInterval = 0.3f;

    private Vector3 moveDirection;
    private float timer ;

    Rigidbody rb;

    [SerializeField] HealthBar healthBar;

    private Vector3 startPos;
    private float direction = 1f;
    private bool isWaiting = false;

    //AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<HealthBar>();
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        RandomizePosition();
        GenerateRandomDirection();
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        startPos = transform.position;
        


    }
    void Update()
    {
        timer += Time.deltaTime;

        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        
        if (timer >= directionChangeInterval)
        {
            GenerateRandomDirection();
            timer = 0f;
        }
    }

    public void Hit()
    {
        health -= 1;

        ScoreManager.instance.RegisterHit();
        healthBar.UpdateHealthBar(health, maxHealth);

        //audioManager.PlaySFX(audioManager.Hit);



        if (health <= 0)
        {
            RandomizePosition();
            health = maxHealth;
            healthBar.UpdateHealthBar(health, maxHealth);

        }
        OnTargetHit?.Invoke();
    }

    void RandomizePosition()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();

    }
    void GenerateRandomDirection()
    {
        float x = UnityEngine.Random.Range(-.1f, .1f);
        float y = 0f; 
        float z = 0f;

        moveDirection = new Vector3(x, y, z).normalized;
    }


}

