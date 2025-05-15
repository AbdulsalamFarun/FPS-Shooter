using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
	[SerializeField] CanvasGroup canvasGroup;

	void OnEnable()
	{
		Timer.OnGameEnded += OnGameEnded;
	}

	void OnDisable()
	{
		Timer.OnGameEnded -= OnGameEnded;
	}

	public void OnGameEnded()
	{
		canvasGroup.alpha = 1f;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;

        Time.timeScale = 0f;
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
