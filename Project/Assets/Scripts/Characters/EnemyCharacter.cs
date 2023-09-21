using UnityEngine;

public class EnemyCharacter : Character
{
    private void OnEnable()
    {
        type = CharType.Enemy;
    }

    protected override Vector2 GetMovementInput()
    {
        // Don't move if there is no player
        var player = GameManager.Instance.Player;
        if (!player)
        {
            return Vector2.zero;
        }
        
        // Otherwise, move towards player
        //
        // Note: To get a direction vector to A from B, use (A - B).normalized
        // Another way to remember is direction = (to - from).normalized
        // normalized ensures the direction vector has a length of 1
        return (player.transform.position - transform.position).normalized;
    }

    public override void TakeDamage(CharType type, float damage)
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
