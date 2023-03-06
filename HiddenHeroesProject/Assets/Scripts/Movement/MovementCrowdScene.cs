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

    public Animator PlayerAnimator;
    public SpriteRenderer playerSpriteRenderer;

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
    public bool NoReallyFaceRight;

    private float _currentSpeed;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _moveInput.VariableUpdated += StartWalkAnim;
    }
    private void OnDisable()
    {
        _moveInput.VariableUpdated -= StartWalkAnim;
    }
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
        playerSpriteRenderer.flipX = true;

        if (Cinematic)
        {
            playerSpriteRenderer.flipX = false;
            if (NoReallyFaceRight)
            {
                playerSpriteRenderer.flipX = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!Cinematic)
        {
            Vector2 movement = _moveInput.Value;

            if (movement != Vector2.zero)
            {
                if (movement.x >= 0)
                {
                    playerSpriteRenderer.flipX = true;
                }
                else
                {
                    playerSpriteRenderer.flipX = false;
                }
            }

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
        else
        {
            if (_rb.velocity.x < 0.0f)
            {
                PlayerAnimator.SetBool("IsWalking", true);
            }
            else
            {
                PlayerAnimator.SetBool("IsWalking", false);
            }
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

    private void StartWalkAnim()
    {
        if (_moveInput.Value.magnitude > 0.0f)
        {
            PlayerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }
    }

    public void MoveLeft()
    {
        playerSpriteRenderer.flipX = false;
        _rb.velocity = Vector2.left * _maxSpeed;
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
        PlayerAnimator.SetBool("IsWalking", false);
    }
}
