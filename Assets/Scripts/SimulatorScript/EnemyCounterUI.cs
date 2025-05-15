using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyCounterUI : MonoBehaviour
{
    public TMP_Text enemyCountText;
    public EndPanel endPanel;

    void Update()
    {
        int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = $"{remaining}";

        if (remaining == 0)
        {
            WinGame(); // or trigger next scene / win screen
        }
    }

    void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
