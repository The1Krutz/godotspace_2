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
  private string debugPath;

  // Constructor

  // Lifecycle Hooks

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    debugPath = this.GetPath();
    BulletScene = ResourceLoader.Load<PackedScene>("res://Bullet.tscn");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  // Public Functions
  public void Shoot()
  {
    SpawnBullet();
    GD.Print(debugPath + " bang!");
  }

  // Private Functions
  private void AimAt(Vector3 target)
  {
    AimDirection = target;
  }

  private void SpawnBullet()
  {
    Node3D bulletSpawnPoint = GetNode<Node3D>("SpawnPoint");
    Bullet bullet = BulletScene.Instantiate<Bullet>();

    GetNode("/root/Main").AddChild(bullet);
    bullet.Transform = bulletSpawnPoint.GlobalTransform;
    bullet.Rotation = bulletSpawnPoint.GlobalRotation;
  }
}
