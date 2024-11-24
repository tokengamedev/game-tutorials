class_name Key extends TextureButton

signal key_pressed(text: String)

@onready var label: Label = $Label


func set_text(text: String) -> void:
	label.text = text


func clear_text() -> void:
	label.text = ""


func _on_key_pressed() -> void:
	key_pressed.emit(label.text)
