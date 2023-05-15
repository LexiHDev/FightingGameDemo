extends Node

var _fov: float = 87.6
var _horizontal_sensitivity: float = 0.5
var _vertical_sensitivity: float = 0.5

class Controls:
	# https://docs.godotengine.org/en/stable/classes/class_inputeventkey.html#class-inputeventkey-property-physical-keycode
	var forward_key: InputEventKey
	var back_key: InputEventKey
	var left_key: InputEventKey
	var right_key: InputEventKey
	var jump_key: InputEventKey
	var rush_key: InputEventKey
	var feint_key: InputEventKey
	var volley_key: InputEventKey



# [SETTERS/GETTERS]
func set_fov(fov: float):
	_fov = fov

func _get_fov() -> float:
	return _fov

func set_horizontal_sensitivity(horizontal_sensitivity):
	_horizontal_sensitivity = horizontal_sensitivity

func get_horizontal_sensitivity() -> float:
	return _horizontal_sensitivity

func set_vertical_sensitivity(vertical_sensitivity):
	_vertical_sensitivity = vertical_sensitivity
	
func get_vertical_sensitivity() -> float:
	return _vertical_sensitivity