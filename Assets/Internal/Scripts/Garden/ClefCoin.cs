using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClefCoin : MonoBehaviour
{
    public float ApproachSpeed;
    public float ExpirationTime;

    private float currentSpeed;
    private bool isMagnetized = false;

    private void Start()
    {
        currentSpeed = ApproachSpeed;
        LeanTween.alpha(gameObject, 0.1f, ExpirationTime);
    }

    private bool IsInRangeOfPlayer()
    {
        return Vector2.Distance(transform.position, Global.playerTransform.position) <= GlobalGarden.CoinMagnetDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bwo"))
        {
            AudioManager.instance.PlaySound(AudioEnum.Ding);
            Global.gardenBuffManager.AddCoins(1);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (IsInRangeOfPlayer())
        {
            isMagnetized = true;
            LeanTween.cancel(gameObject);
        }

        if (isMagnetized)
        {
            transform.position = Vector2.MoveTowards(transform.position, Global.playerTransform.position, currentSpeed * Time.deltaTime);
            currentSpeed += (Time.deltaTime * 2);
        }
    }
}
