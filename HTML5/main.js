// Create game with phaser.
var game = new Phaser.Game(800, 600);

// Add game states.
game.state.add('mainMenu', this.mainMenuState);
game.state.add('game', this.gameState);

game.state.start('mainMenu');