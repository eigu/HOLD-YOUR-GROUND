using UnityEngine;

public class GunSway : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private float _multiplier;

    [SerializeField] private float _bobbingAmount;
    [SerializeField] private float _bobbingSpeed;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.localPosition;
    }

    private void Update()
    {
        Sway();
        Bobbing();
    }

    private void Sway()
    {
        float mouseX = InputManager.Instance.GetMouseDelta().x * _multiplier;
        float mouseY = InputManager.Instance.GetMouseDelta().y * _multiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _smooth * Time.deltaTime);
    }

    private void Bobbing()
    {
        float verticalMovement = Mathf.Sin(Time.time * _bobbingSpeed) * _bobbingAmount;

        Vector3 newPosition = _initialPosition + Vector3.up * verticalMovement;
        transform.localPosition = newPosition;
    }
}
