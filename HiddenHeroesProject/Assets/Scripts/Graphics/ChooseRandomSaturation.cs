using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomSaturation : MonoBehaviour
{
    /// <summary>
    /// SpriteRenderer whos saturation will be altered.
    /// </summary>
    [Header("Component References")]
    [Tooltip("SpriteRenderer whos saturation will be altered.")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Minimum saturation value for the SpriteRenderer.
    /// </summary>
    [Header("Saturation Parameters")]
    [Tooltip("Minimum saturation value for the SpriteRenderer")]
    [SerializeField] private float _saturationMin = 0.1f;

    /// <summary>
    /// Maximum saturation value for the SpriteRenderer.
    /// </summary>
    [Header("Saturation Parameters")]
    [Tooltip("Minimum saturation value for the SpriteRenderer")]
    [SerializeField] private float _saturationMax = 1.0f;

    #region MonoBehaviour Methods
    private void Start()
    {
        Color color = _spriteRenderer.color;
        float hue, saturation, value;
        Color.RGBToHSV(color, out hue, out saturation, out value);
        saturation = Random.Range(_saturationMin, _saturationMax);
        color = Color.HSVToRGB(hue, saturation, value);
        _spriteRenderer.color = color;
    }
    #endregion
}
