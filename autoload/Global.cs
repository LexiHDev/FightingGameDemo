using Godot;
using System;

// Yoinked from https://docs.godotengine.org/en/stable/tutorials/scripting/singletons_autoload.html

public partial class Global : Node
{
    // [ FIELDS ]
    private Node CurrentScene { get; set; }

    private float InputSensitivity { get; set; } = 3 / 3.14f;


    // [ METHODS ]
    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        CurrentScene = root.GetChild(root.GetChildCount());
    }

    public void GotoScene(string path)
    {
        // This function will usually be called from a signal callback,
        // or some other function from the current scene.
        // Deleting the current scene at this point is
        // a bad idea, because it may still be executing code.
        // This will result in a crash or unexpected behavior.

        // The solution is to defer the load to a later time, when
        // we can be sure that no code from the current scene is running:

        CallDeferred(nameof(DeferredGotoScene), path);
    }

    public void DeferredGotoScene(string path)
    {
        // It is now safe to remove the current scene
        CurrentScene.Free();
        // Load a new scene.
        PackedScene nextScene = (PackedScene)GD.Load(path);
        // Instance the new scene.
        CurrentScene = nextScene.Instantiate();
        // Add it to the active scene, as child of root.
        GetTree().Root.AddChild(CurrentScene);
        // Optionally, to make it compatible with the SceneTree.change_scene_to_file() API.
        GetTree().CurrentScene = CurrentScene;
    }
}