using Godot;
using System;

namespace GodotSpace2;

/// <summary>
/// template
/// </summary>
public partial class BulletShooter : Node3D
{
  // Signals

  // Exports

  // Public Fields

  // Backing Fields

  // Private Fields
  private bool isShooting;
  private Vector3 AimDirection;
  private PackedScene BulletScene;
  private Basis BulletSpawnPoint;
  private string debugPath;

  // Constructor

  // Lifecycle Hooks

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    debugPath = this.GetPath();
    BulletScene = ResourceLoader.Load<PackedScene>("res://Bullet.tscn");
    BulletSpawnPoint = GetNode<Node3D>("SpawnPoint").Transform.Basis;

    GD.Print(BulletSpawnPoint);
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  // Public Functions
  public void Shoot()
  {
    GD.Print(debugPath + " bang!");
  }

  public void SpawnBullet()
  {
    Bullet bullet = BulletScene.Instantiate<Bullet>();
    AddChild(bullet);
    bullet.Transform = new Transform3D(BulletSpawnPoint, Position);
  }

  // Private Functions
  private void AimAt(Vector3 target)
  {
    AimDirection = target;
  }
}
