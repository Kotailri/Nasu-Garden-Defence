using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject ExplosionEffectObject;
    public GameObject GameOverUI;
    public CanvasGroup GameOverUIFade;
    public float GameOverUIActivePositionY;

    [Space(5f)]
    public List<Transform> backgroundObjectsToDestroy = new();

    private void Awake()
    {
        Global.gameOverManager = this;
        GameOverUIFade.alpha = 0f;
        GameOverUIFade.gameObject.SetActive(false);
        

        Global.isGameOver = false;
    }

    private void Start()
    {
        GameOverUI.SetActive(false);
    }

    public void DoGameOver(DeathCondition deathCondition)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            EventManager.TriggerEvent(EventStrings.GAME_OVER_KILL_ALL, null);
        }

        Destroy(GameObject.FindGameObjectWithTag("Bwo"));
        Global.playerTransform.gameObject.GetComponent<PlayerHitbox>().enabled = false;
        Global.playerTransform.gameObject.GetComponent<PlayerMovement>().enabled = false;
        Global.playerTransform.gameObject.GetComponent<PlayerAttackManager>().enabled = false;

        //foreach (Transform t in Global.playerTransform)
        //    Destroy(t.gameObject);

        GameOverUIFade.gameObject.SetActive(true);

        StartCoroutine(GameOverCoroutine());
        IEnumerator GameOverCoroutine()
        {
            Instantiate(ExplosionEffectObject, Global.playerTransform.position, Quaternion.identity);

            Rigidbody2D rb = Global.playerTransform.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 5.0f;
            rb.velocity = Vector3.zero;
            float explosionForce = 2f;

            rb.isKinematic = false;

            Vector2 direction = new(1, 1);
            rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);

            Vector3 rotationDirection = new Vector3(0, 0, Mathf.Sign(direction.x));
            rb.AddTorque(explosionForce*2, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.15f);

            foreach (Transform t in backgroundObjectsToDestroy)
            {
                Instantiate(ExplosionEffectObject, t.position, Quaternion.identity);
                t.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.15f);
            }

            yield return new WaitForSeconds(1f);
            rb.gravityScale = 0f;
            rb.velocity = Vector3.zero;
            AudioManager.instance.PlaySound(AudioEnum.GameOver);
            LeanTween.alphaCanvas(GameOverUIFade, 1f, 1f);
            yield return new WaitForSeconds(3f);
            LeanTween.alphaCanvas(GameOverUIFade, 0f, 1f).setOnComplete(()=> { GameOverUIFade.gameObject.SetActive(false); });
            yield return new WaitForSeconds(1f);

            GameOverUI.SetActive(true);
            GameOverUI.GetComponent<GameOverUI>().InitializeGameOverUI();
            LeanTween.moveLocalY(GameOverUI, GameOverUIActivePositionY, 0.5f).setEaseOutBounce();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Global.GameOver(DeathCondition.PlayerDeath);
        }
    }
}
