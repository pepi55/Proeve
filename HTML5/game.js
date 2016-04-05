// Game state.
var gameState = {
    preload: function() {
        game.load.image('ball', 'assets/image.png');
    },

    create: function() {
        game.stage.backgroundColor = '#CCCCCC';
        game.physics.startSystem(Phaser.Physics.ARCADE);
        game.physics.arcade.gravity.y = 1000;

        // Ball setup
        this.ball = game.add.sprite(game.world.centerX, 20, 'ball');
        this.ball.anchor.setTo(0.5, 0.5);

        game.physics.arcade.enable(this.ball);
        this.ball.body.collideWorldBounds = true;
        this.ball.body.bounce.setTo(0.9, 0.9);

        var escButton = game.input.keyboard.addKey(Phaser.Keyboard.Q);
        escButton.onDown.add(this.goToMain, this);

        game.input.onDown.add(this.bounce, this);

        //var spaceButton = game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);
        //spaceButton.onDown.add(this.bounce, this);
    },

    update: function() {
    },

    // Custom functions
    bounce: function() {
        var yVelocity = 0;

        if (game.input.activePointer.y > this.ball.y) {
            yVelocity = -400;
        } else {
            yVelocity = 400;
        }

        if (game.input.activePointer.x > this.ball.x) {
            console.log('Bounce Left');

            this.ball.body.velocity.setTo(this.ball.body.velocity.x + -Phaser.Math.difference(game.input.activePointer.x, this.ball.x), yVelocity);
        } else {
            console.log('Bounce Right');

            this.ball.body.velocity.setTo(this.ball.body.velocity.x + Phaser.Math.difference(game.input.activePointer.x, this.ball.x), yVelocity);
        }
    },

    restartGame: function() {
    },

    goToMain: function() {
        game.state.start('mainMenu');
    },
};