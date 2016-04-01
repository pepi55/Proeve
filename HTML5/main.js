// Initialize Phaser
var game = new Phaser.Game(800, 600, Phaser.AUTO, 'phaser-example', { preload: preload, create: create, update: update });

// Main game state.
function preload() {
	game.load.image('character', 'assets/character.jpg');
}

function create() {
	game.stage.backgroundColor = '#CCCCCC';
	game.physics.startSystem(Phaser.Physics.ARCADE);

	this.character = game.add.sprite(100, 100, 'character');
}
