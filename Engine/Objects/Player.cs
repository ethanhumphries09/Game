namespace Engine;

public class Player(string name, Vector2 position) : GameObject(name, position)
{
    KeyboardState kb;
    readonly float speed = 200f;
    private Vector2 velocity = Vector2.Zero;
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        kb = Keyboard.GetState();
        Move(gameTime);
    }
    private void Move(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        velocity = Vector2.Zero;

        if (kb.IsKeyDown(Keys.W)) velocity.Y -= 1;
        if (kb.IsKeyDown(Keys.S)) velocity.Y += 1;
        if (kb.IsKeyDown(Keys.A)) velocity.X -= 1;
        if (kb.IsKeyDown(Keys.D)) velocity.X += 1;

        if (velocity != Vector2.Zero)
            velocity.Normalize();

        float moveX = velocity.X * speed * delta;
        float moveY = velocity.Y * speed * delta;

        
        // Optional jump (if grounded)
        if (kb.IsKeyDown(Keys.Space))
        {
            velocity.Y = -400f; // jump impulse
            
        }

        Position = new Vector2(Position.X + moveX, Position.Y);
        Collision.CollideX();

        Position = new Vector2(Position.X, Position.Y + moveY);
        Collision.CollideY();
    }
}

