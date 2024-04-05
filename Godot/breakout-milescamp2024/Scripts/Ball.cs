using Godot;
using System;

public partial class Ball : CharacterBody3D
{
    private readonly float _Speed = 7.07f;
    private Vector3 _Velocity = new Vector3(0, -7.07f, 0);
    private bool _HasStarted = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!_HasStarted && Input.IsActionPressed("ui_select"))
        {
            _HasStarted = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        if (_HasStarted)
        {
            velocity = _Velocity;
        }

        Velocity = velocity;
        var collision = MoveAndCollide(velocity * (float)delta);
        if (collision != null)
        {
            var collider = collision.GetCollider();
            Console.WriteLine(collision.GetNormal());

            if (collider is CharacterBody3D)
            {
                var paddle = collider as CharacterBody3D;
                var newDirection = paddle.GlobalPosition.DirectionTo(this.GlobalPosition);

                _Velocity = new Vector3(newDirection.X, newDirection.Y, 0).Normalized() * _Speed;

                //var colPoint = collision.GetPosition();
                //var collisionShape = paddle.GetChild<CollisionShape3D>(0);
                //var mesh = collisionShape.GetChild<MeshInstance3D>(0);
                //var aabb = mesh.GetAabb();
                //var oldMax = paddle.GlobalPosition.X - aabb.Position.X;
                //var oldMin = paddle.GlobalPosition.X + aabb.End.X;
                //Console.WriteLine("paddle pos: " + paddle.GlobalPosition.X);
                //Console.WriteLine("oldMax: " + oldMax);
                //Console.WriteLine("oldMin: " + oldMin);

                //var oldPoint = colPoint.X;
                //Console.WriteLine("oldPoint: " + oldPoint);
                //var length = aabb.Size.X;


                //var oldRange = oldMax - oldMin;
                //Console.WriteLine("oldRange: " + oldRange);
                //var newMax = 170;
                //var newMin = 10;
                //var newRange = newMax - newMin;
                //Console.WriteLine("newRange: " + newRange);

                //float angle;
                //if (oldRange == 0) angle = newMin;
                //else angle = (((oldPoint - oldMin) * newRange) / oldRange) + newMin;
                //Console.WriteLine("angle: " + angle);

                //var angleVector = Vector2.Right.Rotated(angle * MathF.PI / 180.0f);
                //Console.WriteLine("angleVector: " + angleVector);
                //_Velocity = new Vector3(angleVector.X, angleVector.Y, 0).Normalized() * _Speed;

                //Console.WriteLine(colPoint);
                Console.WriteLine("HIT PADDLE: " + paddle.Name);
            }
            else if (collider is StaticBody3D)
            {
                var staticBody = collider as StaticBody3D;
                Console.WriteLine("HIT WALL:" + staticBody.Name);
                _Velocity = _Velocity.Bounce(collision.GetNormal());

                if (staticBody.Name.ToString().StartsWith("Block"))
                {
                    Console.WriteLine("HIT BLOCK: " + staticBody.Name);
                    staticBody.Visible = false;
                    staticBody.SetDeferred("disabled", true);
                    staticBody.DisableMode = DisableModeEnum.Remove;
                    staticBody.ProcessMode = ProcessModeEnum.Disabled;
                    MainScene.IncreaseCounter();
                }
            }
            else
            {
                Console.WriteLine(collider.GetType().Name);
            }
        }
    }
}
