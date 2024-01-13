
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class WeaponSway : MonoBehaviour
// {
//     [Header("Sway Settings")]
//     [SerializeField] private float smooth;
//     [SerializeField] private float swayMultiplier;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Get the input values using Input System API
//         float mouseX = Mouse.current.delta.x.ReadValue() * swayMultiplier;
//         float mouseY = Mouse.current.delta.y.ReadValue() * swayMultiplier;

//         Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
//         Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

//         Quaternion targetrotation = rotationX * rotationY;

//         transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrotation, smooth * Time.deltaTime);
//     }
// }
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] public Transform weaponTransform;

    [Header("Sway Properties")]
    [SerializeField]private float swayAmount = 0.01f;
    [SerializeField] public float maxSwayAmount = 0.1f;
    [SerializeField] public float swaySmooth = 9f;
    [SerializeField] public AnimationCurve swayCurve;

    [Range(0f, 1f)]
    [SerializeField] public float swaySmoothCounteraction = 1f;

    [Header("Rotation")]
    [SerializeField] public float rotationSwayMultiplier = 1f;

    [Header("Position")]
    [SerializeField] public float positionSwayMultiplier = -1f;
    

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector2 sway;

    private void Reset()
    {
        Keyframe[] ks = new Keyframe[] { new Keyframe(0, 0, 0, 2), new Keyframe(1, 1) };
        swayCurve = new AnimationCurve(ks);
    }

    private void Start()
    {
        if (!weaponTransform)
            weaponTransform = transform;
        initialPosition = weaponTransform.localPosition;
        initialRotation = weaponTransform.localRotation;
    }

    private void Update()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * swayAmount;
        float mouseY = Mouse.current.delta.y.ReadValue() * swayAmount;


        sway = Vector2.MoveTowards(sway, Vector2.zero, swayCurve.Evaluate(Time.deltaTime * swaySmoothCounteraction * sway.magnitude * swaySmooth));
        sway = Vector2.ClampMagnitude(new Vector2(mouseX, mouseY) + sway, maxSwayAmount);

        weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, new Vector3(sway.x, sway.y, 0) * positionSwayMultiplier + initialPosition, swayCurve.Evaluate(Time.deltaTime * swaySmooth));
        weaponTransform.localRotation = Quaternion.Slerp(weaponTransform.localRotation, initialRotation * Quaternion.Euler(Mathf.Rad2Deg * rotationSwayMultiplier * new Vector3(-sway.y, sway.x, 0)), swayCurve.Evaluate(Time.deltaTime * swaySmooth));
    }
}
