// Main menu state.
	/** This is a description of the update. */
var mainMenuState = {
	currentHighscore: new Array(),
	preload: function() {
		// TODO: Add screen resize.
	  game.load.image('playButton', 'assets/buttons/play.png');
	  game.load.image('highscoreButton', 'assets/buttons/highscore.png');
	  game.load.image('shopButton', 'assets/buttons/shop.png');
	  game.load.image('backButton', 'assets/buttons/back.png');
		game.load.image('invisibleButton', 'assets/invisibleButton.png');


		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
	},

	create: function() {
		game.stage.backgroundColor = '#BBBBBB';

		/**
		* Represents a book.
		* @param {string} startButton - this is a button to start the game.
		*/
		var startButton;
		startButton = game.add.button(game.world.centerX, game.world.centerY, 'playButton', function() {
		    game.state.start('game');
		}, this);
		startButton.anchor.setTo(0.5, 0.5);

		/** shop button to start the game. */
		var shopButton;
		shopButton = game.add.button(game.world.centerX, game.world.centerY + 150, 'shopButton', function() {
			game.state.start('shop');
		}, this);
		shopButton.anchor.setTo(0.5, 0.5);

		var highscoreButton;
		highscoreButton = game.add.button(game.world.centerX, game.world.centerY + 300, 'highscoreButton', function() {
			game.state.start('highscore');
		}, this);
		highscoreButton.anchor.setTo(0.5, 0.5);

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