using Godot;
using System;

public partial class FighterTemplate : CharacterBody3D
{
    // [ FIELDS ]
    [Export]
    private float Speed = 5.0f;
    [Export]
    private float JumpVelocity = 12.0f;
    [Export(PropertyHint.Enum, "MOUSE_MODE_VISIBLE,MOUSE_MODE_HIDDEN, MOUSE_MODE_CAPTURED, MOUSE_MODE_CONFINED, MOUSE_MODE_CONFINED_HIDDEN")]
    private Input.MouseModeEnum CharMouseMode = Input.MouseModeEnum.Captured;
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    // Get the gimbals.
    public Node3D innerGimbal = GetNode<Node3D>("InnerGimbal");
    public Node3D outerGimbal = GetNode<Node3D>("CameraGimbal");


    // [ METHODS ]
    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump_e") && IsOnFloor())
            velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("left_e", "right_e", "up_e", "down_e");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
