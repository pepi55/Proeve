// Create game with phaser.
var game = new Phaser.Game(2048, 1536, Phaser.CANVAS);

// Add game states.
game.state.add('boot', this.bootState);
game.state.add('load', this.loadState);
game.state.add('mainMenu', this.mainMenuState);
game.state.add('shop', this.shopState);
game.state.add('options', this.optionsState);
game.state.add('highscore', this.highscoreState);
game.state.add('deathScreen', this.deathState);
game.state.add('game', this.gameState);

game.state.start('boot');

//TODO: moeilijkheid,feedback particles,backgrounds,versie maken optimized,music laten loopen tussen games,