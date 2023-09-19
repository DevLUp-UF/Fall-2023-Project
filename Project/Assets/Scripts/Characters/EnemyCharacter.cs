using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public override void TakeDamage(Enums.CharType type, int damage)
    {
        if (type == Enums.CharType.Player)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }
}
