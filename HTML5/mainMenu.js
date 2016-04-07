// Main menu state.
var mainMenuState = {
	preload: function() {
		// TODO: Add screen resize.
		game.load.image('button', 'assets/image.png');
		game.load.image('invisibleButton', 'assets/invisibleButton.png');
	},

	create: function() {
		game.stage.backgroundColor = '#BBBBBB';

		var button;
		button = game.add.button(game.world.centerX, game.world.centerY, 'button', startGame, this);
		button.anchor.setTo(0.5, 0.5);

		var modeButton;
		modeButton = game.add.button(20, 20, 'invisibleButton', ChangeMode, this);
		modeButton.anchor.setTo(0.5, 0.5);


		function ChangeMode() {
			hardMode = !hardMode;
		}

		function startGame() {
			game.state.start('game');
		}
	},

	update: function() {
	},
};