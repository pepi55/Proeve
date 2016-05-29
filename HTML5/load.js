// Loading state for loading all images in cache.
var loadState = {
	preload: function() {
		// Loading image.
		var loadingImage = game.add.sprite(game.world.centerX, game.world.centerY, 'loadingScreenSheet');
		loadingImage.anchor.setTo(0.5);
		loadingImage.animations.add('loading');
		loadingImage.animations.play('loading', 12, true);

		// Audio loading.
		game.load.audio('mainMenuBG','assets/audio/backgroundmusic/menus/mainMenu.wav');
		game.load.audio('gameOverSound','assets/audio/backgroundmusic/menus/gameOver.wav');
		game.load.audio('inGameBG','assets/audio/backgroundmusic/inGame/inGameMusicLoop.wav');
		game.load.audio('startSound','assets/audio/backgroundmusic/inGame/start.wav');

		// Image loading.
		game.load.image('backgroundGame', 'assets/background/background.0.png');
		game.load.image('goal', 'assets/goal/goal.0.png');
		game.load.image('wall', 'assets/image.png');

		game.load.image('deathscreenBackground', 'assets/UI/deathScreen.png');

	  game.load.image('playButton', 'assets/buttons/playButton.png');
	  game.load.image('highscoreButton', 'assets/buttons/highscoreButton.png');
	  game.load.image('shopButton', 'assets/buttons/shopButton.png');
	  game.load.image('backButton', 'assets/buttons/exitButton.png');
		game.load.image('invisibleButton', 'assets/invisibleButton.png');

		game.load.image('muteButton', 'assets/buttons/muteButton.png');
		game.load.image('soundButton', 'assets/buttons/soundButton.png');

		game.load.image('startBackground', 'assets/background/startscreen.png');
		game.load.image('highscoreBackground', 'assets/background/highscorescreen.png');
		game.load.image('logo', 'assets/UI/Vivamacho.png');
		game.load.image('pixel_yellow', 'assets/particles/pixel_yellow.png');
		game.load.image('pixel_blue', 'assets/particles/pixel_blue.png');
		game.load.image('pixel_pink', 'assets/particles/pixel_pink.png');
		game.load.image('cloud_particle1', 'assets/particles/cloud1Particle.png');
		game.load.image('cloud_particle2', 'assets/particles/cloud2Particle.png');
		game.load.image('cloud_particle3', 'assets/particles/cloud3Particle.png');

		game.load.image('lock', 'assets/shop/lock.png');
		game.load.image('backgroundShop', 'assets/shop/shop.0.png');

		// Spriteshit loading.
		// TODO: Decide on how to load balls.
		var charactersJSON = game.cache.getJSON('characters');

		if (charactersJSON != null) {
			if (charactersJSON.characters != null) {
				for (var i = 0; i < charactersJSON.characters.length; i++) {
					game.load.spritesheet('character' + i, 'assets/balls/' + charactersJSON.characters[i].image, 128, 128);
				}
			}
		}
	},

	create: function() {
		game.state.start('mainMenu');
	}
};