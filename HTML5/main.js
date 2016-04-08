// Create game with phaser.
var game = new Phaser.Game(1024, 1366,Phaser.CANVAS);

// Add game states.
game.state.add('mainMenu', this.mainMenuState);
game.state.add('game', this.gameState);

game.state.start('mainMenu');