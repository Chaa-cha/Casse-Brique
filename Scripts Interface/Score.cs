using System.Diagnostics;

public interface IScore
{
    void ScoreAdd(int score);
    void ResetScore();
    int Get();
}

class Score : IScore
{
    private int TotalScore;

    public Score()
    {
        TotalScore = 0;

        ServiceLocator.RegisterService<IScore>(this);
    }

    public void ScoreAdd(int score)
    { TotalScore += score; }

    public void ResetScore()
    { TotalScore = 0; }

    public int Get()
        => TotalScore;

    public void Display()
    { Debug.WriteLine("Score : " + Get()); }
}