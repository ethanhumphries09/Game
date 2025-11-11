using Engine.Components;

namespace Engine;
public class GameObject(Vector2 position) : List<Component>
{
    public Vector2 Position { get; set; } = position;

    public virtual void Update(GameTime gameTime)
    {
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

    public virtual void Debug(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {

        foreach (var component in this)
        {
            component.Debug(spriteBatch, graphicsDevice);
        }

    }

    public new virtual void Add(Component component)
    {
        component.Parent = this;
        base.Add(component);
    }
    public virtual T GetComponent<T>() where T : Component => this.OfType<T>().FirstOrDefault();
}
