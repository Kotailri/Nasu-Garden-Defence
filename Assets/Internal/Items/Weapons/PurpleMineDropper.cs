using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMineDropper : PlayerAttack
{
    [Header("Purple Mine Dropper")]
    public int maxMineCount;

    private List<GameObject> mines = new();

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        for (int i = mines.Count - 1; i >= 0; i--)
        {
            if (mines[i] == null)
            {
                mines.RemoveAt(i);
            }
        }

        AudioManager.instance.PlaySound(AttackSound);
        GameObject mine = Instantiate(AttackPrefab, transform.position, Quaternion.identity);
        mine.GetComponent<PurpleMine>().SetDamage(BaseDamage);
        mines.Add(mine);

        if (mines.Count == maxMineCount + 1)
        {
            if (mines[0] != null)
            {
                Destroy(mines[0]);
            }
        }
    }
}
