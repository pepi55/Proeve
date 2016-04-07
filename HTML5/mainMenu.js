// Main menu state.
var mainMenuState = {
	preload: function() {
		// TODO: Add screen resize.
		game.load.image('button', 'assets/image.png');
	},

	create: function() {
		game.stage.backgroundColor = '#BBBBBB';

		var button;
		button = game.add.button(game.world.centerX, game.world.centerY, 'button', startGame, this);
		button.anchor.setTo(0.5, 0.5);

		function startGame() {
			game.state.start('game');
		}
	},

	update: function() {
	},
};