using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMine : MonoBehaviour
{
    private int damage;

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject exp = Managers.Instance.Resolve<IPrefabMng>().InstantiatePrefab(PrefabEnum.PurpleExplosion, transform.position, Quaternion.identity);
            exp.GetComponent<Explosion>().Initialize(damage);
            Destroy(gameObject);
        }
    }
}
