namespace Pong;

public class CpuPaddle : Paddle {
  public CpuPaddle(int x, int y, int width, int height, int speed) : base(x, y, width, height, speed) {
  }
  public void Update(int ballY) {
    var paddleCenter = Y + Height / 2;
    var isBallAbovePaddle = ballY < paddleCenter;
    var isBallBelowPaddle = ballY > paddleCenter;
    if (isBallAbovePaddle) {
      Y -= Speed;
    }
    else if (isBallBelowPaddle) {
      Y += Speed;
    }

    RestrictMovementToScreenBounds();
  }
}
