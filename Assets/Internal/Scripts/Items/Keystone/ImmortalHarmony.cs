using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalHarmony : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;

    public float ExplosionDamage;
    public int HealPerHit;

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.PLAYER_TAKE_DAMAGE, OnPlayerHit);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.PLAYER_TAKE_DAMAGE, OnPlayerHit);
    }

    public void Initialize(float explosionDamage, int healPerHit, GameObject _explosionPrefab, GameObject _shieldPrefab)
    {
        shieldPrefab = _shieldPrefab;
        explosionPrefab = _explosionPrefab;

        ExplosionDamage = explosionDamage;
        HealPerHit = healPerHit;
    }

    public void OnPlayerHit(Dictionary<string, object> msg)
    {
        CreateExplosion();
        ShieldPlayer();
    }

    private void CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, Global.playerTransform.position, Quaternion.identity);
        explosion.GetComponent<ImmortalHarmonyExplosion>().Initialize(ExplosionDamage, HealPerHit);
    }

    private void ShieldPlayer()
    {
        GameObject shield = Instantiate(shieldPrefab, Vector3.zero, Quaternion.identity);
        shield.transform.SetParent(Global.playerTransform, false);
        shield.transform.localPosition = Vector3.zero;
        Destroy(shield, Global.keystoneItemManager.ImmortalHarmonyShieldTime + GlobalPlayer.GetStatValue(PlayerStatEnum.invincDuration));
    }
}
