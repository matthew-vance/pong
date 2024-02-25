using System.Globalization;
using System.Numerics;

using Raylib_cs;

namespace Pong;

public sealed class Program {

  private readonly static int screenWidth = 1280;
  private readonly static int screenHeight = 800;
  private static readonly int ballRadius = 20;
  private static readonly int ballSpeed = 8; // TODO: Make ball speed increase over time
  private static Ball ball = new(screenWidth / 2, screenHeight / 2, ballSpeed, ballSpeed, ballRadius);

  public Program() {
    ball = new Ball(screenWidth / 2, screenHeight / 2, ballSpeed, ballSpeed, ballRadius);
  }
  public static void Main() {
    var gameName = "Pong";
    var playerScore = 0;
    var opponentScore = 0;

    var paddleWidth = 25;
    var paddleHeight = 120;
    var paddleOffset = 10;
    var paddleSpeed = 6;

    Raylib.InitWindow(screenWidth, screenHeight, gameName);
    Raylib.SetTargetFPS(60);

    var player = new Paddle(paddleOffset, screenHeight / 2 - paddleHeight / 2, paddleWidth, paddleHeight, paddleSpeed);

    var opponent = new CpuPaddle(screenWidth - paddleOffset - paddleWidth, screenHeight / 2 - paddleHeight / 2, paddleWidth, paddleHeight, paddleSpeed);

    while (!Raylib.WindowShouldClose()) {
      Raylib.BeginDrawing();

      // Update
      ball.Update();
      player.Update();
      opponent.Update(ball.Y);

      // Check for collisions
      var ballCenter = new Vector2 { X = ball.X, Y = ball.Y };
      var playerRectangle = new Rectangle { X = player.X, Y = player.Y, Width = player.Width, Height = player.Height };
      var opponentRectangle = new Rectangle { X = opponent.X, Y = opponent.Y, Width = opponent.Width, Height = opponent.Height };

      if (Raylib.CheckCollisionCircleRec(ballCenter, ball.Radius, playerRectangle)) {
        ball.SpeedX *= -1;
      }
      else if (Raylib.CheckCollisionCircleRec(ballCenter, ball.Radius, opponentRectangle)) {
        ball.SpeedX *= -1;
      }

      if (ball.X + ball.Radius >= screenWidth) {
        playerScore++;
        Reset();
      }
      else if (ball.X - ball.Radius <= 0) {
        opponentScore++;
        Reset();
      }

      // Draw
      Raylib.ClearBackground(Color.Black);
      Raylib.DrawLine(screenWidth / 2, 0, screenWidth / 2, screenHeight, Color.White);
      ball.Draw();
      player.Draw();
      opponent.Draw();
      Raylib.DrawText(playerScore.ToString(CultureInfo.InvariantCulture), screenWidth / 2 - 100, 100, 50, Color.White);
      Raylib.DrawText(opponentScore.ToString(CultureInfo.InvariantCulture), screenWidth / 2 + 50, 100, 50, Color.White);

      Raylib.DrawFPS(10, 10);
      Raylib.EndDrawing();
    }

    Raylib.CloseWindow();
  }

  private static void Reset() {
    // TODO: Start each round with a 3, 2, 1 countdown
    // TODO: Make initial ball direction change based on who scored last
    ball.X = screenWidth / 2;
    ball.Y = screenHeight / 2;
  }
}
