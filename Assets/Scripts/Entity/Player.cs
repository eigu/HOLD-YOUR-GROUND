public class Player : Entity
{
  public int KillCount { get; set; }

  protected override void Start()
  {
    base.Start();

    KillCount = 0;
  }

  protected override void Update()
  {
    base.Update();
  }

  public void AddSP(int point)
  {
    if (KillCount < 6)
    {
      KillCount += point;
    }
  }
}
