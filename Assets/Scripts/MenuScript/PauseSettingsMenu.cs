using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseSettingsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_InputField sensitivityInput;
    public Slider volumeSlider;
    public AudioSource backgroundMusic;

    public GameObject settingsPanel;   
    public GameObject pausePanel;      

    private bool isUpdating = false;

    void Start()
    {
        
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        UpdateUI(sensitivity, volume);

        
        sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderChanged);
        sensitivityInput.onEndEdit.AddListener(OnSensitivityInputChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnSensitivitySliderChanged(float value)
    {
        if (isUpdating) return;
        isUpdating = true;
        sensitivityInput.text = value.ToString("F2");
        SensitivitySettings.Sensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
        isUpdating = false;
    }

    void OnSensitivityInputChanged(string value)
    {
        if (isUpdating) return;
        if (float.TryParse(value, out float parsed))
        {
            parsed = Mathf.Clamp(parsed, 0.1f, 10f);
            isUpdating = true;
            sensitivitySlider.value = parsed;
            SensitivitySettings.Sensitivity = parsed;
            PlayerPrefs.SetFloat("Sensitivity", parsed);
            isUpdating = false;
        }
    }

    void OnVolumeChanged(float value)
    {
        if (backgroundMusic != null)
            backgroundMusic.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }

    void UpdateUI(float sensitivity, float volume)
    {
        isUpdating = true;
        sensitivitySlider.value = sensitivity;
        sensitivityInput.text = sensitivity.ToString("F2");
        volumeSlider.value = volume;
        if (backgroundMusic != null)
            backgroundMusic.volume = volume;
        isUpdating = false;
    }

    
    public void ReturnToPauseMenu()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
