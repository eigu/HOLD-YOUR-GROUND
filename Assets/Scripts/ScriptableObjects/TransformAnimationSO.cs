using UnityEngine;

[CreateAssetMenu]
public class TransformAnimationSO : ScriptableObject
{
    public AnimationCurve RotationX = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    public AnimationCurve RotationY = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    public AnimationCurve RotationZ = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    public AnimationCurve PositionX = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    public AnimationCurve PositionY = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);
    public AnimationCurve PositionZ = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.0f, 0.0f);

    public float Duration = 0.2f;

    public bool IsRotating;
    public bool IsMoving;
}
