using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomedBomb : MonoBehaviour
{
    public GameObject Explosion;

    public void Initialize(float Timer, int ExplosionDamage)
    {
        LeanTween.color(gameObject, new Color(1,0,1,1), Timer);
        StartCoroutine(WaitTimer());
        IEnumerator WaitTimer()
        {
            yield return new WaitForSeconds(Timer);
            GameObject g = Instantiate(Explosion, transform.position, Quaternion.identity);
            g.GetComponent<Explosion>().Initialize(ExplosionDamage);
            Destroy(gameObject);
        }
    }
}
