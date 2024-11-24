extends Control
const DICT_PATH := "res://data/dict5.txt"
const GAME_WORDS_PATH := "res://data/game_words.txt"

@onready var keyboard: MarginContainer = $Keyboard
@onready var game_manager: GameManager = $GameManager
@onready var player_words: PlayerWords = $PlayerWords
@onready var message: Label = $Message

@onready var game_over_message: Label = %Message
@onready var try_again_button: Button = %TryAgain
@onready var game_over_dialog: Control = $GameOverDialog

var game: WordleGame
var dictionary: Dictionary = {}
var game_words: PackedStringArray = []
var game_word: String

func _ready() -> void:

	load_dictionary()
	keyboard.setup()
	player_words.setup(keyboard, message)
	player_words.word_submitted.connect(_on_word_submitted)
	game = WordleGame.new(	Globals.WORD_LENGTH,
							Globals.MAX_GUESS_COUNT,
							Globals.allow_duplicates,
							Globals.do_dictionary_check,
							dictionary)
	game_over_dialog.hide()
	start()


func start():
	game_word = get_random_word()
	print("Game Word: ", game_word)
	game.start(game_word)



func _on_word_submitted(word: String):
	var mat = game.submit_word(word)

	player_words.handle_word_submission(mat)


	if game.is_game_over:
		player_words.handle_game_over()

		# Handle game over dialog
		if game.is_player_won:
			try_again_button.visible = false
			game_over_message.text = "You found the Word"
		else:
			try_again_button.visible = true
			game_over_message.text = "You could not find the Word"
		game_over_dialog.show()


func load_dictionary() -> void:
	if dictionary.is_empty():
		var file = FileAccess.open(DICT_PATH, FileAccess.READ)
		var word = file.get_line()
		while (not word.is_empty()):
			dictionary[word] = 1
			word = file.get_line()


func get_random_word() -> String:
	if game_words.is_empty():
		var file = FileAccess.open(GAME_WORDS_PATH, FileAccess.READ)
		var word = file.get_line()
		while (not word.is_empty()):
			game_words.push_back(word)
			word = file.get_line()
	if game_words.size() > 0:
		var index = randi_range(0, game_words.size())
		return game_words[index]
	else:
		return "ERROR"


func _on_new_word_pressed() -> void:
	game_over_dialog.hide()
	start()
	player_words.start()


func _on_try_again_pressed() -> void:
	game_over_dialog.hide()
	game.start(game_word)
	player_words.start()
