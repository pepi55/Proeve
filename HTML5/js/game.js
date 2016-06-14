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
	goalEmitter: null,
	tweenAPosition: 300,
	tweenBPosition: 0,
	goalSpeed: 2,
	moveLeft: true,

	tween: null,
	textTween: null,
	goalTween: null,
	text: null,
	goalText: null,
	genericTapSound: null,
	tapSound: null,
	dropSound: null,
	applauseSound: null,
	startSound: null,
	inGameBGMusic: null,

	setName: null,
	gameobjects: null,
	backgrounds: null,
	walls: null,
	balls: null,

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
			tapSound = 'rock/rockKlick.wav';
		}

		game.load.audio('tapSound', 'assets/audio/soundeffect/' + tapSound);

		// NOTICE ME KUHWHAM SEMPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI
		var particleImgs = JSON.parse(localStorage.getItem('characterParticles'));
		if (particleImgs == null || particleImgs == "null") {
			particleImgs = [ 'assets/particles/cloud1Particle.png', 'assets/particles/tumbleweed1Particle.png', 'assets/particles/sombrero1Particle.png' ];
		}

		for (var i = 0; i < particleImgs.length; i++) {
			game.load.image('particle' + i, particleImgs[i]);
		}
		// NOTICE ME KUHWHAM SEMPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI NOTICE ME KUHWHAM SENPAI

		// setting game relevant sounds.
		//startSound = game.add.audio('startSound');
		//startSound.onStop.add(this.startBG,this);

		//making object groups.
		this.gameobjects = game.add.group();
		this.backgrounds = game.add.group();
		this.walls = game.add.group();
		this.balls = game.add.group();

		var randomValue = Math.floor(Math.random() * 5) + 0;
		setName = "backgroundGame" + randomValue;
	},

	 /** @method
		* @name create
		* @memberof gameState
		* @description this is a create function that is fired after the preload function, this is where we set all the variables
		*/
	create: function() {
		this.score = 0;
		this.goalSpeed = 2;
		//set tween movement speed.
		tweenValue = 5000;
		//startSound.volume = 0.2;
		//startSound.play();

		// Load highscore.
		var str_score = JSON.parse(localStorage.getItem('highScore'));
		if (str_score == null || str_score == "null") {
		} else {
			this.currentHighscore = str_score;
		}

		//set the game physics.
		game.physics.startSystem(Phaser.Physics.ARCADE);
		game.physics.arcade.gravity.y = 1000;

		//bring game objects to the top of the screen(layering).
		game.world.bringToTop(this.backgrounds);
		game.world.bringToTop(this.walls);
		game.world.bringToTop(this.gameobjects);
		game.world.bringToTop(this.balls);

		// sounds
		tapSound = game.add.audio('tapSound');
		genericTapSound = game.add.audio('genericTapSound');
		dropSound = game.add.audio('dropSound');
		applauseSound = game.add.audio('applauseSound');
		//set tween position
		this.tweenAPosition = game.world.width - 300;
		this.tweenBPosition = 300;

		goalEmitter = game.add.emitter(0, 0, 0);
		goalEmitter.makeParticles(['star1','star2','star3']);
		goalEmitter.minParticleSpeed.setTo(-300, -1500);
    goalEmitter.maxParticleSpeed.setTo(300,-200 );
    goalEmitter.minRotation = 0;
    goalEmitter.maxRotation = 0;
  	goalEmitter.setScale(0.5, 0, 0.5, 0, 6000);

		// Add gameobjects to game
		this.goal = game.add.sprite(this.tweenAPosition, game.world.height - 50, 'goal');
		var background = game.add.tileSprite(0, 0, game.world.width, game.world.height, setName);

		game.physics.arcade.enable([
			this.goal,
			this.balls,
			this.walls
		]);

		this.addBall();

		// Background setup
		this.backgrounds.add(background);

		// Goal setup
		this.goal.anchor.setTo(0.5, 0.5);
		this.goal.body.immovable = true;
		this.goal.body.allowGravity = false;
		this.goal.body.setSize(64, 64, 0, 0);

		this.gameobjects.add(this.goal);

		// Walls setup
		this.walls.create(-200, game.world.height, 'wall');
		this.walls.create(game.world.width + 200, game.world.height, 'wall');
		this.walls.create(game.world.width / 2, game.world.height + (game.world.height / 2), 'wall');

		this.walls.forEach(function(wall) {
			wall.anchor.setTo(0.5, 1);
			wall.scale.setTo(05, game.world.height + (game.world.height / 2));

			game.physics.arcade.enable(wall);
			wall.body.immovable = true;
			wall.body.allowGravity = false;
		}, this);

		// Reset ceiling size
		this.walls.children[2].anchor.setTo(0.5);
		this.walls.children[2].scale.setTo(50, 0.5);

		// Back button.
		var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

		// Ball control.
		game.input.onDown.add(this.bounce, this);

		ingameEmitter = game.add.emitter(0, 0, 100);
		ingameEmitter.makeParticles(['particle0','particle1','particle2']);
		ingameEmitter.setScale(0.5, 0, 0.5, 0, 6000);

		game.input.onDown.add(this.particleBurst, this);

		//set score text
		scoreText = game.add.text(game.world.centerX, 20, "0");
		goalText = game.add.text(game.world.centerX, game.world.centerY, "GOAL!!!");

				//	Font style
		goalText.font = 'Arial Black';
		goalText.fontSize = 1;
		goalText.fontWeight = 'bold';

		//	Stroke color and thickness
		goalText.stroke = '#FFFF00';
		goalText.strokeThickness = 10;
		goalText.fill = '#FFFF66';
		goalText.anchor.setTo(0.5,0.5);
		goalText.visible = false;
		//	Font style
		scoreText.font = 'Arial Black';
		scoreText.fontSize = 100;
		scoreText.fontWeight = 'bold';

		//	Stroke color and thickness
		scoreText.stroke = '#FFFF00';
		scoreText.strokeThickness = 3;
		scoreText.fill = '#FF2828';

		goalTween = game.add.tween(goalText).to({ fontSize:200}, 200, Phaser.Easing.Linear.None, false, 0,0,true);
		goalTween.onComplete.add(this.TextVisible,this);
		textTween = game.add.tween(scoreText).to({ fontSize:200}, 100, Phaser.Easing.Linear.None, false, 0,0,true);
	},

	/** @method
	 * @name update
	 * @memberof gameState
	 * @description this is a update function that is fired after the create function,and updates every frame.
	 */
	update: function() {
		game.physics.arcade.collide(this.walls, this.balls, this.wallsCollisionHandler, null, this);
		game.physics.arcade.collide(this.goal, this.balls, this.goalCollisionHandler, null, this);

		this.balls.forEach(function(ball) {
			if (ball.world.y >= game.world.height) {
				ball.kill();

				if (this.balls.total <= 0) {
					this.goToMain();
				}
			}

			if (ball.ballIsAnimated && ball.animations.currentAnim.isFinished) {
				ball.animations.play('idle');
			}
		}, this);

		if (this.goal.world.x >= this.tweenAPosition) {
			moveLeft = true;
		}
		if(this.goal.world.x <= this.tweenBPosition) {
			moveLeft = false;
		}
		if(moveLeft)
		{
			this.goal.x -= this.goalSpeed;
		}
		else{
			this.goal.x += this.goalSpeed;
		}

		//goal move
		//goal speed = 0.5f + (-1 * Mathf.Exp(-GameManager.Score / (float)scoreCurvMax) + 1) * MaxSpeed;
	},

	particleBurst: function() {
		this.balls.forEachAlive(function(ball) {
			ball.practicles();
		}, this);
	},
		goalParticleBurst: function() {
		//  Position the emitter where the mouse/touch event was
    goalEmitter.x = this.goal.world.x;
  	goalEmitter.y = this.goal.world.y;

    //  The first parameter sets the effect to "explode" which means all particles are emitted at once
    //  The second gives each particle a 2000ms lifespan
    //  The third is ignored when using burst/explode mode
    //  The final parameter (10) is how many particles will be emitted in this single burst
    goalEmitter.start(true, 2000, null, 20);
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
	TextVisible: function() {
	goalText.visible = false;
	},

	/** @method
	 * @name goalCollisionHandler
	 * @memberof gameState
	 * @description this is a collision function that is fired when the ball hits the goal and is used for adding score for a skill shot
	 */
	goalCollisionHandler: function(goal, ball) {
		this.score += this.tempScore + 1;
		this.tempScore = 0;
		this.setScoreText();

		if (this.score % 5 == 0) {
			this.addBall();
		}

		ball.body.velocity.setTo(0, 0);
		ball.body.angularVelocity = 0;
		ball.position.x = game.rnd.integerInRange(this.tweenAPosition, this.tweenBPosition);
		ball.position.y = 100;

		ball.addPointScored();
		this.goalParticleBurst();
		dropSound.play();
		tapSound.play();
		goalText.visible = true;
		goalTween.start();
		this.goalSpeed += 0.2;
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
	 * @name addBall
	 * @memberof gameState
	 * @description This function adds a ball to the game.
	 */
	addBall: function() {
		var ball = new BallObject(game, game.rnd.integerInRange(this.tweenAPosition, this.tweenBPosition), 100);
		ball.body.allowGravity = false;

		this.balls.add(ball);
	},

	/** @method
	 * @name bounce
	 * @memberof gameState
	 * @description this is a function that is fired when the player taps the screen and is used for the ball movement.
	 */
	bounce: function() {
		this.tempScore = 0;
		genericTapSound.play();
		this.balls.forEachAlive(function(ball) {
			ball.bounce();

			if (!ball.body.allowGravity) {
				ball.body.allowGravity = true;
			}
		}, this);
	},

	/** @method
	 * @name goToMain
	 * @memberof gameState
	 * @description this is a function that returns the player to the main menu.
	 */
	goToMain: function() {
		//inGameBGMusic.stop();
		//startSound.onStop.remove(this.startBG,this);
		//startSound.stop();

		if (this.score > localStorage.getItem('highestScore') && this.score != 0 || this.score > 0 && this.currentHighscore[0] == null) {
			this.currentHighscore.push("\n" + this.score);
			localStorage.setItem('highScore', JSON.stringify(this.currentHighscore));
			localStorage.setItem('highestScore', this.score);
		}

		deathState.setScreenValues(this.score, localStorage.getItem('highestScore'));
		points += this.score;

		localStorage.setItem('points', points);

		game.state.start('deathScreen');
	},
};