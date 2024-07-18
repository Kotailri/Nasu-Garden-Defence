using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hitbox
{
    
    public Vector2 offset;
    public Vector2 size;

    public Hitbox(Vector2 offset, Vector2 size)
    {
        this.offset = offset;
        this.size = size;
    }
}

[System.Serializable]
public struct FrameHitbox
{
    public int frame;
    public Hitbox hitbox;
}

public class HitboxChangeOnFrame : MonoBehaviour
{
    public List<FrameHitbox> frameHitboxes = new();

    private BoxCollider2D boxCollider;
    private Hitbox defaultHitbox;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        defaultHitbox = new Hitbox(boxCollider.offset, boxCollider.size);
    }

    private void Update()
    {
        int currentFrame = Global.GetCurrentAnimationFrame(anim);
        foreach (FrameHitbox frame in frameHitboxes)
        {
           if (frame.frame == currentFrame)
            {
                boxCollider.offset = frame.hitbox.offset;
                boxCollider.size = frame.hitbox.size;
                return;
            }
        }

        boxCollider.offset = defaultHitbox.offset;
        boxCollider.size = defaultHitbox.size;
    }
}
