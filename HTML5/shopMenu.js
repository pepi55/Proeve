// Shop state.
var character = 0;

var shopState = {
  // Custom "variables".
  characters: null,
  backgrounds: null,

  amountOfCharacters: 2, // The amount of images to load.

	// Phaser functions.
	preload: function() {
		characters = game.add.group();
		backgrounds = game.add.group();

		for (var i = 0; i < this.amountOfCharacters; i++) {
			game.load.image('character' + i, 'assets/balls/image.' + i + '.png');
		}

		var str_points = localStorage.getItem('points');

		if (str_points == null || str_points == 'null') {
			points = 0;
		} else {
			points = parseInt(str_points);
		}

		localStorage.clear();
	},

  create: function() {
		for (var i = 0; i < this.amountOfCharacters; i++) {
			var charNum = i;

			var characteButton = game.add.button(100, 200 + 150 * i, 'character' + i, function(num) {
				localStorage.setItem('character', this.num);
			}, { num: i, });

			characters.add(characteButton);
		}

		var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

		pointsText = game.add.text(game.world.centerX, 20, "Points: " + points);
		pointsText.anchor.x = 0.5;

		// Font style
		pointsText.font = 'Arial Black';
		pointsText.fontSize = 50;
		pointsText.fontWeight = 'bold';

		// Stroke color and thickness
		pointsText.stroke = '#0020C2';
		pointsText.strokeThickness = 5;
		pointsText.fill = '#2B65EC';
  },

	update: function() {
	},

	// Custom functions.
	goToMain: function() {
		localStorage.setItem('points', points);
		game.state.start('mainMenu');
	}
};