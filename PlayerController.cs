using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
  private bool isShooting;
  private readonly List<BulletShooter> primaryWeaponsList = new();

  // Constructor

  // Lifecycle Hooks
  public override void _Ready()
  {
    PlayerBody.GetNode<Camera3D>("Camera").Current = true;

    // populate the list of all primary weapons on this ship
    foreach (Node gunMount in GetParent().GetNode<Node3D>("PrimaryMounts").GetChildren())
    {
      var shooter = gunMount.GetChildOrNull<BulletShooter>(0);

      if (shooter is not null)
      {
        primaryWeaponsList.Add(shooter);
      }
    }

    GD.Print(primaryWeaponsList + " " + primaryWeaponsList.Count);
    GD.Print("Ready");
  }

  public override void _Process(double delta)
  {
    float deltaf = (float)delta;

    GetMovementInput();
    HandleMovement(deltaf);

    GetShootingInput();
    HandleShooting(deltaf);
  }

  // Public Functions

  // Private Functions
  private void GetMovementInput()
  {
    emergencyBrake = Input.IsActionPressed("emergency_brake");

    forwardInput = Input.GetActionStrength("slide_forward") - Input.GetActionStrength("slide_backward");
    lateralInput = Input.GetActionStrength("slide_right") - Input.GetActionStrength("slide_left");
    verticalInput = Input.GetActionStrength("slide_up") - Input.GetActionStrength("slide_down");

    rollInput = Input.GetActionStrength("roll_right") - Input.GetActionStrength("roll_left");
    pitchInput = Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down");
    yawInput = Input.GetActionStrength("yaw_right") - Input.GetActionStrength("yaw_left");
  }

  private void GetShootingInput()
  {
    if (Input.IsActionJustPressed("fire_primary"))
    {
      // isShooting = true;
      // GD.Print("so anyway i started blasting");
      Shoot();
    }
    // if (Input.IsActionJustReleased("fire_primary"))
    // {
    //   isShooting = false;
    //   GD.Print("done shooting");
    // }
  }

  private void HandleMovement(float delta)
  {
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

      PlayerBody.MoveAndCollide(movementDir * delta);

      Transform3D transform = PlayerBody.Transform;
      Basis basis = PlayerBody.Transform.Basis;

      basis = basis.Rotated(basis.X, rollInput * RollSpeed * delta);
      basis = basis.Rotated(basis.Y, -yawInput * YawSpeed * delta);
      basis = basis.Rotated(basis.Z, pitchInput * PitchSpeed * delta);

      transform.Basis = basis;
      PlayerBody.Transform = transform;
    }
  }

  private void HandleShooting(float delta)
  {
    // @todo - also do shooting timer stuff here
    if (isShooting)
    {
      Shoot();
    }
  }

  private void Shoot()
  {
    primaryWeaponsList.ForEach(shooter => shooter.Shoot());
  }
}
