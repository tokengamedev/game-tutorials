class_name WordRow extends HBoxContainer

const WORD_TILE = preload("res://player_words/word_tile.tscn")

func create(word_length: int) -> void:
	for i in word_length:
		var tile :WordTile = WORD_TILE.instantiate()
		add_child(tile)


func clear_letter(index: int) -> void:
	get_child(index).clear_text()


func add_letter(letter: String, index: int) -> void:
	get_child(index).set_text(letter)


func get_word() -> String:
	var out = ""
	for child in get_children():
		out += child.get_text()
	return out


func clear() -> void:
	for child in get_children():
		child.clear_text()
		child.mark(Globals.MatchType.NONE)


func highlight(result: Array[Globals.MatchType] ) -> void:
	for i in get_child_count():
		get_child(i).mark(result[i])
