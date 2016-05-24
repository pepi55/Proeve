// Main menu state.
	/** This is a description of the update. */
var mainMenuState = {
	currentHighscore: new Array(),
	preload: function() {
		// TODO: Add screen resize.
	  game.load.image('playButton', 'assets/buttons/playButton.png');
	  game.load.image('highscoreButton', 'assets/buttons/highscoreButton.png');
	  game.load.image('shopButton', 'assets/buttons/shopButton.png');
	  game.load.image('backButton', 'assets/buttons/exitButton.png');
		game.load.image('invisibleButton', 'assets/invisibleButton.png');

		game.load.image('muteButton', 'assets/buttons/muteButton.png');
		game.load.image('soundButton', 'assets/buttons/soundButton.png');
		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
	},

	create: function() {
		game.stage.backgroundColor = '#BBBBBB';

		/**
		* Represents a book.
		* @param {string} startButton - this is a button to start the game.
		*/
		var startButton;
		startButton = game.add.button(game.world.centerX, game.world.centerY - 200, 'playButton', function() {
		    game.state.start('game');
		}, this);
		startButton.anchor.setTo(0.5, 0.5);

		/** shop button to start the game. */
		var shopButton;
		shopButton = game.add.button(game.world.centerX, game.world.centerY + 50, 'shopButton', function() {
			game.state.start('shop');
		}, this);
		shopButton.anchor.setTo(0.5, 0.5);

		var highscoreButton;
		highscoreButton = game.add.button(game.world.centerX, game.world.centerY + 310, 'highscoreButton', function() {
			game.state.start('highscore');
		}, this);
		highscoreButton.anchor.setTo(0.5, 0.5);

		var soundButton;
		soundButton = game.add.button(400, 1400, 'soundButton', function() {
			game.sound.mute = false
		}, this);
		soundButton.anchor.setTo(0.5, 0.5);
		soundButton.scale.setTo(0.75,0.75);

		var muteButton;
		muteButton = game.add.button(200, 1400, 'muteButton', function() {
			game.sound.mute = true;
		}, this);
		muteButton.anchor.setTo(0.5, 0.5);
				muteButton.scale.setTo(0.75,0.75);


		var resetLocalStorageButton = game.add.button(game.world.centerX, game.world.centerY + 500, 'button', function() {
			localStorage.clear();
		}, this);
		resetLocalStorageButton.anchor.setTo(0.5, 0.5);

		var modeButton;
		modeButton = game.add.button(20, 20, 'invisibleButton', function() {
		    hardMode = !hardMode;
		}, this);
		modeButton.anchor.setTo(0.5, 0.5);
	},

	/** @method
	* @name update
	* @description this is a update function
	*/
	update: function() {
	},
};