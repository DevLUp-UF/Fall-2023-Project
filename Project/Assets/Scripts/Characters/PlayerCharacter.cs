using UnityEngine;

public class PlayerCharacter : Character
{
    protected void OnEnable()
    {
        // Register Player to GameManager
        GameManager.Instance.Player = this;
    }

    private void OnDisable()
    {
        // Unregister Player from GameManager
        GameManager.Instance.Player = null;
    }

    protected override Vector2 GetMovementInput()
    {
        return Vector2.zero;
    }

    public override void TakeDamage(CharType type, int damage)
    {
        if (type == CharType.Enemy)
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
