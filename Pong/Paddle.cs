using Raylib_cs;

namespace Pong;

public class Paddle(int x, int y, int width, int height, int speed) {

  public int X { get; set; } = x;
  public int Y { get; set; } = y;
  public int Width { get; set; } = width;
  public int Height { get; set; } = height;
  public int Speed { get; set; } = speed;
  public void Draw() {
    Raylib.DrawRectangle(X, Y, Width, Height, Color.White);
  }

  public void Update() {
    HandleInput();
  }

  private void HandleInput() {
    if (Raylib.IsKeyDown(KeyboardKey.W)) {
      Y -= Speed;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.S)) {
      Y += Speed;
    }

    RestrictMovementToScreenBounds();
  }

  protected void RestrictMovementToScreenBounds() {
    var isAtTopOfScreen = Y <= 0;
    var isAtBottomOfScreen = Y + Height >= Raylib.GetScreenHeight();

    if (isAtTopOfScreen) {
      Y = 0;
    }

    if (isAtBottomOfScreen) {
      Y = Raylib.GetScreenHeight() - Height;
    }
  }
}
