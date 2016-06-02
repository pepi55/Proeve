var highscoreList = new Array();
var highscoreNameList = new Array();
var highscoreLadderList = new Array();

/**
 * @Class
 * @name highscoreState
 * @desc state that shows the highscores you have obtained.
 * @property {number}			deathScoreValue			   		 -	current score that is shown in the death screen.
 * @property {number}			deathHighestScoreValue		 -	highest score the player had that is shown in the death screen.
 * @property {sound}			deathScreenBGMusic				 -	sound that is played on lose.
 * @property {fontStyle}	style				 							 -	style of the font in the score.
 * @property {text}	titleStyle				 							 -	style of the font in the screen.
 */
var highscoreState = {
  // Custom "variables".
  style: null,
  titleStyle: null,
  // Phaser functions.

  create: function() {
   var highscoreBackground = game.add.sprite(0, 0,'highscoreBackground');
    style = {};

    //	Font style
		style.font = 'Passion One';
		style.fontSize = 75;
		style.fontWeight = 'bold';

		//	Stroke color and thickness
		style.stroke = '#FFFF00';
		style.strokeThickness = 3;
		style.fill = '#FF2828';

   	titleText = game.add.text(game.world.centerX - 150, 70, "HIGHSCORES");
    //	Font style
		titleText.font = 'Passion One';
		titleText.fontSize = 50;
		titleText.fontWeight = 'bold';
		//	Stroke color and thickness
		titleText.stroke = '#0020C2';
		titleText.strokeThickness = 5;
		titleText.fill = '#2B65EC';

    var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

  		var str_highscore = JSON.parse(localStorage.getItem('highScore'));

  		if (str_highscore == null || str_highscore == "null") {
  			//highscoreList[0] = 0;
  		} else {
  			highscoreList = str_highscore;
  			this.checkScoreValues();
  		}




  		var backButton;
		  backButton = game.add.button(100, 100, 'backButton', function() {
		  game.state.start('mainMenu');
		  }, this);
		  backButton.anchor.setTo(0.5, 0.5);
  },

  	/** @method
	* @name addScore
	* @memberof highscoreState
	* @description this is a function that adds a score to the highscore list
	*/
  addScore: function(scoreToAdd) {
    if(typeof scoreToAdd === 'number') {
      if(highscoreList.length >= 9)
      {
        highscoreList.splice(10,1);
      }
      highscoreList.push("\n" +scoreToAdd );

    }
  },
   /** @method
	* @name checkScoreValues
	* @memberof highscoreState
	* @description this is a function checks the scores and in the arrays and displays them on the screen.
	*/
  checkScoreValues: function(){
    highscoreLadderList = []
    var sortedList = highscoreList;
    sortedList.sort( function(a,b) { return b - a; } );
    console.log(sortedList);
      for (var i = 0; i < sortedList.length; i++) {
     highscoreLadderList.push("" + (i+1) + ".\n");
    }
    var scoreText = game.add.text(game.world.centerX - 100 + 100, 200, '', style);
    scoreText.parseList(sortedList);
    var ladderText = game.add.text(game.world.centerX - 100, 300, '', style);
    ladderText.parseList(highscoreLadderList);
  },
    goToMain: function() {
		game.state.start('mainMenu');
  },
};