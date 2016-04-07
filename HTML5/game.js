// Game state.
var walls;
var tweenAPosition;
var tweenBPosition;

var gameState = {
    preload: function() {
        game.load.image('ball', 'assets/image.png');
        game.load.image('goal', 'assets/image.png');

        game.load.image('wall1', 'assets/image.png');
        game.load.image('wall2', 'assets/image.png');

        // this has to be active for the fps to be counting.
        game.time.advancedTiming = true;
    },

    create: function() {
        game.stage.backgroundColor = '#CCCCCC';
        game.physics.startSystem(Phaser.Physics.ARCADE);
        game.physics.arcade.gravity.y = 1000;

        var tweenA;
        var tweenB;

        tweenAPosition = 750;
        tweenBPosition = 70;
        // Add gameobjects to game
        this.ball = game.add.sprite(game.world.centerX, 20, 'ball');
        this.goal = game.add.sprite(tweenAPosition, game.world.height - 50, 'goal');

        this.wall1 = game.add.sprite(0, game.world.height, 'wall1');
        this.wall2 = game.add.sprite(game.world.width, game.world.height, 'wall2');

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
        this.wall1.scale.setTo(1, 20);
        this.wall2.scale.setTo(1, 20);

        this.wall1.body.immovable = true;
        this.wall2.body.immovable = true;
        this.wall1.body.allowGravity = false;
        this.wall2.body.allowGravity = false;

        walls = game.add.group();

        walls.add(this.wall1);
        walls.add(this.wall2);

        var escKey = game.input.keyboard.addKey(Phaser.Keyboard.Q);
        escKey.onDown.add(this.goToMain, this);

        game.input.onDown.add(this.bounce, this);

        //set the goals tweens.
        tweenA = game.add.tween(this.goal).to({ x: tweenAPosition }, 2500, 'Linear', true, 0);
        tweenB = game.add.tween(this.goal).to({ x: tweenBPosition }, 2500, 'Linear', true, 0);
    },

    update: function() {
        game.physics.arcade.collide(walls, this.ball);
        game.physics.arcade.collide(this.goal, this.ball, this.goalCollisionHandler);

        if (this.ball.world.y >= game.world.height) {
            game.state.start('mainMenu');
        }

        if(this.goal.world.x == tweenAPosition) {
            tweenB.start();
        } else if(this.goal.world.x == tweenBPosition) {
            tweenA.start();
        }
    },

    // Used for rendering debug texts
    render: function() {
        // fps log
        game.debug.text(game.time.fps, 2, 14, "#00ff00");
    },

    // Custom functions
    goalCollisionHandler: function() {
        game.state.start('game');
    },

    bounce: function() {
        var yVelocity = 0;

        if (game.input.activePointer.y > this.ball.y) {
            yVelocity = -400;
        } else {
            yVelocity = 400;
        }

        if (game.input.activePointer.x > this.ball.x) {
            this.ball.body.velocity.setTo(this.ball.body.velocity.x + -Phaser.Math.difference(game.input.activePointer.x, this.ball.x), yVelocity);
        } else {
            this.ball.body.velocity.setTo(this.ball.body.velocity.x + Phaser.Math.difference(game.input.activePointer.x, this.ball.x), yVelocity);
        }
    },

    restartGame: function() {
    },

    goToMain: function() {
        game.state.start('mainMenu');
    },
};