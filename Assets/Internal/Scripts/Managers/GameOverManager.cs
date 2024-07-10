using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject ExplosionEffectObject;
    public GameObject GameOverUI;
    public CanvasGroup GameOverUIFade;
    public float GameOverUIActivePositionY;

    [Space(5f)]
    public List<Transform> backgroundObjectsToDestroy = new();
    public SpriteRenderer playerRenderer;

    private void Awake()
    {
        Global.gameOverManager = this;
        GameOverUIFade.alpha = 0f;
        GameOverUIFade.gameObject.SetActive(false);
        GameOverUI.SetActive(false);
    }

    public void DoGameOver(DeathCondition deathCondition)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            EventManager.TriggerEvent(EventStrings.GAME_OVER_KILL_ALL, null);
        }

        Global.playerTransform.gameObject.GetComponent<PlayerHitbox>().enabled = false;
        Global.playerTransform.gameObject.GetComponent<PlayerMovement>().enabled = false;
        Global.playerTransform.gameObject.GetComponent<PlayerAttackManager>().enabled = false;
        playerRenderer.enabled = false;

        foreach (Transform t in Global.playerTransform)
            Destroy(t.gameObject);

        GameOverUIFade.gameObject.SetActive(true);

        StartCoroutine(GameOverCoroutine());
        IEnumerator GameOverCoroutine()
        {
            Instantiate(ExplosionEffectObject, Global.playerTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);

            foreach (Transform t in backgroundObjectsToDestroy)
            {
                Instantiate(ExplosionEffectObject, t.position, Quaternion.identity);
                t.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.15f);
            }

            yield return new WaitForSeconds(1f);
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
            DoGameOver(DeathCondition.PlayerDeath);
        }
    }
}
