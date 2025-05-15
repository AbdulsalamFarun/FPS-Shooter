using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Gun equippedWeapon;
    public bool isShootingDisabled = true;

    [SerializeField] Animator handAnimator;
    [SerializeField] Transform firePoint;
    [SerializeField] Camera playerCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject armsContainer;
    [SerializeField] GameObject gunContainer;
    [SerializeField] GameObject wallHitDecalPrefab;
    [SerializeField] GameObject projectilePrefab;

    private PlayerController playerController;
    private PlayerStats playerStats;

    AudioManager audioManager;


    private float lastTimeShot = 0f;
    private int currentRecoilIndex = 0;
   
    
    void Start()
    {
        
        audioManager= AudioManager.Instance;
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        equippedWeapon = new AK47();

        PullOutGun(() => { });
    }

    
    void Update()
    {
        if (isShootingDisabled)
        {
            return;
        }

        bool isTryingToShoot = isShootingDisabled == false && Input.GetKey(KeyCode.Mouse0);

        if (isTryingToShoot)
        {
            HandleShooting();
            Debug.Log(" is playing" + audioManager.Shooting.name);
            audioManager.PlaySFX(audioManager.Shooting);
            

        }
        else
        {
            playerController.SetGunRotation(
                Vector3.Lerp(
                    playerController.gunRotation,
                    Vector3.zero,
                    equippedWeapon.fireRatePerSecond * Time.deltaTime
                )
            );
        }
    }

    public void PullOutGun(Action onFinish)
    {
        gunContainer.SetActive(true);
        StartCoroutine(OnPullOutGun(onFinish));
    }

    public void HideGun()
    {
        gunContainer.SetActive(false);
        isShootingDisabled = true;
    }

    public void SetIsShootingDisabled(bool _isShootingDisabled)
    {
        isShootingDisabled = _isShootingDisabled;
    }

    void HandleShooting()
    {
        if (Time.time - lastTimeShot >= 1 / equippedWeapon.fireRatePerSecond)
        {
            PlayAttackAnimation();

            muzzleFlash.Play();

            HandleGunRecoil();

            Instantiate(projectilePrefab, firePoint.transform.position, firePoint.rotation);

            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Hitbox hitbox = hit.transform.GetComponent<Hitbox>();
                if (hitbox != null)
                {
                    hitbox.ApplyHit();
                }

               Instantiate(wallHitDecalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }

            lastTimeShot = Time.time;
        }
    }

    void HandleGunRecoil()
    {
        if (Time.time - lastTimeShot >= equippedWeapon.recoilResetTimeSeconds)
        {
            playerController.SetGunRotation(playerController.gunRotation + equippedWeapon.recoilPattern[0]);
            currentRecoilIndex = 1;
        } else
        {
            playerController.SetGunRotation(playerController.gunRotation + equippedWeapon.recoilPattern[currentRecoilIndex]);

            if (currentRecoilIndex + 1 <= equippedWeapon.recoilPattern.Length - 1)
            {
                currentRecoilIndex += 1;
            } else
            {
                currentRecoilIndex = 0;
            }
        }
    }

    void PlayAttackAnimation()
    {
        handAnimator.Play("Fire");
    }

    IEnumerator OnPullOutGun(Action onFinish)
    {
        
        isShootingDisabled = true;
        handAnimator.Play("Draw");
        

        yield return new WaitForSeconds(playerStats.pullGunOutDurationSeconds);

        isShootingDisabled = false;
        onFinish();
    }
}
