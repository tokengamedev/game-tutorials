class_name Match extends RefCounted

var error: Globals.MatchError
var result: Array[Globals.MatchType] = []


func _init(word_length: int) -> void:
	error = Globals.MatchError.NONE
	result.resize(word_length)


func _to_string() -> String:
	var out := str(Globals.MatchError.find_key(error)) + ": ["
	for type in result:
		out += str(Globals.MatchType.find_key(type)) + ", "
	out += "]"
	return out
