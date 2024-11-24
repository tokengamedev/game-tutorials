extends Node

enum MatchError { NONE, INVALID_WORD, NOT_IN_DICTIONARY, DUPLICATE_WORD }
enum MatchType { NONE, FAILED, PARTIAL, SUCCESS }


const WORD_LENGTH := 5
const MAX_GUESS_COUNT := 6

## If true Virtual keyboard will be standard key structure(QWERTY) or else Alphabetic (ABCDEF)
var use_standard_keyboard: bool = false

## If true game will allow user to enter duplicate words else error out
var allow_duplicates: bool = true

## If true game will allow valid words in dicxtionary or else ignore entry
var do_dictionary_check: bool = false
