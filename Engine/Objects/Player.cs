using Engine.Components;

namespace Engine;

public class Player(string name, Vector2 position) : GameObject(name, position)
{
    KeyboardState kb;
    KeyboardState kbp;
    readonly float speed = 200f;
    private Vector2 Velocity = Vector2.Zero;
    private float gravity = 200f;
    public bool Grounded { get; set; } = false;
    private Collider collider => GetComponent<Collider>();
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        kbp = kb;
        kb = Keyboard.GetState();
        Move(gameTime);
    }
    private void Move(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Velocity.X = 0;

        if (Grounded)
        {
            Velocity.Y = 0;
        }

        if (kb.IsKeyDown(Keys.A)) Velocity.X -= speed * delta;
        if (kb.IsKeyDown(Keys.D)) Velocity.X += speed * delta;

        if (!Grounded && Velocity.Y < gravity)
            Velocity.Y += (gravity / 10) * delta;

        float moveX = Velocity.X;
        float moveY = Velocity.Y;


        if (Grounded && kb.IsKeyDown(Keys.Space) && !kbp.IsKeyDown(Keys.Space))
        {
            Grounded = false;
            Velocity.Y = -10; // jump impulse

        }

        Position = new Vector2(Position.X + moveX, Position.Y);
        CollideX();

        Position = new Vector2(Position.X, Position.Y + moveY);
        CollideY();
    }
    public void CollideX()
    {
        foreach (var collider2 in Collision.Colliders)
        {
            if (collider != collider2)
            {
                if (collider.Bounds.Intersects(collider2.Bounds))
                {
                    collider.Parent.Position = new(collider.Parent.PreviousPosition.X, collider.Parent.Position.Y);
                }
            }
        }
    }
    
    public void CollideY()
    {
        foreach (var collider2 in Collision.Colliders)
        {
            if (collider != collider2)
            {
                if (collider.Bounds.Intersects(collider2.Bounds))
                {
                    collider.Parent.Position = new(collider.Parent.Position.X, collider.Parent.PreviousPosition.Y);
                    if (Velocity.Y > 0.0f)
                    {
                        Grounded = true;
                    }
                }
            }
        }
    }
}


