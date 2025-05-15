using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class EnemyHits : MonoBehaviour
{
    public EnemyAI enemyAI; 
    public TMP_Text hitCountText; 

    void Update()
    {
        if (enemyAI != null && hitCountText != null)
        {
            hitCountText.text =  $"{enemyAI.playerHitCount}";
           
        }
    }
}
