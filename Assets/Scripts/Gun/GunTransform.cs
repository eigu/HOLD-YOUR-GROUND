using System.Collections;
using UnityEngine;

public class GunTransform : MonoBehaviour
{
    [SerializeField] private TransformAnimationSO _gunTransform;

    private float m_animationCurveTime = 0;
    private float m_timePassed;
    private Coroutine _recoil;

    private IEnumerator StartRecoil()
    {
        m_timePassed = 0;
        m_animationCurveTime = 0;
        while (m_timePassed < _gunTransform.Duration)
        {
            m_timePassed += Time.deltaTime;
            m_animationCurveTime = m_timePassed / _gunTransform.Duration;

            if (_gunTransform.IsRotating)
            {
                RotateObject(m_animationCurveTime);
            }

            if (_gunTransform.IsMoving)
            {
                MoveObject(m_animationCurveTime);
            }

            yield return null;
        }
    }

    private void RotateObject(float animationCurveTime)
    {
        Vector3 rotation = CalculateNextRotation(animationCurveTime);

        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.localRotation = targetRotation;
    }

    private void MoveObject(float animationCurveTime)
    {
        Vector3 position = CalculateNextPosition(animationCurveTime);

        transform.localPosition = position;
    }

    private Vector3 CalculateNextRotation(float animationCurveTime)
    {
        float rotationX = _gunTransform.RotationX.Evaluate(animationCurveTime);
        float rotationY = _gunTransform.RotationY.Evaluate(animationCurveTime);
        float rotationZ = _gunTransform.RotationZ.Evaluate(animationCurveTime);

        return new Vector3(rotationX, rotationY, rotationZ);
    }

    private Vector3 CalculateNextPosition(float animationCurveTime)
    {
        float positionX = _gunTransform.PositionX.Evaluate(animationCurveTime);
        float positionY = _gunTransform.PositionY.Evaluate(animationCurveTime);
        float positionZ = _gunTransform.PositionZ.Evaluate(animationCurveTime);

        return new Vector3(positionX, positionY, positionZ);
    }

    public void ApplyTransform()
    {
        if (_recoil != null)
        {
            StopCoroutine(_recoil);
        }
        _recoil = StartCoroutine(StartRecoil());

    }
}
