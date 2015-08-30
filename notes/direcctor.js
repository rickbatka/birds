gamestate (static){
	base, state: 
	mainmenu:
		listgames (default)
		newgame-form
		newgame-find-opponent
		newgame-add-friend
	loadscene:
		loading
	battle:
		showingreplay
		notmyturn-battleground (pan & zoom, no actions)
		notmyturn-cards (look at cards, no action)
		myturn-cards (animate newly received cards first time they see them)
		myturn-battleground
	
	no pause needed!
}

director (singleton): controls flow between state, loads and transitions scenes, manages game state
	idea: stateToState transitioner objects?
	idea: cards can add methods to the director as extension methods (prefix with card- or something to help with clutter)
	

main menu -> new game
	choose opponent
	dice rool for p1 / p2 (who goes first) (maybe host sould get to go first)
	loadscene
	gamestart
	goto taketurn / notmyturn
	
man menu -> continue game (or respond to push message)
	loadscene
	gameresume
	turn / notmyturn

loadscene ->
	show loading screen...
	previous turns exist? download most recent turn
	init director w/ current turn, prev turn info
	goto taketurn / notmyturn

notmyturn ->
	previous turns exist? start showing replay of my last turn
		skip button available
	after replay: view battleground, icon above opponent: waiting for opponent...
	allow zoom / pan
	allow look at cards
	
myturn ->
	previous turns exist? start showing replay of opponent's last turn
		skip button available
	after replay: show new cards drawn w/ animation (possible to draw diff number of cards, repeatable animation)
	looking at cards now
		ability to hide card drawer and be in "justlooking-myturn" mode
		use cards, cards might take you into "justlooking-myturn" to place objects, etc
		finish using cards (continue button)
	battle-myturn mode
		take your shots
	after all shots taken:
		recap drawer drops in: cards used, points used, damage done. "waiting for opponent now - ping them?"
	
scenes:
	sandbox
	mainmenu
	battleground