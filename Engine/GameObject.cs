using Engine.Components;

namespace Engine;
public class GameObject(Vector2 position) : List<Component>
{
    public Vector2 Position { get; set; } = position;

    public void Update(GameTime gameTime)
    {
        foreach (var component in this)
        {
            component.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(var component in this)
        {
            component.Draw(spriteBatch);
        }
    }

    public new void Add(Component component)
    {
        component.Parent = this;
        base.Add(component);
    }
}
