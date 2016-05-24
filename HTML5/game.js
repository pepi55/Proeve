// Game state.
var highScore = 0;
var points = 0;

var hardMode = true;

/** This is the state in which the game is played. */
var gameState = {
  // Custom "variables".
  tweenAPosition: 0,
  tweenBPosition: 70,

  textTween: null,
  text: null,
	tapSound: null,

	gameobjects: null,
	backgrounds: null,
  walls: null,

  score: 0,
	tempScore: 0,

  currentHighscore: new Array(),
  currentHighscoreUsers: new Array(),
  randomNames: new Array("Bob","Achmed","Ash","John","Seymore"),

 	/** @method
	* @name preload
	* @description this is a preload function that is fired before the create function, this is where we create variables and images
	*/
  preload: function() {
  	var str_names = JSON.parse(localStorage.getItem('users'));

		if (str_names == null || str_names == "null") {

		} else {
			console.log("names ="+str_names);
			this.currentHighscoreUsers = str_names;
		}

		var str_score = JSON.parse(localStorage.getItem('highScore'));

		if (str_score == null || str_score == "null") {

		} else {
			console.log("score ="+str_score);
			this.currentHighscore = str_score;
		}

		var tapSound = localStorage.getItem('characterSound');

		if (tapSound == null || tapSound == "null") {
			tapSound = 'click2.wav';
		}

		var character = localStorage.getItem('characterImage');

		if (character == null || character == "null") {
			character = 'image.0.png';
		}
		/*
		else {
			character = parseInt(str_character);
		}
		*/

		this.gameobjects = game.add.group();
		this.backgrounds = game.add.group();
		this.walls = game.add.group();

		game.load.image('background', 'assets/background/background.0.png');
		game.load.image('ball', 'assets/balls/' + character);
		game.load.image('goal', 'assets/goal/goal.0.png');
		game.load.image('wall', 'assets/image.png');

		game.load.audio('tapSound', 'assets/audio/soundeffect/' + tapSound);
	},

	/** @method
	* @name create
	* @description this is a create function that is fired after the preload function, this is where we set all the variables
	*/
  create: function() {
		// this has to be active for the fps to be counting.
		game.time.advancedTiming = true;


		game.physics.startSystem(Phaser.Physics.ARCADE);
		game.physics.arcade.gravity.y = 1500;

		game.world.bringToTop(this.backgrounds);
		game.world.bringToTop(this.walls);
		game.world.bringToTop(this.gameobjects);

		// sounds
		tapSound = game.add.audio('tapSound');

		//set tween position
		this.tweenAPosition = game.world.width - 100;
		// Add gameobjects to game
		this.ball = game.add.sprite(game.world.centerX, 20, 'ball');
		this.goal = game.add.sprite(this.tweenAPosition, game.world.height - 50, 'goal');

/*
		this.wall1 = game.add.sprite(-70, game.world.height, 'wall1');
		this.wall2 = game.add.sprite(game.world.width + 70, game.world.height, 'wall2');
		this.ceiling = game.add.sprite()
*/
		this.walls.create(-70, game.world.height, 'wall');
		this.walls.create(game.world.width + 70, game.world.height, 'wall');
		this.walls.create(game.world.width / 2, game.world.height + (game.world.height / 2), 'wall');

		this.background = game.add.tileSprite(0, 0, game.world.width, game.world.height, 'background');

		game.physics.arcade.enable([
			this.ball,
      this.goal,
      this.walls
      ]);

    // Background setup
    this.backgrounds.add(this.background);

		// Ball setup
		this.ball.anchor.setTo(0.5, 0.5);
		this.ball.body.bounce.setTo(0.9, 0.9);

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
		tweenA = game.add.tween(this.goal).to({ x: this.tweenAPosition }, 5000, 'Linear', true, 0);
		tweenB = game.add.tween(this.goal).to({ x: this.tweenBPosition }, 5000, 'Linear', true, 0);

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
	* @description this is a update function that is fired after the create function,and updates every frame.
	*/
  update: function() {
		game.physics.arcade.collide(this.walls, this.ball, this.wallsCollisionHandler, null, this);
		game.physics.arcade.collide(this.goal, this.ball, this.goalCollisionHandler, null, this);

		if (this.ball.world.y >= game.world.height) {
			this.goToMain();
		}

		if(this.goal.world.x == this.tweenAPosition) {
			tweenB.start();
		} else if(this.goal.world.x == this.tweenBPosition) {
			tweenA.start();
		}
  },

	/** @method
	* @name render
	* @description this is a render function that is used for rendering debug texts
	*/
  render: function() {
    // fps log
    game.debug.text(game.time.fps, 2, 14, "#00ff00");
  },

  // Custom functions
  	/** @method
	* @name wallsCollisionHandler
	* @description this is a collision function that is fired when hitting a wall and is used for adding score for a skill shot
	*/
  wallsCollisionHandler: function() {
    this.tempScore++;
  },

 	/** @method
	* @name goalCollisionHandler
	* @description this is a collision function that is fired when the ball hits the goal and is used for adding score for a skill shot
	*/
  goalCollisionHandler: function() {
    //game.state.start('game');
    this.score += this.tempScore + 1;
    this.tempScore = 0;

    this.setScoreText();
    this.ball.body.velocity.setTo(0, 0);
    this.ball.body.angularVelocity = 0;
    this.ball.position.x = game.world.randomX;
    this.ball.position.y = 0;
  },

	/** @method
	* @name setScoreText
	* @description this is a function that is fired when the ball hits the goal and is used for updateing the score text on the UI.
	*/
  setScoreText: function() {
   scoreText.text = this.score;
   textTween.start();
  },
	/** @method
	* @name bounce
	* @description this is a function that is fired when the player taps the screen and is used for adding force to the ball.
	*/
  bounce: function() {
		var yVelocity = 0;
		this.tempScore = 0;
		tapSound.play();

		if (game.input.activePointer.y > this.ball.y) {
		  yVelocity = -800;
		} else {
			this.tempScore++;

		  yVelocity = this.ball.body.velocity.y + 800;
		}

		if (hardMode == true) {
			var xVelocity = Phaser.Math.difference(game.input.activePointer.x, this.ball.x);

			/*
			if (velocityMode == true) {
				xVelocity = 400 - Phaser.Math.difference(game.input.activePointer.x, this.ball.x);

				if (xVelocity < 0) {
				  xVelocity = 0;
				}

			} else {
			}
			*/

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
	* @description this is a function that returns the player to the main menu.
	*/
	goToMain: function() {
		if (this.score > localStorage.getItem('highestScore') && this.score != 0 || this.score > 0 && this.currentHighscore[0] == null) {
			this.currentHighscore.push("\n" + this.score);
			this.currentHighscoreUsers.push(this.randomNames[Math.floor(Math.random() * 5)]);
			localStorage.setItem('users', JSON.stringify(this.currentHighscoreUsers));
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