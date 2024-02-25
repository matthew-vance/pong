using Raylib_cs;

namespace Pong;

public sealed class Ball(int x, int y, int speedX, int speedY, int radius) {
  public int X { get; set; } = x;
  public int Y { get; set; } = y;
  public int Radius { get; set; } = radius;
  public int SpeedX { get; set; } = speedX;
  public int SpeedY { get; set; } = speedY;

  public void Draw() {
    Raylib.DrawCircle(X, Y, Radius, Color.White);
  }

  public void Update() {
    X += SpeedX;
    Y += SpeedY;

    if (Y + Radius >= Raylib.GetScreenHeight() || Y - Radius <= 0) {
      SpeedY *= -1;
    }
    if (X + Radius >= Raylib.GetScreenWidth() || X - Radius <= 0) {
      SpeedX *= -1;
    }
  }
}
