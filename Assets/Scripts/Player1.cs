using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1: MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    float sensitivity;

    float verticalLookRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sensitivity = SensitivitySettings.Sensitivity;
    }

    void Update()
    {
        if (Timer.GameEnded)
            return;

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity);

        verticalLookRotation -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
        if (Time.timeScale == 0f) return;
    }
}