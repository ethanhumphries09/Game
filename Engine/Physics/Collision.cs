using Engine.Components;

namespace Engine.Physics;
public class Collision
{
    public static List<Collider> Colliders { get; } = new();

    public static void Collide()
    {
       /* foreach (var collider in Colliders)
        {
            foreach (var collider2 in Colliders)
            {
                if(collider != collider2)
                {
                    if(collider.Bounds.Intersects(collider2.Bounds)) {
                       // collider.Parent.Position = new(150, 150);
                    }
                }
            }
        }*/
    }
}
