    using UnityEngine;

    public class PlayerLook : MonoBehaviour
    {
        private PowerUpInfo m_currentPowerUpInfo;

        [SerializeField] private Vector3 _playerOrientation;
        [SerializeField] private Transform _cameraHolder;
        [SerializeField] private Transform _playerHand;

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
            HandAim();
        }

        private void Look()
        {
            Vector3 mouseDelta = InputManager.Instance.GetMouseDelta();
            mouseDelta *= _sensitivity;
            m_smoothedDelta = Vector2.Lerp(m_smoothedDelta, mouseDelta, 1f / _smoothing);

            Transform lockedEnemy = m_currentPowerUpInfo.GetComponent<AutoAim>().LockedEnemy;

            if (lockedEnemy != null)
            {
                Vector3 lookDirection = lockedEnemy.position - transform.position;

                float horizontalAngle = Vector3.Angle(transform.forward, new Vector3(lookDirection.x, 0, lookDirection.z));


                Vector3 directionToEnemy = (lockedEnemy.position - _cameraHolder.position).normalized;
                float distance = Vector3.Distance(_cameraHolder.position, lockedEnemy.position);
                float verticalAngle = Mathf.Atan2(directionToEnemy.y, distance) * Mathf.Rad2Deg;

                int rotationDirection = Vector3.Cross(transform.forward, lookDirection).y > 0 ? 1 : -1;

                _playerOrientation.x += horizontalAngle * rotationDirection * Time.deltaTime * 5f;
                _playerOrientation.y = verticalAngle;
            }
            else
            {
                _playerOrientation.x += mouseDelta.x;
                _playerOrientation.y -= mouseDelta.y;
            }

            _playerOrientation.x = Mathf.Repeat(_playerOrientation.x, 360f);
            _playerOrientation.y = Mathf.Clamp(_playerOrientation.y, -30f, 30f);

            _cameraHolder.rotation = Quaternion.Euler(new Vector3(_playerOrientation.y, transform.rotation.eulerAngles.y, 0));
            transform.rotation = Quaternion.Euler(new Vector3(0, _playerOrientation.x, 0));
        }

        private void HandAim()
        {
            if (_playerHand != null)
            {
                _playerHand.LookAt(InputManager.Instance.GetCrosshairPoint());
            }
        }
    }
