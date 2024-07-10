using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : EnemyDeath
{
    public GameObject ExplosionEffect;

    public override void Die()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            LeanTween.scale(g, Vector3.zero, 0.5f).setOnComplete(() => { Destroy(g); });
        }

        GetComponent<Collider2D>().enabled = false;
        Global.bossHealthBarManager.DeactivateHealthBar();
        LeanTween.scale(gameObject, Vector3.zero, 0.5f);
        LeanTween.alpha(gameObject, 0, 0.5f).setOnComplete(() => { 
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); });
    }
}
