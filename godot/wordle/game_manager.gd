class_name GameManager extends Node

## If true Virtual keyboard will be standard key structure(QWERTY) or else Alphabetic (ABCDEF)
@export var use_standard_keyboard: bool = false

## If true game will allow user to enter duplicate words else error out
@export var allow_duplicates: bool = true

## If true game will allow valid words in dicxtionary or else ignore entry
@export var do_dictionary_check: bool = false


func _ready() -> void:
	Globals.allow_duplicates = allow_duplicates
	Globals.do_dictionary_check = do_dictionary_check
	Globals.use_standard_keyboard = use_standard_keyboard
