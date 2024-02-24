public class Player : Entity
{
    public int currentSP;

    protected override void Start()
    {
        base.Start();

        currentSP = 0;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void AddSP(int sp)
    {
        currentSP += sp;
    }
}
