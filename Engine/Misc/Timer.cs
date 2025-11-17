namespace Engine.Misc;

public class Timer(float time)
{
    public float timer = 0;
    public void Update(GameTime gameTime)
    {
        if(timer > 0)
        {
            timer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
    public void Start()
    {
        timer = time;
    }
    public bool Completed()
    {
        return timer <= 0;
    }
}