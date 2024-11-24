class_name WordTile extends TextureRect

const MatchType = Globals.MatchType

const TILE_TEXTURES := {
	MatchType.NONE: preload("res://assets/images/empty_tile.png"),
	MatchType.FAILED: preload("res://assets/images/failed_tile.png"),
	MatchType.PARTIAL: preload("res://assets/images/partial_tile.png"),
	MatchType.SUCCESS: preload("res://assets/images/success_tile.png")
}

@onready var label: Label = $Label

func _ready() -> void:
	clear_text()


func get_text() -> String:
	return label.text


func set_text(text: String) -> void:
	label.text = text


func clear_text() -> void:
	label.text = ""

func mark(type: MatchType):
	texture = TILE_TEXTURES[type]
