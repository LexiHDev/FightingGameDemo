using Godot;
using System;

public partial class FighterTemplate : CharacterBody3D
{
    // [ FIELDS ]
    [Export]
    private float Speed = 5.0f;
    [Export]
    private float JumpVelocity = 12.0f;
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    //  Gimbals.
    public Node3D innerGimbal;
    public Node3D outerGimbal;
    private Global global;



    // [ OVERRIDES ]
    public override void _Ready()
    {
        innerGimbal = GetNodeOrNull<Node3D>("CameraGimble/InnerGimble");
        outerGimbal = GetNodeOrNull<Node3D>("CameraGimble");
        global = GetNodeOrNull<Global>("/root/Global");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("menu_e"))
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else if (@event.IsActionPressed("click_e"))
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            GetViewport().SetInputAsHandled();
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump_e") && IsOnFloor())
            velocity.Y = JumpVelocity;

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
