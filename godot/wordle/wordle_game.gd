class_name WordleGame extends RefCounted

var word_length: int
var max_guess_count: int

var do_check_duplicates: bool
var do_check_dictionary: bool

var dictionary: Dictionary

# Working variables
var game_word: String
var guessed_words: Array[String] = []

var is_game_over: bool = false
var is_player_won := false


func _init(length: int, count: int, check_duplicates: bool, check_dictionary: bool, dict5: Dictionary) -> void:
	word_length = length
	max_guess_count = count
	do_check_dictionary = check_dictionary
	do_check_duplicates = check_duplicates
	dictionary = dict5


func start(word: String) -> void:
	game_word = word
	guessed_words.clear()
	is_game_over = false
	is_player_won = false


func submit_word(word: String) -> Match:
	var mat := Match.new(word_length)

	if word.length() < word_length:
		mat.error = Globals.MatchError.INVALID_WORD
		return mat
	elif do_check_dictionary and not dictionary.has(word):
		mat.error = Globals.MatchError.NOT_IN_DICTIONARY
	elif do_check_duplicates and guessed_words.has(word):
		mat.error = Globals.MatchError.DUPLICATE_WORD

	var word_found := true
	for i in word_length:
		if word[i] == game_word[i]:
			mat.result[i] = Globals.MatchType.SUCCESS
		else:
			word_found = false
			var partial_found := false
			for j in word_length:
				if word[i] == game_word[j]:
					mat.result[i] = Globals.MatchType.PARTIAL
					partial_found = true

			if not partial_found:
				mat.result[i] = Globals.MatchType.FAILED

	if mat.error == Globals.MatchError.NONE:
		guessed_words.push_back(word)

	if word_found or guessed_words.size() == max_guess_count:
		is_game_over = true
		is_player_won = word_found

	return mat
