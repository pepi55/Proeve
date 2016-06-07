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