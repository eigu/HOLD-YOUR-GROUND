using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PowerUpInfo m_currentPowerUpInfo;

    [SerializeField] private Vector3 _playerOrientation;
    [SerializeField] private Transform _cameraHolder;

    [SerializeField, Range(0, 2)] private float _sensitivity;
    [SerializeField, Range(0, 100)] private float _smoothing;

    private Vector2 m_smoothedDelta = Vector2.zero;

    private void Start()
    {
        m_currentPowerUpInfo = FindObjectOfType<PowerUpInfo>();
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Vector3 mouseDelta = InputManager.Instance.GetMouseDelta();
        mouseDelta *= _sensitivity;
        m_smoothedDelta = Vector2.Lerp(m_smoothedDelta, mouseDelta, 1f / _smoothing);
     
        _playerOrientation.x += mouseDelta.x;
        _playerOrientation.y -= mouseDelta.y;

        _playerOrientation.x = Mathf.Repeat(_playerOrientation.x, 360f);
        _playerOrientation.y = Mathf.Clamp(_playerOrientation.y, -30f, 30f);

        _cameraHolder.rotation = Quaternion.Euler(new Vector3(_playerOrientation.y, transform.rotation.eulerAngles.y, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, _playerOrientation.x, 0));
    }
}
