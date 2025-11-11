namespace Engine.Components;
public class Sprite(Texture2D texture) : Component
{
    public Texture2D Texture { get; } = texture;
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Parent.Position, Color.White);
    }
}
