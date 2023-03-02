using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Recieves events from the Player Input component to control the attached
/// object.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Component responsible for moving the player in the crowd scene.
    /// </summary>
    [Tooltip("Component responsible for moving the player in the crowd scene.")]
    [SerializeField] private Vector2Variable _playerMoveInput;

    #region Input Action Responses
    /// <summary>
    /// Accepts context from the PlayerInput component and moves the attached
    /// object according with its movementCrowdScene component.
    /// </summary>
    /// <param name="context">Context received from the Player Input 
    /// component.</param>
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMoveInput.Value = context.ReadValue<Vector2>();
        }
        else
        {
            _playerMoveInput.Value = Vector2.zero;
        }

    }
    #endregion
}
