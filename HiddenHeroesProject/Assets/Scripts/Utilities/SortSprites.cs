using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSprites : MonoBehaviour
{
    /// <summary>
    /// Sprite renderer to be sorted.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Sprite renderer to be sorted.")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The point at which the Y value of the sprite will be compared for 
    /// layer assignment.
    /// </summary>
    [Tooltip("The point at which the Y value of the sprite will be " + 
        "compared for layer assignment.")]
    [SerializeField] private Transform _sortingPoint;

    /// <summary>
    /// What sorting layer should this sprite be on?
    /// </summary>
    [Header("Sorting Parameters")]
    [Tooltip("What sorting layer should this sprite be on?")]
    [SerializeField] private string _sortingLayerName = "Default";

    /// <summary>
    /// Base sorting order level. This should be very high due to the nature
    /// of the sorting algorithm (i.e. 5000);
    /// </summary>
    [Tooltip("Base sorting order level. This should be very high due to " + 
        "the nature of the sorting algorithm (i.e. 5000).")]
    [SerializeField] private int _sortingOrderBase = 5000;

    /// <summary>
    /// Optional offset to add to the sorting layer. Useful for grouping 
    /// together sprites that should be drawn over or below others.
    /// </summary>
    [Tooltip("Optional offset to add to the sorting layer. Useful for " + 
        "grouping together sprites that should be drawn over or below others.")]
    [SerializeField] private int _offset = 0;

    #region MonoBehaviour Methods
    private void Awake()
    {
        _spriteRenderer.sortingLayerName = _sortingLayerName;
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = 
            (int)(_sortingOrderBase - _sortingPoint.position.y - _offset);
    }
    #endregion
}
