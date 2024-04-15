using System.Diagnostics;

public interface IPlayerLife
{
    void LifeAdd(int life);
    void LifeRemove(int life);
    void ResetLife();
    bool Get();
}

class PlayerLife : IPlayerLife
{
    private int LifePoint;
    private int NoLife;
    private bool Gameover;

    public PlayerLife()
    {
        LifePoint = 3;
        NoLife = 0;
        Gameover = false;
        ServiceLocator.RegisterService<IPlayerLife>(this);
    }

    public void LifeAdd(int life)
    { LifePoint += life; }

    public void LifeRemove(int life)
    {
        LifePoint -= life;

        if (LifePoint.Equals(NoLife))
        { Gameover = true; }
    }

    public void ResetLife()
    {
        LifePoint = 3;
        Gameover = false;
    }
    public int GetLifePoint()
        => LifePoint;

    public bool Get()
        => Gameover;

    public void Display()
    { Debug.WriteLine("Life : " + LifePoint + " || GameOver : " + Get()); }
}