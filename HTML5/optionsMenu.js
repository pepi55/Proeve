// Options state.


var optionsState = {
  create: function() {
    var reso2048x1536;
		reso2048x1536 = game.add.button(game.world.centerX, game.world.centerY, 'button', function() {
		this.scale.setGameSize(2048, 1536);
		}, this);
		reso2048x1536.anchor.setTo(0.5, 0.5);

		var reso1920x1080;
		reso1920x1080 = game.add.button(game.world.centerX, game.world.centerY + 100, 'button', function() {
				this.scale.setGameSize(1920, 1080);
		}, this);
		reso1920x1080.anchor.setTo(0.5, 0.5);

		var reso1366x768;
		reso1366x768 = game.add.button(game.world.centerX, game.world.centerY + 200, 'button', function() {
				this.scale.setGameSize(1366, 768);
		}, this);
		reso1366x768.anchor.setTo(0.5, 0.5);

			var backButton;
		backButton = game.add.button(50, 50, 'button', function() {
		game.state.start('mainMenu');
		}, this);
		backButton.anchor.setTo(0.5, 0.5);
  },
};