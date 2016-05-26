// death state.


var deathState = {
  deathScoreValue: 0,
  deathHighestScoreValue: 0,
  preload: function(){
  			game.load.image('deathscreenBackground', 'assets/UI/deathScreen.png');
  },
  create: function() {


  	this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, 'background');
  	this.screenSprite = game.add.sprite(game.world.centerX , game.world.centerY, 'deathscreenBackground');
  	this.screenSprite.anchor.setTo(0.5, 0.5);
  	this.screenSprite.scale.setTo(1.5,1.5);
		var backButton;
		backButton = game.add.button(100, 100, 'backButton', function() {
		game.state.start('mainMenu');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);

		var replayButton;
		replayButton = game.add.button(game.world.centerX - 90, game.world.centerY + 90, 'playButton', function() {
		game.state.start('game');
		}, this);
		//replayButton.anchor.setTo(0.5, 0.5);
		replayButton.scale.setTo(0.75,0.75);


		style = {};
		 //	Font style
		style.font = 'Passion One';
		style.fontSize = 75;
		style.fontWeight = 'bold';

		//	Stroke color and thickness
		//style.stroke = '#0C090A';
		//style.strokeThickness = 7;
		//style.fill = '#52D017';

					//	Stroke color and thickness
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
