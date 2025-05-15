using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooterTracking : MonoBehaviour
{
    public static Action OnTargetMissed;
    //AudioManager audioManager;
    [SerializeField] Camera cam;

    private void Awake()
    {
       // audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    void Update()
    {
        
        
        if (Timer.GameEnded)
            return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TargetTracking targetTracking = hit.collider.gameObject.GetComponent<TargetTracking>();

                if (targetTracking != null)
                {
                    targetTracking.Hit();

                    //audioManager.PlaySFX(audioManager.Hit);
                }
                else
                {
                    ScoreManager.instance.RegisterMiss();
                }
            }
            else
            {
                ScoreManager.instance.RegisterMiss();
            }
        }
    }
}

