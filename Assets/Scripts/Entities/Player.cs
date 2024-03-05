using UnityEngine;

public class Player : Entity
{
    [SerializeField] private IntVariableSO KillCount;

    protected override void Start()
    {
        base.Start();

        KillCount.Value = 0;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void AddSP(int point)
    {
        if (KillCount.Value < 6)
        {
            KillCount.Value += point;
        }
    }
}
