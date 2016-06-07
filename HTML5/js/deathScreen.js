/**
 * @Class
 * @name deathState
 * @desc state that starts when the player loses in the game state.
 * @property {number}			deathScoreValue			 -	current score that is shown in the death screen.
 * @property {number}			deathHighestScoreValue		 -	highest score the player had that is shown in the death screen.
 * @property {sound}			deathScreenBGMusic		 -	sound that is played on lose.
 * @property {fontStyle}		style				 -	style of the font in the screen.
 */
var deathState = {
  deathScoreValue: 0,
  deathHighestScoreValue: 0,
  deathScreenBGMusic: null,
  preload: function(){
  	this.deathScreenBGMusic = game.add.audio('gameOverSound');
  	this.deathScreenBGMusic.volume = 0.2;
  },
  /** @method
   * @name create
   * @memberof deathState
   * @description this is a create function that is fired after the preload function, this is where we create variables and images
   */
  create: function() {
  	this.deathScreenBGMusic.play();
  	this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, setName);
  	this.screenSprite = game.add.sprite(game.world.centerX , game.world.centerY, 'deathscreenBackground');
  	this.screenSprite.anchor.setTo(0.5, 0.5);
  	this.screenSprite.scale.setTo(1.5,1.5);
		var backButton;
		backButton = game.add.button(100, 100, 'backButton', function() {
			this.deathScreenBGMusic.stop();
		game.state.start('mainMenu');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);

		var replayButton;
		replayButton = game.add.button(game.world.centerX - 90, game.world.centerY + 90, 'playButton', function() {
			this.deathScreenBGMusic.stop();
		game.state.start('game');
		}, this);
		replayButton.scale.setTo(0.75,0.75);


		style = {};
		// Font style
		style.font = 'Passion One';
		style.fontSize = 75;
		style.fontWeight = 'bold';

		// Stroke color and thickness
		style.stroke = '#FFFF00';
		style.strokeThickness = 3;
		style.fill = '#FF2828';


		var	highscoreText = game.add.text(game.world.centerX, game.world.centerY - 100, "HIGHEST = " + deathHighestScoreValue,style);
		var	scoreText = game.add.text(game.world.centerX, game.world.centerY, "SCORE   = "  + deathScoreValue,style);
		var	loseText = game.add.text(game.world.centerX, game.world.centerY - 200, "REPLAY?",style);
		highscoreText.anchor.setTo(0.5);
		scoreText.anchor.setTo(0.5);
		loseText.anchor.setTo(0.5);


  },
  setScreenValues: function(score,highestscore) {
	deathScoreValue = score;
	deathHighestScoreValue = highestscore;
	if(highestscore == null)
	{
		deathHighestScoreValue = 0;
	}
	else
	{
		deathHighestScoreValue = deathHighestScoreValue;
	}
  }
};
