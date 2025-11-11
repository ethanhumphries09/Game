using Engine.Physics;

namespace Engine.Components;
public class Collider : Component
{
    private Point Size { get; set; } = Point.Zero;
    private Point Offset { get; set; }

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

            Vector2 worldPos = Parent.Position + Offset.ToVector2();
            return new Rectangle(
                (int)worldPos.X,    // X position
                (int)worldPos.Y,    // Y position
                size.X,             // Width
                size.Y              // Height
            );
        }
    }

    public Collider()
    {
       // Collision.Colliders.Add(this);
    }


    public Collider(Point size, Point offset) : this()
    {
        Size = size;
        Offset = offset;
        Collision.Colliders.Add(this);
    }

    public override void Debug(SpriteBatch spriteBatch, GraphicsDevice graphics)
    {
        Texture2D pixel;
        pixel = new Texture2D(graphics, 1, 1);
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