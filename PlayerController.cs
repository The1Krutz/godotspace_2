using Godot;
using System;

namespace GodotSpace2;

/// <summary>
/// template
/// </summary>
public partial class PlayerController : Node
{
  // Signals

  // Exports
  [Export]
  public CharacterBody3D PlayerBody;
  [Export]
  public float PitchSpeed = 1.0f;
  [Export]
  public float RollSpeed = 1.0f;
  [Export]
  public float YawSpeed = 1.0f;
  [Export]
  public float VerticalSpeed = 50.0f;
  [Export]
  public float LateralSpeed = 50.0f;
  [Export]
  public float ForwardSpeed = 50.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private float pitchInput;
  private float rollInput;
  private float yawInput;
  private float verticalInput;
  private float lateralInput;
  private float forwardInput;
  private bool emergencyBrake;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready()
  {
    GD.Print("Ready");

    PlayerBody.GetNode<Camera3D>("Camera").Current = true;
  }

  public override void _Process(double delta)
  {
    float deltaf = (float)delta;

    GetInput();

    if (emergencyBrake)
    {
      PlayerBody.Velocity = Vector3.Zero;
    }
    else
    {
      Vector3 movementDir = PlayerBody.Transform.Basis * new Vector3(
        forwardInput * ForwardSpeed,
        verticalInput * VerticalSpeed,
        lateralInput * LateralSpeed
      );

      PlayerBody.MoveAndCollide(movementDir * deltaf);

      Transform3D transform = PlayerBody.Transform;
      Basis basis = PlayerBody.Transform.Basis;

      basis = basis.Rotated(basis.X, rollInput * RollSpeed * deltaf);
      basis = basis.Rotated(basis.Y, -yawInput * YawSpeed * deltaf);
      basis = basis.Rotated(basis.Z, pitchInput * PitchSpeed * deltaf);

      transform.Basis = basis;
      PlayerBody.Transform = transform;
    }
  }

  // Public Functions

  // Private Functions
  private void GetInput()
  {
    emergencyBrake = Input.IsActionPressed("emergency_brake");

    forwardInput = Input.GetActionStrength("slide_forward") - Input.GetActionStrength("slide_backward");
    lateralInput = Input.GetActionStrength("slide_right") - Input.GetActionStrength("slide_left");
    verticalInput = Input.GetActionStrength("slide_up") - Input.GetActionStrength("slide_down");

    rollInput = Input.GetActionStrength("roll_right") - Input.GetActionStrength("roll_left");
    pitchInput = Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down");
    yawInput = Input.GetActionStrength("yaw_right") - Input.GetActionStrength("yaw_left");
  }
}
