using UnityEngine;

namespace Characters
{
    public class PlayerFlip : MonoBehaviour
    {
        [SerializeField] private PlayerAim playerAim;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Update()
        {
            spriteRenderer.flipX = playerAim.AimWorldPosition.x < transform.position.x;
        }
    }
}
