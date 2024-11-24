class_name PlayerWords extends MarginContainer

signal word_submitted(word: String)

const ERROR_MESSAGES := {
	Globals.MatchError.NONE: "",
	Globals.MatchError.INVALID_WORD: "INVALID WORD",
	Globals.MatchError.NOT_IN_DICTIONARY: "NOT IN DICTIOONARY",
	Globals.MatchError.DUPLICATE_WORD: "DUPLICATE WORD",
}

const WORD_ROW = preload("res://player_words/word_row.tscn")
@onready var word_rows: VBoxContainer = $WordRows

var keyboard: KeyBoard

var current_row := 0
var current_col := 0
var message: Label

func _ready() -> void:
	create_word_layout()


func create_word_layout() -> void:
	for row in Globals.MAX_GUESS_COUNT:
		var word_row := WORD_ROW.instantiate()
		word_row.create(Globals.WORD_LENGTH)
		word_rows.add_child(word_row)


func setup(kb: KeyBoard, label: Label) -> void:
	keyboard = kb
	keyboard.clear_pressed.connect(_on_clear_pressed)
	keyboard.submit_pressed.connect(_on_submit_pressed)
	keyboard.key_pressed.connect(_on_key_pressed)
	message = label
	message.text = ""


func start() -> void:
	current_row = 0
	current_col = 0
	keyboard.enable_input(true)
	for child in word_rows.get_children():
		child.clear()


func _on_clear_pressed() -> void:
	if current_col > 0:
		current_col -= 1;
		word_rows.get_child(current_row).clear_letter(current_col)
		message.text = ""


func _on_submit_pressed() -> void:
	if current_col == Globals.WORD_LENGTH:
		if current_row >=0 and current_row < Globals.MAX_GUESS_COUNT:
			word_submitted.emit(word_rows.get_child(current_row).get_word())


func _on_key_pressed(key: String) -> void:
	if current_col >= 0 and current_col < Globals.WORD_LENGTH:
		word_rows.get_child(current_row).add_letter(key, current_col)
		current_col += 1


func handle_word_submission(_match: Match) -> void:
	message.text = ERROR_MESSAGES[_match.error]
	if _match.error != Globals.MatchError.NONE:
		return

	word_rows.get_child(current_row).highlight(_match.result)
	current_col = 0
	current_row += 1

func handle_game_over():
	keyboard.enable_input(false)
