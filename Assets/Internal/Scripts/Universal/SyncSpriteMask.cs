using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SyncSpriteMask : MonoBehaviour
{
    private SpriteRenderer animatedSpriteRenderer;
    private SpriteMask spriteMask;

    private void Awake()
    {
        spriteMask = gameObject.AddComponent<SpriteMask>();
        animatedSpriteRenderer = GetComponent<SpriteRenderer>();

        spriteMask.alphaCutoff = 1.0f;
    }

    private void Update()
    {
        if (animatedSpriteRenderer != null && spriteMask != null)
        {
            spriteMask.sprite = animatedSpriteRenderer.sprite;
        }
    }
}
