namespace Engine.Components;
public class Sprite(Texture2D texture) : Component
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Parent.Position, Color.White);
    }
}
