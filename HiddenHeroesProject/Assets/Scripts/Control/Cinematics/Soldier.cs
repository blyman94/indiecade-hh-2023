using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    /// <summary>
    /// What is the fastest speed this object can move at?
    /// </summary>
    [Header("Movement Parameters")]
    [Tooltip("What is the fastest speed this object can move at?")]
    [SerializeField] private float _maxSpeed = 1.0f;

    /// <summary>
    /// Rigidbody2d controlling the movement of this object.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Rigidbody2d controlling the movement of this object.")]
    [SerializeField] private Rigidbody2D _rb;

    public void MoveLeft()
    {
        _rb.velocity = Vector2.left * _maxSpeed;
    }

    public void MoveRight()
    {
        _rb.velocity = Vector2.right * _maxSpeed;
    }

    public void SendInRandomDir(float yDir)
    {
        float speed = _rb.velocity.magnitude;
        _rb.velocity = new Vector2(1.0f, yDir) * speed;
    }

    public void SetNewSpeed(float newSpeed)
    {
        Vector2 velDir = _rb.velocity.normalized;
        _rb.velocity = velDir * newSpeed;
    }
}
