using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public static Action OnTargetHit;
	AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
	{
		RandomizePosition();
	}

	public void Hit()
	{
		
		RandomizePosition();
		OnTargetHit?.Invoke();
		audioManager.PlaySFX(audioManager.Hit);
	}

	void RandomizePosition()
	{
		transform.position = TargetBounds.Instance.GetRandomPosition();

	}
}
