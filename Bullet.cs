using Godot;
using System;

namespace GodotSpace2;

/// <summary>
/// template
/// </summary>
public partial class Bullet : Area3D
{
  // Signals

  // Exports
  [Export]
  public float Speed = 20.0f;

  // Public Fields

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(double delta)
  {
    Position += Transform.Basis.X * Speed * (float)delta;
  }

  public void OnBulletEntered(Node3D body)
  {
    GD.Print("bullet hits!" + body);
    QueueFree();
  }

  // Public Functions

  // Private Functions
}
