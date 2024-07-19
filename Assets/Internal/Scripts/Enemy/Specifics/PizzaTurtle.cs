using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTurtle : MonoBehaviour
{
    public Hitbox shellHitbox;
    private Hitbox defaultHitbox;
    private BoxCollider2D col;

    [HideInInspector]
    public bool IsInAnimation = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        defaultHitbox = new Hitbox(col.offset, col.size);
    }

    public void Hide()
    {
        StartCoroutine(HideCoroutine());
        

        IEnumerator HideCoroutine()
        {
            yield return new WaitUntil(() => IsInAnimation == false);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            anim.SetTrigger("hide");

            yield return new WaitUntil(() => IsInAnimation == false);

            GetComponent<TurtleGetHit>().IsImmune = true;
            col.offset = shellHitbox.offset;
            col.size = shellHitbox.size;
        }
    }

    public void Unhide()
    {
        StartCoroutine(UnhideCoroutine());
        

        IEnumerator UnhideCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsInAnimation == false);
            anim.SetTrigger("unhide");

            yield return new WaitUntil(() => IsInAnimation == false);
            GetComponent<TurtleGetHit>().IsImmune = false;

            col.offset = defaultHitbox.offset;
            col.size = defaultHitbox.size;
        }
    }
}
