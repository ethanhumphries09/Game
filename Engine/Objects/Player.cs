using Engine.Components;
using System.Diagnostics;

namespace Engine;

public class Player(string name, Vector2 position) : GameObject(name, position)
{
    private KeyboardState kb;
    private KeyboardState kbp;

    private Vector2 Velocity = Vector2.Zero;

    private const float speed = 200;
    private const float acceleration = 5f;
    private const float deceleration = 10f;

    private const float jumpStrength = 15f;
    private const float dashStrength = 30f;
    private const float gravity = 50f;
    private const float terminalVelocity = 1000f;

    private int maxJumps = 2;
    private int jumps;

    private float targetSpeed = 0;

    bool left => kb.IsKeyDown(Keys.A);
    bool right => kb.IsKeyDown(Keys.D);

    //private bool canJump;

    private Collider Collider => GetComponent<Collider>();
    private Rectangle PreviousBounds = Rectangle.Empty;
    private Rectangle GroundedBox;
    private Rectangle ClimbingBox;

    private Timer dashTimer = new(1);

    private bool grounded;
    private bool climbing;

    private bool CheckClimbing()
    {
        if (!grounded)
        {
            ClimbingBox = new Rectangle(
                Collider.Bounds.X - 1,
                Collider.Bounds.Y,
                Collider.Bounds.Width + 2,
                1
            );
            foreach (var collider in Collision.Colliders)
            {
                if (collider != Collider && ClimbingBox.Intersects(collider.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public bool CheckGrounded()
    {
        GroundedBox = new Rectangle(
            Collider.Bounds.X,
            Collider.Bounds.Bottom + 1,
            Collider.Bounds.Width,
            1
        );
        foreach (var collider in Collision.Colliders)
        {
            if (GroundedBox.Intersects(collider.Bounds))
            {
                return true;
            }
        }
        return false;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Color color;
        base.Draw(spriteBatch);
        if (Grounded()) color = Color.Blue;
        else color = Color.Green;
        spriteBatch.Draw(MyGame.Core.Main.pixel, GroundedBox, color);

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        kbp = kb;
        kb = Keyboard.GetState();

        dashTimer.Update(gameTime);
        grounded = CheckGrounded();
        climbing = CheckClimbing();

        PreviousBounds = Collider.Bounds;
        Move(gameTime);

    }
    private void Move(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;


        targetSpeed = 0f;


        if (left && !right)
            targetSpeed = -speed * delta;
        else if (right && !left)
            targetSpeed = speed * delta;
        if (targetSpeed == 0f)
            Velocity.X = MathHelper.Lerp(Velocity.X, targetSpeed, deceleration * delta);
        else
            Velocity.X = MathHelper.Lerp(Velocity.X, targetSpeed, acceleration * delta);

        //not grounded
        if (!grounded)
        {
            if (Velocity.Y < terminalVelocity * delta)
            {
                Velocity.Y += gravity * delta;
            }
            else if (Velocity.Y > terminalVelocity * delta)
            {
                Velocity.Y = terminalVelocity * delta;
            }

            if (jumps == maxJumps)
            {
                jumps--;
            }
        }
        //grounded
        else
        {
            Velocity.Y = 0;
            jumps = maxJumps;
        }

        if(climbing)
        {
            Velocity.Y = 50 * delta;
        }

        //jump
        if ((climbing || jumps >= 1) && kb.IsKeyDown(Keys.Space) && !kbp.IsKeyDown(Keys.Space))
        {
            Jump();
        }

        //dash
        if (kb.IsKeyDown(Keys.LeftShift) && !kbp.IsKeyDown(Keys.LeftShift))
        {
            Dash();
        }

        //move and collide
        Position = new Vector2(Position.X + Velocity.X, Position.Y);
        if (Position.X != PreviousPosition.X)
            CollideX();

        Position = new Vector2(Position.X, Position.Y + Velocity.Y);
        if (Position.Y != PreviousPosition.Y)
            CollideY();

    }
    public void CollideX()
    {
        foreach (var collider2 in Collision.Colliders)
        {
            if (Collider != collider2)
            {
                if (Collider.Bounds.Intersects(collider2.Bounds))
                {
                    //right side
                    if (PreviousBounds.Right <= collider2.Bounds.Left)
                    {

                        Position = new(collider2.Bounds.Left - Collider.Bounds.Width, Position.Y);

                        Velocity.X = 0;
                    }
                    //left
                    else if (PreviousBounds.Left >= collider2.Bounds.Right)
                    {
                        Position = new(collider2.Bounds.Right, Position.Y);

                        Velocity.X = 0;
                    }
                }
            }
        }
    }

    public void CollideY()
    {
        foreach (var collider2 in Collision.Colliders)
        {
            if (Collider != collider2)
            {
                if (Collider.Bounds.Intersects(collider2.Bounds))
                {
                    //top
                    if (PreviousBounds.Bottom <= collider2.Bounds.Top)
                    {
                        Position = new(Position.X, collider2.Bounds.Top - Collider.Bounds.Height - Collider.Offset.Y);


                    }
                    //bottom
                    else if (PreviousBounds.Top >= collider2.Bounds.Bottom)
                    {
                        Position = new(Position.X, collider2.Bounds.Bottom - Collider.Bounds.Height);

                        Velocity.Y = 0;

                    }
                }
            }
        }
    }
    public void Dash()
    {
        if (right)
        {
            Velocity.Y = 0;
            Velocity.X = dashStrength;
            dashTimer.Start();
        }
        if (left)
        {
            Velocity.Y = 0;
            Velocity.X = -dashStrength;
            dashTimer.Start();
        }
    }
    public void Jump()
    {
        if (!climbing)
        {
            Velocity.Y = -jumpStrength;
            jumps--;
        }
        else
        {
            Velocity.Y = -jumpStrengthd;
            Velocity.X = -10;
            jumps = 1;
        }
    }
}



