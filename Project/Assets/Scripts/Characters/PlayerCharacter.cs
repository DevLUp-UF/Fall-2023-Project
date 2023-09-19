using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override void TakeDamage(Enums.CharType type, int damage)
    {
        if(type == Enums.CharType.Enemy)
        {
            health -= damage;
            if(health < 0)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }
}
