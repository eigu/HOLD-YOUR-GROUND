using UnityEngine;

[System.Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariableSO Variable;

    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}
