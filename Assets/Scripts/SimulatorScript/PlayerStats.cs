using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float DefaultGravity = -21f;

    [System.NonSerialized] public float runningMovementSpeed = 4f;
    [System.NonSerialized] public float walkingMovementSpeed = 1.5f;
    [System.NonSerialized] public float crouchingMovementSpeed = 1f;
    [System.NonSerialized] public float gravity = DefaultGravity;
    [System.NonSerialized] public float jumpHeight = 1f;
    [System.NonSerialized] public float standingHeightY = 2f;
    [System.NonSerialized] public float crouchHeightY = 0.5f;
    [System.NonSerialized] public float crouchSpeed = 5f;
    [System.NonSerialized] public float pullGunOutDurationSeconds = 0.7f;
}
