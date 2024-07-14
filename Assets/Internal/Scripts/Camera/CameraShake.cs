using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.5f;
    public float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.position;
    }

    public void ShakeCamera(float _shakeDuration = 0.5f, float _shakeMagnitude = 0.5f, float _dampingSpeed = 1f)
    {
        if (LeanTween.isTweening(gameObject)) { return; }
        shakeDuration = _shakeDuration;
        shakeMagnitude = _shakeMagnitude;
        dampingSpeed = _dampingSpeed;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = initialPosition;
    }
}
