// Main menu state.
var mainMenuState = {
	preload: function() {
		game.load.image('button', 'assets/image.png');
	},

	create: function() {
		game.stage.backgroundColor = '#CCCCCC';

		game.add.button(game.world.centerX, game.world.centerY, 'button', startGame, this);

		function startGame() {
			game.state.start('game');
		}
	},

	update: function() {
	},
};