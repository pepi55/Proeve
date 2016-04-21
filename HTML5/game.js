// Game state.
var highScore = 0;
var points = 0;

var hardMode = true;

var gameState = {
  // Custom "variables".
  tweenAPosition: 900,
  tweenBPosition: 70,

  textTween: null,
	tempScore: 0,
  text: null,

  walls: null,

  score: 0,

  // Native functions.
  preload: function() {
		var str_score = localStorage.getItem('highScore');

		if (str_score == null || str_score == "null") {
			highscore = 0;
		} else {
			highScore = parseInt(str_score);
		}

		var str_character = localStorage.getItem('character');

		if (str_character == null || str_character == "null") {
			character = 0;
		} else {
			character = parseInt(str_character);
		}

		localStorage.clear();

		walls = game.add.group();

		game.load.image('ball', 'assets/balls/image.' + character + '.png');
		game.load.image('goal', 'assets/image.png');

		game.load.image('wall1', 'assets/image.png');
		game.load.image('wall2', 'assets/image.png');
	},

  create: function() {
		// this has to be active for the fps to be counting.
		game.time.advancedTiming = true;

		game.stage.backgroundColor = '#C85A17';
		game.physics.startSystem(Phaser.Physics.ARCADE);
		game.physics.arcade.gravity.y = 1000;

		// Add gameobjects to game
		this.ball = game.add.sprite(game.world.centerX, 20, 'ball');
		this.goal = game.add.sprite(this.tweenAPosition, game.world.height - 50, 'goal');

		this.wall1 = game.add.sprite(-64, game.world.height, 'wall1');
		this.wall2 = game.add.sprite(game.world.width + 64, game.world.height, 'wall2');

		game.physics.arcade.enable([
			this.ball,
      this.goal,
      this.wall1,
      this.wall2
      ]);

		// Ball setup
		this.ball.anchor.setTo(0.5, 0.5);
		this.ball.body.bounce.setTo(0.9, 0.9);

		// Goal setup
		this.goal.anchor.setTo(0.5, 0.5);
		this.goal.body.immovable = true;
		this.goal.body.allowGravity = false;

		// Walls setup
		this.wall1.anchor.setTo(0.5, 1);
		this.wall2.anchor.setTo(0.5, 1);
		this.wall1.scale.setTo(0.5, 50);
		this.wall2.scale.setTo(0.5, 50);

		this.wall1.body.immovable = true;
		this.wall2.body.immovable = true;
		this.wall1.body.allowGravity = false;
		this.wall2.body.allowGravity = false;

		walls.add(this.wall1);
		walls.add(this.wall2);

		var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
		escKey.onDown.add(this.goToMain, this);

		game.input.onDown.add(this.bounce, this);

		//set the goals tweens.
		tweenA = game.add.tween(this.goal).to({ x: this.tweenAPosition }, 2500, 'Linear', true, 0);
		tweenB = game.add.tween(this.goal).to({ x: this.tweenBPosition }, 2500, 'Linear', true, 0);

		//set score text
		scoreText = game.add.text(game.world.centerX, 20, "0");

		//	Font style
		scoreText.font = 'Arial Black';
		scoreText.fontSize = 50;
		scoreText.fontWeight = 'bold';

		//	Stroke color and thickness
		scoreText.stroke = '#0020C2';
		scoreText.strokeThickness = 5;
		scoreText.fill = '#2B65EC';

		textTween = game.add.tween(scoreText).to({ fontSize:100}, 100, Phaser.Easing.Linear.None, false, 0,0,true);
  },

  update: function() {
		game.physics.arcade.collide(walls, this.ball, this.wallsCollisionHandler, null, this);
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

  // Used for rendering debug texts
  render: function() {
    // fps log
    game.debug.text(game.time.fps, 2, 14, "#00ff00");
  },

  // Custom functions
  wallsCollisionHandler: function() {
    this.tempScore++;
  },

  goalCollisionHandler: function() {
    //game.state.start('game');
    this.score += this.tempScore + 1;
    this.tempScore = 0;
    points += this.score;

    this.setScoreText();
    this.ball.body.velocity.setTo(0, 0);
    this.ball.position.x = game.world.randomX;
    this.ball.position.y = 0;
  },

  setScoreText: function() {
   scoreText.text = "" + this.score;
   textTween.start();
  },

  bounce: function() {
		var yVelocity = 0;
		this.tempScore = 0;

		if (game.input.activePointer.y > this.ball.y) {
		  yVelocity = -400;
		} else {
		  yVelocity = this.ball.body.velocity.y + 400;
		}

		if(hardMode == true) {
		  var xVelocity = Phaser.Math.difference(game.input.activePointer.x, this.ball.x);
		  /*
		  if (xVelocity < 0) {
	      xVelocity = 0;
		  }
		  */

		  if (game.input.activePointer.x > this.ball.x) {
	      this.ball.body.velocity.setTo(this.ball.body.velocity.x + -xVelocity, yVelocity);
		  } else {
	      this.ball.body.velocity.setTo(this.ball.body.velocity.x + xVelocity, yVelocity);
		  }
		} else {
		  this.ball.body.velocity.setTo(this.ball.body.velocity.x, yVelocity);
		}
  },

  goToMain: function() {
		if (this.score > highScore) {
			localStorage.setItem('highScore', this.score);
		}

		localStorage.setItem('points', points);

		this.score = 0;
		game.state.start('mainMenu');
  },
};