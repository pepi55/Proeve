// Game state.
var highScore = 0;
var points = 0;

var hardMode = true;

/**
 * @Class
 * @name gameState
 * @desc state in which the game is played.
 * @property {particle}	ingameEmitter			   -	Particle emmiter for the ball.
 * @property {tween}	tweenAPosition				 -	First tween position for the goal.
 * @property {tween}	tweenBPosition				 -	Second tween position for the goal.
 * @property {tween}	textTween							 -	Tween to scale the text on score.
 * @property {text}	  textScore							 -  text field.
 * @property {sound}	tapSound							 -	Sound that plays on tap/click.
 * @property {sound}	startSound						 -	Sound that plays on start.
 * @property {sound}	inGameBGMusic					 -	Sound that plays after the start sound and loops.
 * @property {object}	gameobjects 					 -	gameobjects in the gameState.
 * @property {tileSprite}	backgrounds 			 -	Background sprite.
 * @property {object}	walls 								 -	The wall objects that the ball can bounce on.
 * @property {number}	charNr 								 -	Number of the current character selected.
 * @property {number}	score 								 -	Score for getting the ball into the goal.
 * @property {number}	tempScore 						 -	A temporary score for trickshots.
 * @property {string}	currentHighscore 			 -	How much gold the party starts with.
 * @property {string}	setName 							 -	Name for selecting a random background.
 */
var gameState = {
	// Custom "variables".
	ingameEmitter: null,
	tweenAPosition: 0,
	tweenBPosition: 300,

	tween: null,
	textTween: null,
	text: null,
	tapSound: null,
	startSound: null,
	inGameBGMusic: null,

	setName: null,
	gameobjects: null,
	backgrounds: null,
	walls: null,

	charNr: 0,

	score: 0,
	tempScore: 0,

	currentHighscore: new Array(),

	/** @method
	 * @name preload
	 * @memberof gameState
	 * @description this is a preload function that is fired before the create function, this is where we create variables and images
	 */
	preload: function() {
		// Load tapsound.
		menuBGMusic.stop();
		var tapSound = localStorage.getItem('characterSound');
		if (tapSound == null || tapSound == "null") {
			tapSound = 'click2.wav';
		}
		game.load.audio('tapSound', 'assets/audio/soundeffect/' + tapSound);

		// setting game relevant sounds.
		startSound = game.add.audio('startSound');
		inGameBGMusic = game.add.audio('inGameBG');
		startSound.onStop.add(this.startBG,this);

		//making object groups.
		this.gameobjects = game.add.group();
		this.backgrounds = game.add.group();
		this.walls = game.add.group();

		var randomValue = Math.floor(Math.random() * 5) + 0;
		setName = "backgroundGame" + randomValue;
	},

	 /** @method
		* @name create
		* @memberof gameState
		* @description this is a create function that is fired after the preload function, this is where we set all the variables
		*/
	create: function() {
		//set tween movement speed.
		tweenValue = 5000;
		startSound.volume = 0.2;
		startSound.play();

		// Load character number.
		var characterInt = 0;
		var str_character = parseInt(localStorage.getItem('characterImageNr'), 10);
		if (str_character == null || str_character == "null" || isNaN(str_character)) {
			characterInt = 0;
		} else {
			characterInt = str_character;
		}

		// Load highscore.
		var str_score = JSON.parse(localStorage.getItem('highScore'));
		if (str_score == null || str_score == "null") {
		} else {
			this.currentHighscore = str_score;
		}

		// this has to be active for the fps to be counting.
		game.time.advancedTiming = true;

		//set the game physics.
		game.physics.startSystem(Phaser.Physics.ARCADE);
		game.physics.arcade.gravity.y = 1500;

		//bring game objects to the top of the screen(layering).
		game.world.bringToTop(this.backgrounds);
		game.world.bringToTop(this.walls);
		game.world.bringToTop(this.gameobjects);

		// sounds
		tapSound = game.add.audio('tapSound');

		//set tween position
		this.tweenAPosition = game.world.width - 300;

		// Add gameobjects to game
		this.ball = game.add.sprite(game.world.centerX, 20, 'character' + characterInt);
		this.goal = game.add.sprite(this.tweenAPosition, game.world.height - 50, 'goal');

		this.ballIsAnimated = this.ball.animations.validateFrames([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]);
		if (this.ballIsAnimated) {
			this.ball.animations.add('idle', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], 10, false);
			this.ball.animations.add('tapnimation', [12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23], 24, false);
			this.ball.animations.play('idle');
		}

		this.walls.create(-70, game.world.height, 'wall');
		this.walls.create(game.world.width + 70, game.world.height, 'wall');
		this.walls.create(game.world.width / 2, game.world.height + (game.world.height / 2), 'wall');

		this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, setName);

		game.physics.arcade.enable([
			this.ball,
			this.goal,
			this.walls
		]);

		// Background setup
		this.backgrounds.add(this.background);

		// Ball setup
		this.ball.anchor.setTo(0.5, 0.5);
		this.ball.body.bounce.setTo(0.5, 0.5);

		this.gameobjects.add(this.ball);

		// Goal setup
		this.goal.anchor.setTo(0.5, 0.5);
		this.goal.body.immovable = true;
		this.goal.body.allowGravity = false;
		this.goal.body.setSize(64, 64, 0, 0);

		this.gameobjects.add(this.goal);

		// Walls setup
		for (var i = 0; i < this.walls.children.length; i++) {
			this.walls.children[i].anchor.setTo(0.5, 1);
			this.walls.children[i].scale.setTo(0.5, game.world.height + (game.world.height / 2));
			this.walls.children[i].body.immovable = true;
			this.walls.children[i].body.allowGravity = false;
		}

		// Reset ceiling size
		this.walls.children[2].anchor.setTo(0.5);
		this.walls.children[2].scale.setTo(50, 0.5);

		// Back button.
		var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

		// Ball control.
		game.input.onDown.add(this.bounce, this);

		//set the goals tweens.
		tween = this.game.add.tween(this.goal).to({
			x: [this.tweenBPosition,this.tweenAPosition]
		}, 5000);
		tween.start();
		tween.onComplete.add(this.setNewTweenSpeed, this);
		

		ingameEmitter = game.add.emitter(0, 0, 100);

		ingameEmitter.makeParticles(['cloud_particle1','cloud_particle2','cloud_particle3']);
		ingameEmitter.setScale(0.5, 0, 0.5, 0, 6000);
		game.input.onDown.add(this.particleBurst, this);

		//set score text
		scoreText = game.add.text(game.world.centerX, 20, "0");

		//	Font style
		scoreText.font = 'Arial Black';
		scoreText.fontSize = 50;
		scoreText.fontWeight = 'bold';

		//	Stroke color and thickness
		scoreText.stroke = '#FFFF00';
		scoreText.strokeThickness = 3;
		scoreText.fill = '#FF2828';

		textTween = game.add.tween(scoreText).to({ fontSize:100}, 100, Phaser.Easing.Linear.None, false, 0,0,true);
	},

	/** @method
	 * @name update
	 * @memberof gameState
	 * @description this is a update function that is fired after the create function,and updates every frame.
	 */
	update: function() {
		game.physics.arcade.collide(this.walls, this.ball, this.wallsCollisionHandler, null, this);
		game.physics.arcade.collide(this.goal, this.ball, this.goalCollisionHandler, null, this);

		if (this.ball.world.y >= game.world.height) {
			this.goToMain();
		}

		if (this.goal.world.x == this.tweenAPosition) {
			//	tweenB.start();
		} else if(this.goal.world.x == this.tweenBPosition) {
			//	tweenA.start();
		}

		if (this.ballIsAnimated && this.ball.animations.currentAnim.isFinished) {
			this.ball.animations.play('idle');
		}
		
		//goal move
		//goal speed = 0.5f + (-1 * Mathf.Exp(-GameManager.Score / (float)scoreCurvMax) + 1) * MaxSpeed;
		
		var speed = 0.5 + (-1 * Math.exp(-score/scoreCurvMax)+1)*maxSpeed;
	},

	particleBurst: function() {
		// Position the emitter where the mouse/touch event was
		ingameEmitter.x = this.ball.world.x;
		ingameEmitter.y = this.ball.world.y;

		// The first parameter sets the effect to "explode" which means all particles are emitted at once
		// The second gives each particle a 2000ms lifespan
		// The third is ignored when using burst/explode mode
		// The final parameter is how many particles will be emitted in this single burst
		ingameEmitter.start(true, 2000, null, 10);
	},

	 // Custom functions
	 /** @method
		* @name wallsCollisionHandler
		* @memberof gameState
		* @description this is a collision function that is fired when hitting a wall and is used for adding score for a skill shot
		*/
	wallsCollisionHandler: function() {
		this.tempScore++;
	},

	 /** @method
		* @name startBG
		* @memberof gameState
		* @description this is a function that starts the background music.
		*/
	startBG: function() {
		inGameBGMusic.volume = 0.2;
		inGameBGMusic.loop = true;
		inGameBGMusic.play();
	},

	 /** @method
		* @name goalCollisionHandler
		* @memberof gameState
		* @description this is a collision function that is fired when the ball hits the goal and is used for adding score for a skill shot
		*/
	goalCollisionHandler: function() {
		//game.state.start('game');
		this.score += this.tempScore + 1;
		this.tempScore = 0;

		this.setScoreText();
		this.ball.body.velocity.setTo(0, 0);
		this.ball.body.angularVelocity = 0;
		this.ball.position.x = game.rnd.integerInRange(this.tweenAPosition,this.tweenBPosition);
		this.ball.position.y = 0;
	},

	/** @method
	 * @name setNewTweenSpeed
	 * @memberof gameState
	 * @description this function is fired when the goal tween is completed and keeps looping with a random speed.
	 */
	setNewTweenSpeed: function(){
		tween = this.game.add.tween(this.goal).to({
			x: [this.tweenBPosition,this.tweenAPosition]
		}, game.rnd.integerInRange(2500,5000));

		tween.start();
		tween.onComplete.add(this.setNewTweenSpeed, this);
	},

	/** @method
	 * @name setScoreText
	 * @memberof gameState
	 * @description this is a function that is fired when the ball hits the goal and is used for updateing the score text on the UI.
	 */
	setScoreText: function() {
		scoreText.text = this.score;
		textTween.start();
	},

	/** @method
	 * @name bounce
	 * @memberof gameState
	 * @description this is a function that is fired when the player taps the screen and is used for the ball movement.
	 */
	bounce: function() {
		var yVelocity = 0;
		this.tempScore = 0;
		tapSound.play();

		if (this.ballIsAnimated) {
			this.ball.animations.play('tapnimation');
		}

		if (game.input.activePointer.y > this.ball.y) {
			yVelocity = -800;
		} else {
			this.tempScore++;

			yVelocity = this.ball.body.velocity.y + 800;
		}

		if (hardMode == true) {
			var xVelocity = Phaser.Math.difference(game.input.activePointer.x, this.ball.x);

			this.ball.body.angularVelocity = angVelocity = this.ball.x - game.input.activePointer.x;

			if (game.input.activePointer.x > this.ball.x) {
				this.ball.body.velocity.setTo(this.ball.body.velocity.x + -xVelocity, yVelocity);
			} else {
				this.ball.body.velocity.setTo(this.ball.body.velocity.x + xVelocity, yVelocity);
			}
		} else {
			this.ball.body.velocity.setTo(this.ball.body.velocity.x, yVelocity);
		}
	},

	/** @method
	 * @name goToMain
	 * @memberof gameState
	 * @description this is a function that returns the player to the main menu.
	 */
	goToMain: function() {
		inGameBGMusic.stop();
		startSound.onStop.remove(this.startBG,this);
		startSound.stop();

		if (this.score > localStorage.getItem('highestScore') && this.score != 0 || this.score > 0 && this.currentHighscore[0] == null) {
			this.currentHighscore.push("\n" + this.score);
			localStorage.setItem('highScore', JSON.stringify(this.currentHighscore));
			localStorage.setItem('highestScore', this.score);
		}

		deathState.setScreenValues(this.score, localStorage.getItem('highestScore'));
		points += this.score;

		localStorage.setItem('points', points);
		this.score = 0;

		game.state.start('deathScreen');
	},
};
