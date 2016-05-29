// Loads some things for the loading screen.
var bootState = {
	preload: function() {
		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;

		game.load.json('characters', 'json/characters.json');
		game.load.spritesheet('loadingScreenSheet', 'assets/UI/sheetScreenswitch.png', 1005, 900);
	},

	create: function() {
		game.state.start('load');
	}
};