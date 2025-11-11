namespace Engine.Components;
public class Movement : Component
{
    KeyboardState kb;
    readonly float speed = 200f;
    public override void Update(GameTime gameTime)
    {
        kb = Keyboard.GetState();
        Move(gameTime);
    }
    private void Move(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 movement = Vector2.Zero;

        if (kb.IsKeyDown(Keys.W))
            movement.Y -= 1;
        if (kb.IsKeyDown(Keys.S))
            movement.Y += 1;
        if (kb.IsKeyDown(Keys.A))
            movement.X -= 1;
        if (kb.IsKeyDown(Keys.D))
            movement.X += 1;

        if (movement != Vector2.Zero)
            movement.Normalize();

        // Move the parent GameObject
        Parent.Position += movement * speed * delta;
    }
}
