using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text accuracyText;

    private int score = 0;
    private int hits = 0;
    private int misses = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterHit()
    {
        score += 10;
        hits++;
        UpdateUI();
    }

    public void RegisterMiss()
    {
        score -= 1;
        misses++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"{score}";

        int total = hits + misses;
        float accuracy = total > 0 ? ((float)hits / total) * 100 : 100;
        accuracyText.text = $"{accuracy:F1}";
    }
}

