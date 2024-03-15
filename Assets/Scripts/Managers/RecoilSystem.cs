using System.Collections;
using UnityEngine;

public class RecoilSystem : MonoBehaviour
{
    [Header("Animation Curve")]
    // All recoil curves must start and end with zero.
    [SerializeField] private AnimationCurve _rotationX = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private AnimationCurve _rotationY = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private AnimationCurve _rotationZ = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private AnimationCurve _positionX = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private AnimationCurve _positionY = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private AnimationCurve _positionZ = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField] private float _animationCurveTime = 0;
    [SerializeField] private float _duration = 0.2f;
    private float m_timePassed;
    private Coroutine _recoil;

    private IEnumerator StartRecoil()
    {

        m_timePassed = 0;
        _animationCurveTime = 0;
        while (m_timePassed < _duration)
        {
            m_timePassed += Time.deltaTime;
            _animationCurveTime = m_timePassed / _duration;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(CalculateNextRotation()), _animationCurveTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, CalculateNextPosition(), _animationCurveTime);
            yield return null;
        }
    }

    private Vector3 CurrentRotation, CurrentPosition;

    public Vector3 CalculateNextRotation()
    {
        float Rotationx = _rotationX.Evaluate(_animationCurveTime);
        float Rotationy = _rotationY.Evaluate(_animationCurveTime);
        float Rotationz = _rotationZ.Evaluate(_animationCurveTime);

        CurrentRotation =
                     new Vector3(
                     Rotationx,
                     Rotationy,
                     Rotationz
                    );

        return CurrentRotation;
    }

    public Vector3 CalculateNextPosition()
    {
        float Positionx = _positionX.Evaluate(_animationCurveTime);
        float Positiony = _positionY.Evaluate(_animationCurveTime);
        float Positionz = _positionZ.Evaluate(_animationCurveTime);

        CurrentPosition =
                  new Vector3
                  (
                  Positionx,
                  Positiony,
                  Positionz
                  );
        return CurrentPosition;
    }

    public void ApplyRecoil()
    {
        if (_recoil != null)
        {
            StopCoroutine(_recoil);
        }
        _recoil = StartCoroutine(StartRecoil());

    }
}

