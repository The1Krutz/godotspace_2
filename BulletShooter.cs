using Godot;
using System;

namespace GodotSpace2;

public enum WeaponGrouping
{
  Primary,
  Secondary
}

/// <summary>
/// template
/// </summary>
public partial class BulletShooter : Node3D
{
  // Signals

  // Exports
  [Export]
  public WeaponGrouping WeaponGroup = WeaponGrouping.Primary;

  // Public Fields

  // Backing Fields

  // Private Fields
  private bool isShooting;

  // Constructor

  // Lifecycle Hooks

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    GetInput();

    if (isShooting && WeaponGroup == WeaponGrouping.Primary)
    {
      Shoot();
    }
  }

  // Public Functions

  // Private Functions
  private void GetInput()
  {
    if (Input.IsActionJustPressed("fire_primary"))
    {
      StartShooting();
    }
    if (Input.IsActionJustReleased("fire_primary"))
    {
      StopShooting();
    }

    if (isShooting)
    {
      Shoot();
    }
  }

  private void Shoot()
  {
    GD.Print("bang");
    // @todo - recycle the lmg script from your horde shooter
  }

  private void StartShooting()
  {
    isShooting = true;
    GD.Print("so anyway i started blasting");
  }

  private void StopShooting()
  {
    isShooting = false;
    GD.Print("done shooting");
  }
}
