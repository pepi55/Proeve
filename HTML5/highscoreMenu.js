// highscore state.
var highscoreList = new Array();
var highscoreNameList = new Array();
var highscoreLadderList = new Array();

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
		style.font = 'Arial Black';
		style.fontSize = 50;
		style.fontWeight = 'bold';

		//	Stroke color and thickness
		style.stroke = '#0C090A';
		style.strokeThickness = 7;
		style.fill = '#52D017';

   	titleStyle = game.add.text(game.world.centerX, 20, "Highscores");
    //	Font style
		titleStyle.font = 'Arial Black';
		titleStyle.fontSize = 50;
		titleStyle.fontWeight = 'bold';

		//	Stroke color and thickness
		titleStyle.stroke = '#0020C2';
		titleStyle.strokeThickness = 5;
		titleStyle.fill = '#2B65EC';

    var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

		//input screen to set the name of the highscore user.
	/*	  var input = new CanvasInput({
      canvas: document.getElementById('canvas'),
      fontSize: 18,
      fontFamily: 'Arial',
      fontColor: '#212121',
      fontWeight: 'bold',
      width: 300,
      padding: 8,
      borderWidth: 1,
      borderColor: '#000',
      borderRadius: 3,
      boxShadow: '1px 1px 0px #fff',
      innerShadow: '0px 0px 5px rgba(0, 0, 0, 0.5)',
      placeHolder: 'Enter message here...'
      });*/

       //localStorage.clear();
  		var str_highscore = JSON.parse(localStorage.getItem('highScore'));

  		if (str_highscore == null || str_highscore == "null") {
  			//highscoreList[0] = 0;
  		} else {
  			console.log("score ="+str_highscore);
  			highscoreList = str_highscore;
  		}

  		console.log(localStorage.getItem(('users')))
  		var str_users = localStorage.getItem('users');

  		if (str_users == null || str_users == "null") {
  		} else {
  			console.log("users ="+str_users);
  			highscoreNameList = str_users;
  			this.checkScoreValues();
  		}
  		console.log(game.world.height);
  },

  update: function() {
  		var addScoreKey = game.input.keyboard.addKey(Phaser.Keyboard.P);
		  addScoreKey.onDown.add(this.addRandomValues, this);

		  var checkScoreKey = game.input.keyboard.addKey(Phaser.Keyboard.O);
		  checkScoreKey.onDown.add(this.checkScoreValues, this);
  },

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

  addRandomValues: function(){
    this.addScore(Math.floor((Math.random() * 100) + 1));
  },
  checkScoreValues: function(){
    //highscoreList.sort(function(a, b){return b-a});
    //console.log(highscoreList.length);

    var sortedList = highscoreList;
    sortedList.sort( function(a,b) { return b - a; } );
      for (var i = 0; i < sortedList.length; i++) {
     //highscoreLadderList.push("" + i + ": \n");
    }

    var scoreText = game.add.text(game.world.centerX + 300, game.world.centerY, '', style);
    scoreText.parseList(sortedList);
    //var ladderText = game.add.text(game.world.centerX, game.world.centerX, '', style);
    //ladderText.parseList(highscoreLadderList);
    console.log(highscoreNameList);
    var usersText = game.add.text(game.world.centerX , game.world.centerY + 60 , '', style);
    //usersText.parseList(highscoreNameList);
    var result = highscoreNameList.replace(/[["]/g, "");
    var second = result.replace(/[,]/g, ":\n");
    //replace(/[^a-zA-Z ]/g, "")
    usersText.text = second;

  },
    goToMain: function() {
		game.state.start('mainMenu');
  },
};