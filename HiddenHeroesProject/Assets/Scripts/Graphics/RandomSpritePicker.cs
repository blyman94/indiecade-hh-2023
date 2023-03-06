using UnityEngine;

public class RandomSpritePicker : MonoBehaviour
{
    public Sprite[] sprites;

    public bool RandomLeftRight;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public bool FaceLeft;

    private void Start()
    {

        if (sprites != null && sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            Debug.LogError("No sprites assigned!");
        }

        if (RandomLeftRight)
        {
            spriteRenderer.flipX = Random.Range(0, 1.0f) > 0.5f;
        }

        if (FaceLeft)
        {
            spriteRenderer.flipX = false;
        }
    }
}
