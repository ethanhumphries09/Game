using Engine;

namespace Engine.Components;
public abstract class Component
{
    public GameObject Parent { get; set; }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void Debug(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) { }
}