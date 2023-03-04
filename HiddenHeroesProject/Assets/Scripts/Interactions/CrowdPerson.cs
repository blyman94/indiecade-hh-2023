using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPerson : MonoBehaviour
{
    /// <summary>
    /// What is the fastest speed this object can move at?
    /// </summary>
    [Header("Movement Parameters")]
    [Tooltip("What is the fastest speed this object can move at?")]
    [SerializeField] private float _maxSpeed = 1.0f;

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
    /// How far away from the start position must this object be before it
    /// tries to restore the start position?
    /// </summary>
    [Tooltip("How far away from the start position must this object be " +
        "before it tries to restore the start position?")]
    [SerializeField] private float restoreDistance = 0.1f;

    /// <summary>
    /// Rigidbody2d controlling the movement of this object.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Rigidbody2d controlling the movement of this object.")]
    [SerializeField] private Rigidbody2D _rb;

    /// <summary>
    /// Collider of this object.
    /// </summary>
    [Tooltip("Collider of this object.")]
    [SerializeField] private CircleCollider2D _collider;

    /// <summary>
    /// Current move input of the player object.
    /// </summary>
    [Tooltip("Current move input of the player object.")]
    [SerializeField] private Vector2Variable _playerMoveInput;

    /// <summary>
    /// Position of the player object.
    /// </summary>
    [Tooltip("Position of the player object.")]
    [SerializeField] private Vector2Variable _playerPosition;

    /// <summary>
    /// Starting position of this object.
    /// </summary>
    private Vector2 _startPosition;

    /// <summary>
    /// Is this object currently touching the player?
    /// </summary>
    private bool _isTouchingPlayer;

    public bool IsRiotScene = false;

    #region MonoBehaviour Methods
    private void Start()
    {
        _startPosition = transform.position;
        if (IsRiotScene)
        {
            MoveLeft();
        }
    }
    private void FixedUpdate()
    {
        if (PlayerPushing())
        {
            return;
        }
        else
        {
            RestoreStartPosition();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _isTouchingPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _isTouchingPlayer = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + _collider.offset, _collider.radius);

    }
    #endregion

    public void MoveLeft()
    {
        _rb.velocity = Vector2.left * _maxSpeed;
    }

    /// <summary>
    /// Uses AddForce to move this object back to it's start position, if it is
    /// moved away from its start position.
    /// </summary>
    private void RestoreStartPosition()
    {
        if (!IsRiotScene)
        {
            // Check if the object has moved away from its start position
            if (Vector2.Distance(transform.position, _startPosition) > restoreDistance)
            {
                // Calculate the direction to move the object back to its start position
                Vector2 direction = (_startPosition - (Vector2)transform.position).normalized;

                // Clamp velocity to max speed
                if (_rb.velocity.magnitude > _maxSpeed)
                {
                    _rb.velocity = _rb.velocity.normalized * _maxSpeed;
                }

                // Apply a force to move the object in the calculated direction
                _rb.AddForce(direction * _acceleration);
            }
            else
            {
                _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero,
                    _deceleration * Time.deltaTime);
            }
        }
        else
        {
            MoveLeft();
        }
    }

    /// <summary>
    /// Determines if the player is currently pushing on this object upwards
    /// or downwards
    /// </summary>
    /// <returns>Bool. True if the player is below and pushing up, or if the
    /// player is above and pushing down. False otherwise. </returns>
    private bool PlayerPushing()
    {
        if (!_isTouchingPlayer)
        {
            return false;
        }

        bool isPlayerAbove = transform.position.y <= _playerPosition.Value.y;

        if (isPlayerAbove && _playerMoveInput.Value.y < 0.0f)
        {
            return true;
        }
        else if (!isPlayerAbove && _playerMoveInput.Value.y > 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
