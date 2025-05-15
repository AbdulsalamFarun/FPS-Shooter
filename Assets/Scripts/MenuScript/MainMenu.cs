using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider sensitivitySlider;
    public TMP_InputField sensitivityInput;

    private float sensitivity = 1f;
    private bool isUpdating = false; // prevent feedback loop

    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
        SensitivitySettings.Sensitivity = sensitivity;
        UpdateUI(sensitivity);


        // Add listeners
        if (sensitivitySlider != null)
            sensitivitySlider.onValueChanged.AddListener(OnSliderChanged);

        if (sensitivityInput != null)
            sensitivityInput.onEndEdit.AddListener(OnInputChanged);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    void OnSliderChanged(float value)
    {
        if (isUpdating) return;

        isUpdating = true;
        sensitivity = value;
        sensitivityInput.text = sensitivity.ToString("F2");
        SensitivitySettings.Sensitivity = sensitivity;
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        isUpdating = false;
    }

    void OnInputChanged(string value)
    {
        if (isUpdating) return;

        if (float.TryParse(value, out float parsed))
        {
            parsed = Mathf.Clamp(parsed, 0.1f, 10f);

            isUpdating = true;
            sensitivity = parsed;
            sensitivitySlider.value = sensitivity;
            PlayerPrefs.SetFloat("Sensitivity", sensitivity);
            isUpdating = false;
        }
    }

    void UpdateUI(float value)
    {
        isUpdating = true;
        sensitivitySlider.value = value;
        sensitivityInput.text = value.ToString("F2");
        isUpdating = false;
    }
}
