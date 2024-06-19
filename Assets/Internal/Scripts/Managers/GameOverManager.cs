using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private void Awake()
    {
        Global.gameOverManager = this;
    }

    public void DoGameOver()
    {
        StartCoroutine(GameOverCoroutine());
        IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
