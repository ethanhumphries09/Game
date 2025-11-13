using Engine.Components;

namespace Engine;

public class GameObject(string name, Vector2 position) : List<Component>
{
    public Vector2 Position { get; set; } = position;
    public Vector2 PreviousPosition { get; protected set; } = position;
    public string Name { get; set; } = name;
    public virtual void Update(GameTime gameTime)
    {
        PreviousPosition = Position;
        foreach (var component in this)
        {
            component.Update(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in this)
        {
            component.Draw(spriteBatch);
        }
    }

    public new virtual void Add(Component component)
    {
        component.Parent = this;
        base.Add(component);
    }
    public virtual T GetComponent<T>() where T : Component => this.OfType<T>().FirstOrDefault();
}
