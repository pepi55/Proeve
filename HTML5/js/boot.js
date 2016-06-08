/**
 * @Class
 * @name bootState
 * @desc Loads some things for the loading screen.
 */
var bootState = {
	/** @method
	* @name preload
	* @memberof bootState
	* @description this is a preload function that is fired before the create function, this is used for setting the games scaling and loading a couple of things.
	*/
	preload: function() {
		game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
		if(localStorage.getItem('firstTime') == null)
		{
			localStorage.clear();
			localStorage.setItem('firstTime','yes');
		}
		bootLoadingText = game.add.text(game.world.centerX, 100, "Loading...");
		bootLoadingText.fontSize = 50;
		bootLoadingText.fontWeight = 'bold';
		//	Stroke color and thickness
		bootLoadingText.stroke = '#0020C2';
		bootLoadingText.strokeThickness = 5;
		bootLoadingText.fill = '#2B65EC';
		bootLoadingText.anchor.setTo(0.5);
		game.load.json('characters', 'json/characters.json');
		game.load.spritesheet('loadingScreenSheet', 'assets/UI/sheetScreenswitch.png', 1005, 900);
	},

	/** @method
	 * @name create
	 * @memberof bootState
	 * @description Create loads the load state after the preload function is done loading everything nessecary for the load state.
	 */
	create: function() {
		game.state.start('load');
	}
};