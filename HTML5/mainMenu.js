// Main menu state.
var mainMenuState = {
	preload: function() {
		// TODO: Add screen resize.
	  var startButtonImage = game.load.image('button', 'assets/image.png');
		game.load.image('invisibleButton', 'assets/invisibleButton.png');

		startButtonImage = startButtonImage.crossOrigin = "Anonymous";
		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
	},

	create: function() {
		game.stage.backgroundColor = '#BBBBBB';

		var startButton;
		startButton = game.add.button(game.world.centerX, game.world.centerY, 'button', function() {
		    game.state.start('game');
		}, this);
		startButton.anchor.setTo(0.5, 0.5);

		var shopButton;
		shopButton = game.add.button(game.world.centerX, game.world.centerY + 50, 'button', function() {
			game.state.start('shop');
		}, this);
		shopButton.anchor.setTo(0.5, 0.5);

		var optionsButton;
		optionsButton = game.add.button(game.world.centerX, game.world.centerY + 150, 'button', function() {
			game.state.start('options');
		}, this);
		optionsButton.anchor.setTo(0.5, 0.5);

		var modeButton;
		modeButton = game.add.button(20, 20, 'invisibleButton', function() {
		    hardMode = !hardMode;
		}, this);
		modeButton.anchor.setTo(0.5, 0.5);
	},

	update: function() {
	},
};