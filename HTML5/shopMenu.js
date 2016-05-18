// Shop state.
var shopState = {
  // Custom "variables".
  characterGroup: null,
  backgroundGroup: null,

	// Phaser functions.
	preload: function() {
		characterGroup = game.add.group();
		backgroundGroup = game.add.group();

		game.load.json('characters', 'json/characters.json');

		game.load.onFileComplete.add(function() {
			var charactersJSON = game.cache.getJSON('characters');

			if (charactersJSON != null) {
				if (charactersJSON.characters != null) {
					for (var i = 0; i < charactersJSON.characters.length; i++) {
						game.load.image('character' + i, 'assets/balls/' + charactersJSON.characters[i].image);
					}
				}
			}
		}, game);

		game.load.image('lock', 'assets/shop/lock.png');

		/*
		for (var i = 0; i < this.amountOfCharacters; i++) {
			game.load.image('character' + i, 'assets/balls/image.' + i + '.png');
		}
		*/

		var str_points = localStorage.getItem('points');

		if (str_points == null || str_points == 'null') {
			points = 0;
		} else {
			points = parseInt(str_points, 10);
		}

		//localStorage.clear();
	},

  create: function() {
  	/*
		for (var i = 0; i < this.amountOfCharacters; i++) {
			var characterButton = game.add.button(100, 200 + 150 * i, 'character' + i, function() {
				localStorage.setItem('character', this.num);
			}, { num: i, });

			characters.add(characterButton);
		}
		*/
		var backButton;
		  backButton = game.add.button(100, 100, 'backButton', function() {
		  game.state.start('mainMenu');
		  }, this);
		  backButton.anchor.setTo(0.5, 0.5);

		pointsText = game.add.text(game.world.centerX, 20, "Points: " + points);

		var charactersJSON = game.cache.getJSON('characters');
		var xPos = 200;

		for (var i = 0; i < charactersJSON.characters.length; i++) {
			if (i % 5 == 0) {
				xPos += 250;
				yPos = 0;
			}

			yPos += 250;

			var characterButton = game.add.button(xPos, yPos, 'character' + i, function() {
				localStorage.setItem('characterImage', this.charImg);
				localStorage.setItem('characterSound', this.charSnd);
			}, { charSnd: charactersJSON.characters[i].sound, charImg: charactersJSON.characters[i].image });

			//console.log(charactersJSON.characters[i].characterPrice);
			//console.log(localStorage.getItem('pricePaid' + i) + ' ' + i);
			//console.log(localStorage.getItem('pricePaid' + i) == charactersJSON.characters[i].characterPrice);

			if (localStorage.getItem('pricePaid' + i) != charactersJSON.characters[i].characterPrice
			&& parseInt(charactersJSON.characters[i].characterPrice, 10) > 0) {
				var characterLock = game.add.sprite(xPos, yPos, 'lock');

				var style = {
					font: "32px Arial",
					fill: "#ff0044",
					wordWrap: true,
					wordWrapWidth: characterLock.width,
					align: "center",
					backgroundColor: "#ffff00"
				};

				var priceText = game.add.text(characterLock.x, characterLock.y, charactersJSON.characters[i].characterPrice + 'pt$', style);
				priceText.anchor.set(0.5);

				//characterLock.addChild(priceText);

				characterLock.inputEnabled = true;
				characterLock.scale.setTo(1.1);

				characterLock.events.onInputDown.add(function() {
					if (points - this.charPrice >= 0) {
						console.log('unglock, dextroi \'n --pts');

						points -= this.charPrice;
						this.ptsTxt.text = 'points: ' + points;
						localStorage.setItem('pricePaid' + this.charIndex, this.charPrice);

						this.button.destroy();
					} else {
						console.log('not nuff chaching');
					}
				}, { charIndex: i, ptsTxt: pointsText, charPrice: charactersJSON.characters[i].characterPrice, button: characterLock });
			}

			/*
			if (parseInt(charactersJSON.characters[i].characterPrice, 10) > 0) {
				var characterLockButton = game.add.button(100, 200 + 150 * i, 'lock', function() {
					if (this.currency - this.charPrice > 0) {
						console.log('unglock, destroy and minus points');
					} else {
						console.log('not nuff doekes');
					}
				}, { btn: this, charPrice: charactersJSON.characters[i].characterPrice, currency: points });

				characterLockButton.input.priorityID = 1;
			}
			*/

			characterButton.input.priorityID = 0;
			characterGroup.add(characterButton);
		}

		var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

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