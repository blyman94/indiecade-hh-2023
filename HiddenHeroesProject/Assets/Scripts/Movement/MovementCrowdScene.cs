using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrowdScene : MonoBehaviour
{
    /// <summary>
    /// What is the fastest speed this object can move at?
    /// </summary>
    [Header("Movement Parameters")]
    [Tooltip("What is the fastest speed this object can move at?")]
    [SerializeField] private float _maxSpeed = 5.0f;

    /// <summary>
    /// At which rate does this object approach max speed?
    /// </summary>
    [Tooltip("At which rate does this object approach max speed?")]
    [SerializeField] private float _acceleration = 2.5f;

    /// <summary>
    /// At which rate does this object approach max speed?
    /// </summary>
    [Tooltip("At which rate does this object approach 0 speed?")]
    [SerializeField] private float _deceleration = 2.5f;

    /// <summary>
    /// Rigidbody2D of the object being moved.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Rigidbody2D of the object being moved.")]
    [SerializeField] private Rigidbody2D _rb;

    /// <summary>
    /// Vector2Variable to store move input for this object.
    /// </summary>
    [Tooltip("Vector2Variable to store move input for this object.")]
    [SerializeField] private Vector2Variable _moveInput;

    /// <summary>
    /// Vector2Variable to store position for this object.
    /// </summary>
    [Tooltip("Vector2Variable to store position for this object.")]
    [SerializeField] private Vector2Variable _position;

    public bool Cinematic = false;

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        if (!Cinematic)
        {
            Vector2 movement = _moveInput.Value;

            if (movement.sqrMagnitude > 0.0f)
            {
                movement *= _acceleration;

                // Clamp velocity to max speed
                if (_rb.velocity.magnitude > _maxSpeed)
                {
                    _rb.velocity = _rb.velocity.normalized * _maxSpeed;
                }

                // Apply movement to rigidbody
                _rb.AddForce(movement, ForceMode2D.Force);
            }
            else
            {
                _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero,
                    _deceleration * Time.deltaTime);
            }
        }
    }
    private void Update()
    {
        if (!Cinematic)
        {
            _position.Value = transform.position;
        }
    }
    #endregion 

    public void MoveLeft()
    {
        _rb.velocity = Vector2.left * _maxSpeed;
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
    }
}
