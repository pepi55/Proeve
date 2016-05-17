// death state.


var deathState = {
  deathScoreValue: 0,
  deathHighestScoreValue: 0,
  preload: function(){
  			game.load.image('deathscreenBackground', 'assets/deathScreen.png');
  },
  create: function() {


  		this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, 'background');
  			this.screenSprite = game.add.sprite(game.world.centerX , game.world.centerY, 'deathscreenBackground');
  				this.screenSprite.anchor.setTo(0.35, 0.6);
			var backButton;
		backButton = game.add.button(game.world.centerX, game.world.centerY + 60, 'backButton', function() {
		game.state.start('mainMenu');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);

			var replayButton;
		replayButton = game.add.button(game.world.centerX + 200, game.world.centerY, 'playButton', function() {
		game.state.start('game');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);


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


		var	highscoreText = game.add.text(game.world.centerX - 50, game.world.centerY - 200, "Highest = " + deathHighestScoreValue,style);
		var	scoreText = game.add.text(game.world.centerX - 50, game.world.centerY - 100, "Score     = "  + deathScoreValue,style);
		var	loseText = game.add.text(game.world.centerX, game.world.centerY - 300, "Replay?",style);


  },
  setScreenValues: function(score,highestscore) {
	deathScoreValue = score;
	deathHighestScoreValue = highestscore;
	deathHighestScoreValue = deathHighestScoreValue.replace(/\D/g,'');
  }
};
