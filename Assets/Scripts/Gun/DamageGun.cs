using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGun : MonoBehaviour
{
    public float Damage;
    public float BulletRange;
    private Transform PlayerCamera;
    public RecoilSystem recoil;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public Transform attackpoint;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    // Update is called once per frame
public void shoot()
{
    recoil.recoil();
    Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward);
    if (Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
    {
        Collider hitCollider = hitInfo.collider; // Use lowercase 'c' for collider
        if (hitCollider.gameObject.TryGetComponent(out Entity enemy))
        {
            enemy.Health -= Damage;
        }
    }

    // Instantiate muzzle flash
    GameObject newMuzzleFlash = Instantiate(muzzleFlash, attackpoint.position, Quaternion.identity);

    // Match the rotation of the muzzle flash with the rotation of the weapon
    newMuzzleFlash.transform.rotation = Quaternion.Euler(0, -90, 0) * transform.rotation;

    // Destroy muzzle flash after a certain delay (e.g., 0.5 seconds)
    Destroy(newMuzzleFlash, 0.5f);
}



}
