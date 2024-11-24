class_name KeyBoard extends MarginContainer

signal key_pressed(key: String)
signal submit_pressed
signal clear_pressed


const STANDARD_KEYBOARD := "QWERTYUIOPASDFGHJKLZXCVBNM"
const ALPHABET_KEYBOARD := "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

@onready var key_rows: VBoxContainer = $KeyRows

var input_enabled := true

func setup() -> void:
	var current = STANDARD_KEYBOARD if Globals.use_standard_keyboard else ALPHABET_KEYBOARD
	var i := 0
	for child in key_rows.get_children():
		for key in child.get_children():
			if key is Key:
				key.set_text(current[i])
				key.key_pressed.connect(_on_key_pressed)
				i += 1


func _input(event: InputEvent) -> void:
	if not input_enabled:
		return

	if event is InputEventKey and event.is_pressed():
		if event.keycode == KEY_ENTER:
			_on_submit_pressed()
		elif event.keycode == KEY_BACKSPACE or event.keycode == KEY_DELETE:
			_on_clear_pressed()
		elif event.keycode >= KEY_A and event.keycode <= KEY_Z:
			_on_key_pressed(OS.get_keycode_string(event.keycode))


func enable_input(enable: bool) -> void:
	input_enabled = enable

	for child in key_rows.get_children():
		for key in child.get_children():
			key.disabled = not enable


func _on_key_pressed(key_string: String) -> void:
	key_pressed.emit(key_string.to_upper())


func _on_submit_pressed() -> void:
	submit_pressed.emit()


func _on_clear_pressed() -> void:
	clear_pressed.emit()
