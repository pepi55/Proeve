// Game state.
var walls;

var gameState = {
    preload: function() {
        game.load.image('ball', 'assets/image.png');
        game.load.image('wall1', 'assets/image.png');
        game.load.image('wall2', 'assets/image.png');
    },

    create: function() {
        game.stage.backgroundColor = '#CCCCCC';
        game.physics.startSystem(Phaser.Physics.ARCADE);
        game.physics.arcade.gravity.y = 1000;

        // Add gameobjects to game
        this.ball = game.add.sprite(game.world.centerX, 20, 'ball');
        this.wall1 = game.add.sprite(0, game.world.height, 'wall1');
        this.wall2 = game.add.sprite(game.world.width, game.world.height, 'wall2');

        walls = game.add.group();

        game.physics.arcade.enable([
                this.ball,
                this.wall1,
                this.wall2
            ]);

        // Ball setup
        this.ball.anchor.setTo(0.5, 0.5);
        //this.ball.body.collideWorldBounds = true;
        this.ball.body.bounce.setTo(0.9, 0.9);

        // Walls setup
        this.wall1.anchor.setTo(0.5, 1);
        this.wall2.anchor.setTo(0.5, 1);
        this.wall1.scale.setTo(1, 20);
        this.wall2.scale.setTo(1, 20);

        this.wall1.body.immovable = true;
        this.wall2.body.immovable = true;
        this.wall1.body.allowGravity = false;
        this.wall2.body.allowGravity = false;
        this.wall1.body.enable = true;
        this.wall2.body.enable = true;

        walls.add(this.wall1);
        walls.add(this.wall2);

        var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
        escKey.onDown.add(this.goToMain, this);

        game.input.onDown.add(this.bounce, this);
    },

    update: function() {
        game.physics.arcade.collide(walls, this.ball);
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