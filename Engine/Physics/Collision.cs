namespace Engine.Physics;

public class Collision
{
    public static List<Collider> Colliders { get; } = new();
    public static void Collide()
    {
        foreach (var collider in Colliders)
        {
            foreach (var collider2 in Colliders)
            {
                if (collider != collider2)
                {
                    // Current position and previous position
                    Vector2 current = collider.Parent.Position;
                    Vector2 previous = collider.Parent.PreviousPosition;

                    // Horizontal movement first
                    Rectangle horizontalBounds = new Rectangle(
                        (int)(current.X),
                        (int)(previous.Y),
                        collider.Bounds.Width,
                        collider.Bounds.Height
                    );

                    if (horizontalBounds.Intersects(collider2.Bounds))
                    {
                        // Collision on X-axis, revert X
                        current.X = previous.X;
                    }

                    // Vertical movement next
                    Rectangle verticalBounds = new Rectangle(
                        (int)(current.X),
                        (int)(current.Y),
                        collider.Bounds.Width,
                        collider.Bounds.Height
                    );

                    if (verticalBounds.Intersects(collider2.Bounds))
                    {
                        // Collision on Y-axis, revert Y
                        current.Y = previous.Y;
                    }

                    // Apply the resolved position
                    collider.Parent.Position = current;
                }
            }
        }
    }
}
