// Main menu state.
	/** This is a description of the update. */
var mainMenuState = {
	emitter: null,
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

		game.load.image('startBackground', 'assets/background/startscreen.png')
		game.load.image('logo', 'assets/UI/Vivamacho.png')
		game.load.image('pixel_yellow', 'assets/particles/pixel_yellow.png');
		game.load.image('pixel_blue', 'assets/particles/pixel_blue.png');
		game.load.image('pixel_pink', 'assets/particles/pixel_pink.png');

		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
	},

	create: function() {


		game.stage.backgroundColor = '#BBBBBB';
		var startBackground = game.add.sprite(0, 0,'startBackground');
		var logo = game.add.sprite(0, -200, 'logo');

		/**
		* Represents a book.
		* @param {string} startButton - this is a button to start the game.
		*/
		var startButton;
		startButton = game.add.button(game.world.centerX - 400, game.world.centerY - 200, 'playButton', function() {
		    game.state.start('game');
		}, this);
		startButton.anchor.setTo(0.5, 0.5);

		/** shop button to start the game. */
		var shopButton;
		shopButton = game.add.button(game.world.centerX - 400, game.world.centerY + 50, 'shopButton', function() {
			game.state.start('shop');
		}, this);
		shopButton.anchor.setTo(0.5, 0.5);

		var highscoreButton;
		highscoreButton = game.add.button(game.world.centerX - 400, game.world.centerY + 310, 'highscoreButton', function() {
			game.state.start('highscore');
		}, this);
		highscoreButton.anchor.setTo(0.5, 0.5);

		var soundButton;
		soundButton = game.add.button(1600, 1400, 'soundButton', function() {
			game.sound.mute = false
		}, this);
		soundButton.anchor.setTo(0.5, 0.5);
		soundButton.scale.setTo(0.75,0.75);

		var muteButton;
		muteButton = game.add.button(1800, 1400, 'muteButton', function() {
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

		emitter = game.add.emitter(0, 0, 100);

    emitter.makeParticles(['pixel_yellow','pixel_pink','pixel_blue']);
    emitter.gravity = 200;
    emitter.setScale(0.5, 0, 0.5, 0, 6000);
    game.input.onDown.add(this.particleBurst, this);

	},

	/** @method
	* @name update
	* @description this is a update function
	*/
	update: function() {
	},

	particleBurst: function() {
    //  Position the emitter where the mouse/touch event was
    emitter.x = game.input.activePointer.x;
    emitter.y = game.input.activePointer.y;

    //  The first parameter sets the effect to "explode" which means all particles are emitted at once
    //  The second gives each particle a 2000ms lifespan
    //  The third is ignored when using burst/explode mode
    //  The final parameter (10) is how many particles will be emitted in this single burst
    emitter.start(true, 2000, null, 10);
	},
};