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
    private Vector3 lockedPosition;
    private bool isHiding = false;

    public GameObject blocker;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        defaultHitbox = new Hitbox(col.offset, col.size);
        blocker.transform.localScale = new Vector3(blocker.transform.localScale.x, 0, blocker.transform.localScale.z);
    }

    private void Update()
    {
        if (isHiding)
        {
            transform.position = lockedPosition;
        }
    }

    public void Hide()
    {
        StartCoroutine(HideCoroutine());
        

        IEnumerator HideCoroutine()
        {
            yield return new WaitUntil(() => IsInAnimation == false);
            LeanTween.cancel(blocker);
            LeanTween.scaleY(blocker, 1.5f, 0.5f);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<EnemyMovement>().CanBeKnockedBack = false;
            GetComponent<EnemyMovement>().DisableMovement();
            lockedPosition = transform.position;
            isHiding = true;
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
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => IsInAnimation == false);
            LeanTween.cancel(blocker);
            LeanTween.scaleY(blocker, 0, 0.25f);

            GetComponent<EnemyMovement>().CanBeKnockedBack = true;
            GetComponent<EnemyMovement>().EnableMovement();
            anim.SetTrigger("unhide");

            yield return new WaitUntil(() => IsInAnimation == false);
            GetComponent<TurtleGetHit>().IsImmune = false;
            isHiding = false;
            col.offset = defaultHitbox.offset;
            col.size = defaultHitbox.size;
        }
    }
}
