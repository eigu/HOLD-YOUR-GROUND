public class Player : Entity
{
  public int CurrentSP { get; set; }

  protected override void Start()
  {
    base.Start();

    CurrentSP = 0;
  }

  protected override void Update()
  {
    base.Update();
  }

  public void AddSP(int sp)
  {
    CurrentSP += sp;
  }
}
