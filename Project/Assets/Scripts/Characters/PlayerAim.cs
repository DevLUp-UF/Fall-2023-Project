using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference aim;

    // Usually I would mark these as [NonSerialized] so the data here isn't saved nor shown in the inspector
    // However, showing the data can be useful for visualization and debugging
    [Header("Debug")]
    public Vector2 AimScreenPosition;
    public Vector2 AimWorldPosition;

    private void Update()
    {
        // Read current aim (mouse) position
        AimScreenPosition = aim.action.ReadValue<Vector2>();

        var camera = Camera.main;

        // Get a ray going from the aimed position pointing towards the world
        var ray = camera.ScreenPointToRay(AimScreenPosition);

        // Create a plane that represents the world
        // Note: This plane doesn't actually exist physically in the game world. It is only used to represent it.
        var plane = new Plane(
            Vector3.back, // Point the plane towards the camera
            transform.position); // Center it on the player

        // Get the intersection point between the ray and the world plane
        if (plane.Raycast(ray, out var distance))
        {
            var point = ray.GetPoint(distance);
            AimWorldPosition = point;
        }

        // Also works, but only reliable for 2D games
        // The technique above of generating a plane and calculating an intersection with it works for 2D and 3D games
        // AimWorldPosition = camera.ScreenToWorldPoint(AimScreenPosition);
    }
}
