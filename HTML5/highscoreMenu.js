// highscore state.
var highscoreList = new Array();
var highscoreNameList = new Array();
var highscoreLadderList = new Array();

/** This is the state in which the game is played. */
var highscoreState = {
  // Custom "variables".
  style: null,
  titleStyle: null,
  // Phaser functions.
  preload: function() {

  },

  create: function() {



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

   	titleStyle = game.add.text(game.world.centerX - 100, 20, "HIGHSCORES");
    //	Font style
		titleStyle.font = 'Passion One';
		titleStyle.fontSize = 50;
		titleStyle.fontWeight = 'bold';
console.log(titleStyle.font);
		//	Stroke color and thickness
		titleStyle.stroke = '#0020C2';
		titleStyle.strokeThickness = 5;
		titleStyle.fill = '#2B65EC';



    var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);



  		var str_highscore = JSON.parse(localStorage.getItem('highScore'));

  		if (str_highscore == null || str_highscore == "null") {
  			//highscoreList[0] = 0;
  		} else {
  			console.log("score ="+str_highscore);
  			highscoreList = str_highscore;
  		}

  		var str_users = localStorage.getItem('users');

  		if (str_users == null || str_users == "null") {
  		} else {
  			console.log("users ="+str_users);
  			highscoreNameList = str_users;
  			this.checkScoreValues();
  		}
  		var backButton;
		  backButton = game.add.button(100, 100, 'backButton', function() {
		  game.state.start('mainMenu');
		  }, this);
		  backButton.anchor.setTo(0.5, 0.5);
  },

  update: function() {
  		var addScoreKey = game.input.keyboard.addKey(Phaser.Keyboard.P);
		  addScoreKey.onDown.add(this.addRandomValues, this);

		  var checkScoreKey = game.input.keyboard.addKey(Phaser.Keyboard.O);
		  checkScoreKey.onDown.add(this.checkScoreValues, this);
  },

  	/** @method
	* @name addScore
	* @description this is a function that adds a score to the highscore list
	*/
  addScore: function(scoreToAdd) {
    if(typeof scoreToAdd === 'number') {
      console.log(scoreToAdd);
      if(highscoreList.length >= 9)
      {
        highscoreList.splice(10,1);
      }
      highscoreList.push("\n" +scoreToAdd );

    }
  },
  /** @method
	* @name addRandomValues
	* @description this is a function that sends a random number between 0-100 to addscore for developing purposes.
	*/
  addRandomValues: function(){
    this.addScore(Math.floor((Math.random() * 100) + 1));
  },
   /** @method
	* @name checkScoreValues
	* @description this is a function checks the scores and in the arrays and displays them on the screen.
	*/
  checkScoreValues: function(){
    //highscoreList.sort(function(a, b){return b-a});
    //console.log(highscoreList.length);
    highscoreLadderList = []
    var sortedList = highscoreList;
    sortedList.sort( function(a,b) { return b - a; } );
      for (var i = 0; i < sortedList.length; i++) {
     highscoreLadderList.push("" + (i+1) + ".\n");
    }

    var scoreText = game.add.text(game.world.centerX - 100 + 100, 200, '', style);
    scoreText.parseList(sortedList);
    var ladderText = game.add.text(game.world.centerX - 100, 300, '', style);
    ladderText.parseList(highscoreLadderList);
    console.log(highscoreNameList);
    //var usersText = game.add.text(game.world.centerX , game.world.centerY + 60 , '', style);
    //usersText.parseList(highscoreNameList);
    //var result = highscoreNameList.replace(/[["]/g, "");
    //var second = result.replace(/[,]/g, ":\n");
    //replace(/[^a-zA-Z ]/g, "")
    //usersText.text = second;

  },
    goToMain: function() {
		game.state.start('mainMenu');
  },
};