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

    private float _currentSpeed;

    #region MonoBehaviour Methods
    private void Start()
    {
        if (!Cinematic)
        {
            _position.Value = transform.position;
        }
        if (Cinematic)
        {
            Stop();
        }
    }
    private void FixedUpdate()
    {
        if (!Cinematic)
        {
            Vector2 movement = _moveInput.Value;

            if (movement.sqrMagnitude > 0.0f)
            {
                _currentSpeed =
                    Mathf.MoveTowards(_currentSpeed, _maxSpeed,
                    _acceleration * Time.fixedDeltaTime);
            }
            else
            {
                _currentSpeed =
                    Mathf.MoveTowards(_currentSpeed, 0.0f,
                    _deceleration * Time.fixedDeltaTime);
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _maxSpeed);
            _rb.velocity = _moveInput.Value * _currentSpeed;
        }
    }
    private void Update()
    {
        if (_position != null)
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
