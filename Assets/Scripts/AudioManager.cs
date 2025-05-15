using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    

    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clips -----------")]
    public AudioClip background;
    public AudioClip Hit;
    public AudioClip Walking;
    public AudioClip running;
    public AudioClip Shooting;


    private Dictionary<string, AudioClip> sfxClips;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        sfxClips = new Dictionary<string, AudioClip>
        {
            { "Hit",Hit},
            { "Walking",Walking },
            { "running",running },
            { "Shooting",Shooting },

        };
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
        SFXSource.volume = 0.5f;
    }

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log(clip.name);
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
