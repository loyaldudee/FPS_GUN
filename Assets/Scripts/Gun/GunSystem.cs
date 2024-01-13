using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;  // Add this line to include the new Input System namespace

public class GunSystem : MonoBehaviour 
{
    public UnityEvent OnGunShoot;
    public float Firecooldown;
    public bool Automatic;
    private float CurrentCooldown;

    private void Start() {
        CurrentCooldown = Firecooldown;
    }

    void Update()
    {
        // Use the new Input System methods
        if (Automatic)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                if (CurrentCooldown <= 0f)
                {
                    OnGunShoot?.Invoke();
                    CurrentCooldown = Firecooldown;
                }
            }
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (CurrentCooldown <= 0f)
                {
                    OnGunShoot?.Invoke();
                    CurrentCooldown = Firecooldown;
                }
            }
        }
        CurrentCooldown -= Time.deltaTime;
    }
}
