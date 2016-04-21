// highscore state.
var highscoreList = new Array();
var textList = new Array(10);

var highscoreState = {
  // Custom "variables".
  style: null,
  // Phaser functions.
  preload: function() {

  },

  create: function() {
    for (var i = 0; i < highscoreList.length; i++) {
      Things[i]
    }
    style = { font: "16px Courier", fill: "#fff", tabs: [ 164, 60, 80 ] };

    //	Font style
		style.font = 'Arial Black';
		style.fontSize = 50;
		style.fontWeight = 'bold';

		//	Stroke color and thickness
		style.stroke = '#0020C2';
		style.strokeThickness = 5;
		style.fill = '#2B65EC';
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
    highscoreList.sort(function(a, b){return b-a});
    console.log(highscoreList.length);
    for (var i = 0; i < highscoreList.length; i++) {
      console.log(highscoreList[i]);
    }

    var sortedList = highscoreList;
    sortedList.sort( function(a,b) { return b - a; } );
    var text2 = game.add.text(game.world.centerX, 120, '', style);
    text2.parseList(sortedList);
  },
};