using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatInfinite : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public float scrollSpeed = 1.0f;

    private float _textureUnitSizeX;

    private void Start()
    {
        Sprite sprite = _spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

    }
    private void Update()
    {
        transform.position += (Vector3.left * scrollSpeed) * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (_cameraTransform.position.x - transform.position.x >=
            _textureUnitSizeX)
        {
            float offsetPositionX =
                (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX,
                transform.position.y);
        }
    }
}
