namespace Engine.Components;

public class Collider : Component
{
    private Point Size { get; set; } = Point.Zero;


    public Rectangle Bounds
    {
        get  // Calculated every time you access it
        {
            Point size = Size;

            // If size is zero, get from sprite
            if (size == Point.Zero)
            {
                var sprite = Parent?.GetComponent<Sprite>();  // ?. prevents crash if Parent is null
                if (sprite != null)
                {
                    size = new Point(sprite.Texture.Width, sprite.Texture.Height);
                }
            }


            return new Rectangle(
                (int)Parent.Position.X,    // X position
                (int)Parent.Position.Y,    // Y position
                size.X,             // Width
                size.Y              // Height
            );
        }
    }

    public Collider()
    {
        Collision.Colliders.Add(this);
    }




    public override void Draw(SpriteBatch spriteBatch)
    {
        Texture2D pixel;
        pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);
        // Top
        spriteBatch.Draw(pixel, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, 1), Color.White);
        // Bottom
        spriteBatch.Draw(pixel, new Rectangle(Bounds.X, Bounds.Bottom - 1, Bounds.Width, 1), Color.White);
        // Left
        spriteBatch.Draw(pixel, new Rectangle(Bounds.X, Bounds.Y, 1, Bounds.Height), Color.White);
        // Right
        spriteBatch.Draw(pixel, new Rectangle(Bounds.Right - 1, Bounds.Y, 1, Bounds.Height), Color.White);

    }
}