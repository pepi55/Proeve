// Shop state.
var shopState = {
  // Custom "variables".
  characterGroup: null,
  backgroundGroup: null,

	// Phaser functions.
	preload: function() {
		this.characterGroup = game.add.group();
		this.backgroundGroup = game.add.group();

		game.world.bringToTop(this.backgroundGroup);
		game.world.bringToTop(this.characterGroup);

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
		this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, 'backgroundShop');
		this.backgroundGroup.add(this.background);

		var backButton;
		backButton = game.add.button(100, 100, 'backButton', function() {
		game.state.start('mainMenu');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);

		pointsText = game.add.text(game.world.centerX, 20, "Points: " + points);

		var charactersJSON = game.cache.getJSON('characters');
		var xPos = 100;

		for (var i = 0; i < charactersJSON.characters.length; i++) {
			if (i % 3 == 0) {
				xPos += 280;
				yPos = 0;
			}

			yPos += 360;

			var characterButton = game.add.button(xPos, yPos, 'character' + i, function() {
				localStorage.setItem('characterImage', this.charImg);
				localStorage.setItem('characterImageNr', this.charNr);
				localStorage.setItem('characterSound', this.charSnd);
			}, { charNr: i, charSnd: charactersJSON.characters[i].sound, charImg: charactersJSON.characters[i].image });

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

			characterButton.input.priorityID = 0;
			this.characterGroup.add(characterButton);
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