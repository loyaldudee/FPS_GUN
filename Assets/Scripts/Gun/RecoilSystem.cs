using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecoilSystem : MonoBehaviour
{
    Vector3 currentRotation, targetRotation, targetPosition, currentPosition, initialGunPosition;
    public Transform cam;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    [SerializeField] private float kickBack;
    public float snapiness, returnAmount;

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        initialGunPosition = transform.localPosition;
        sway();
    }

    // Update is called once per frame
    void Update()
    {
        
        targetRotation = Vector3.Lerp(targetRotation,Vector3.zero, Time.deltaTime * returnAmount);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snapiness);
        transform.localRotation = Quaternion.Euler(currentRotation);
        
        back();
        
         
        
    }
    void sway()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * swayMultiplier;
        float mouseY = Mouse.current.delta.y.ReadValue() * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetrotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrotation, smooth * Time.deltaTime);
    }
    public void recoil()
    {
        targetPosition -= new Vector3(0,0,kickBack);
        targetRotation += new Vector3(recoilX,Random.Range(-recoilY,recoilY), Random.Range(-recoilZ, recoilZ));

    }

    void back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * snapiness);
        transform.localPosition = currentPosition;
    }
}
