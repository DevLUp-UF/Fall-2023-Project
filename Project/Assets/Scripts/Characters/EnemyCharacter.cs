using UnityEngine;

public class EnemyCharacter : Character
{
    protected override Vector2 GetMovementInput()
    {
        return Vector2.zero;
    }

    public override void TakeDamage(CharType type, int damage)
    {
        if (type == CharType.Player)
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
