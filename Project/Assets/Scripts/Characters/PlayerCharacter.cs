using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    // This uses the new Unity Input System, which is not installed by default
    // To install it, press Ctrl+K and type in "Window/Package Manager", then search for "Input System"
    [Header("Player Input")]
    [SerializeField]
    protected InputActionReference Movement;

    protected void OnEnable()
    {
        // Register Player to GameManager
        GameManager.Instance.Player = this;
        type = CharType.Player;
    }

    private void OnDisable()
    {
        // If statement handles edge case
        // GameManager.Instance might be null when the scene unloads
        if (GameManager.Exists)
        {
            // Unregister Player from GameManager
            GameManager.Instance.Player = null;
        }
    }

    protected override Vector2 GetMovementInput()
    {
        return Movement.action.ReadValue<Vector2>();
    }

    public override void TakeDamage(CharType type, float damage)
    {
        if (type == CharType.Enemy || type == CharType.Obstacle)
        {
            health -= damage;
            if (health < 0.05)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }
}
