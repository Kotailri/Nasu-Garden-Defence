using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour, IHasTriggerEnter, AffectedByAmplifier, AffectedByApexStride
{
    private void Awake()
    {
        gameObject.AddComponent<CallsTriggerCollisions>();
    }

    
    private void Start()
    {
        if (Global.keystoneItemManager.ApexStrideLevel == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        SaveShootPosition();
    }

    public void OnTriggerEnterEvent(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            if (collisionObject.TryGetComponent(out EnemyGetHit hit))
            {
                int damage = Mathf.FloorToInt(PlayerScriptableSettings.ProjectileDamage
                    * PlayerScriptableSettings.PlayerDamageAmp
                    * GetAmplifiedRangeAmount(ShootPosition, transform.position));
                damage = AddApexStrideDamage(damage);
                hit.GetHit(damage);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (transform.position.x >= Global.MaxX)
        {
            Destroy(gameObject);
        }
    }

    public float GetAmplifiedRangeAmount(Vector2 startPosition, Vector2 endPosition)
    {
        if (!GlobalItemToggles.HasAmplifier)
        {
            return 1;
        }

        return Mathf.Clamp(Global.keystoneItemManager.DistanceAmplificationAmount * Vector2.Distance(startPosition, endPosition), 1, 2.5f);
    }

    private Vector2 ShootPosition = Vector2.zero;
    public void SaveShootPosition()
    {
        ShootPosition = transform.position;
    }

    public int AddApexStrideDamage(int damage)
    {
        if (Global.keystoneItemManager.ApexStrideLevel == 2)
        {
            return damage + (int)(damage * Global.keystoneItemManager.DamageBoostMultiplier);
        }
        return damage;
    }
}
