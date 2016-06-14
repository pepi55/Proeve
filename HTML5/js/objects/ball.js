BallObject = function(game, x, y) {
	// Load character number.
	var characterInt = 0;
	var str_character = parseInt(localStorage.getItem('characterImageNr'), 10);
	if (str_character == null || str_character == "null" || isNaN(str_character)) {
		characterInt = 0;
	} else {
		characterInt = str_character;
	}

	Phaser.Sprite.call(this, game, x, y, 'character' + characterInt);

	game.physics.arcade.enable(this);

	this.ballIsAnimated = this.animations.validateFrames([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]);
	if (this.ballIsAnimated) {
		this.animations.add('idle', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11], 10, false);
		this.animations.add('tapnimation', [12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23], 24, false);
		this.animations.play('idle');
	}

	this.anchor.setTo(0.5, 0.5);
	this.body.bounce.setTo(0.5, 0.5);

	// Add title text.
	var style = {
		font: "32px Arial",
		fill: "#ff0044",
		wordWrap: true,
		wordWrapWidth: this.width,
		align: "center",
		backgroundColor: "#ffff00"
	};

	this.addChild(game.add.text(0, 0, 'Noob', style));
	this.getChildAt(0).anchor.set(0.5, -1.5);

	this.pointsScored = 0;
	this.ballLevelIndex = 0;
	this.ballLevels = [
		'Adept',
		'Apprentice',
		'Squire',
		'Expert',
		'Master',
		'Grand Master',
		'Hero',
		'Legendary',
		'Godlike'
	];
};

BallObject.prototype = Object.create(Phaser.Sprite.prototype);
BallObject.prototype.constructor = BallObject;

BallObject.prototype.ballIsAnimated = false;

BallObject.prototype.practicles = function() {
	if (game.physics.arcade.distanceToPointer(this) < 400) {
		ingameEmitter.x = this.world.x;
		ingameEmitter.y = this.world.y;

		ingameEmitter.start(true, 2000, null, 10);
	}
};

BallObject.prototype.addPointScored = function() {
	this.pointsScored++;

	if (this.pointsScored % 5 == 0) {
		// Increase title.
		// If final title, add extra title.
		var text = this.ballLevels[this.ballLevelIndex];

		if (this.ballLevelIndex >= this.ballLevels.length) {
			this.ballLevelIndex = this.ballLevels.length;
		} else {
			this.ballLevelIndex++;
		}

		this.getChildAt(0).text = text;
	}
};

BallObject.prototype.bounce = function() {
	if (game.physics.arcade.distanceToPointer(this) < 400) {
		if (this.body.allowGravity == false) {
			this.body.allowGravity = true;
		}

		var yVelocity = 0;

		if (this.ballIsAnimated) {
			this.animations.play('tapnimation');
		}

		this.body.angularVelocity = angVelocity = this.x - game.input.activePointer.x;

		if (game.input.activePointer.y > this.y) {
			var amount = 1000;
		} else {
			var amount = 500;
		}

		var force = amount - (Math.pow(-game.physics.arcade.distanceToPointer(this), 2) * 0.005);
		if (force < 0) {
			force = 0;
		}

		if (hardMode == true) {
			this.body.velocity.x += -Math.cos(game.physics.arcade.angleToPointer(this)) * force;
		}

		this.body.velocity.y += -Math.sin(game.physics.arcade.angleToPointer(this)) * force;
	}

	/*
	var yVelocity = 0;

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

		this.body.angularVelocity = angVelocity = this.ball.x - game.input.activePointer.x;

		if (game.input.activePointer.x > this.ball.x) {
			//this.ball.body.velocity.setTo(this.ball.body.velocity.x + -xVelocity, yVelocity);
			this.body.velocity.setTo(game.physics.arcade.distanceToPointer(this));
		} else {
			this.body.velocity.setTo(this.ball.body.velocity.x + xVelocity, yVelocity);
		}
	} else {
		this.body.velocity.setTo(this.ball.body.velocity.x, yVelocity);
	}
	*/
};