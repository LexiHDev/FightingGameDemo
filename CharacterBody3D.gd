# Get the gravity from the project settings to be synced with RigidBody nodes.
extends CharacterBody3D

@export var SPEED: float = 5.0
@export var JUMP_VELOCITY: float = 6.0

@export var mouse_mode: Input.MouseMode = Input.MOUSE_MODE_CAPTURED
@export var rotation_speed: float = PI / 2.2
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")


func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y -= gravity * delta

	# Handle Jump.
	if Input.is_action_just_pressed("jump_e") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	var input_dir = Input.get_vector("left_e", "right_e", "up_e", "down_e")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)
	move_and_slide()
