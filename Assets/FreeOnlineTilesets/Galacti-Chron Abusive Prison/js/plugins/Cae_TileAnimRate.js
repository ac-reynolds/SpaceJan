//=============================================================================
// Cae_TileAnimRate.js
//=============================================================================

/*:
 * @plugindesc v1.2 - Lets you specify the rate at which animated map tiles cycle their animation. Can also add an on/off switch to the in-game options.
 * @author Caethyril
 *
 * @help Plugin Commands:
 *   None.
 *
 * Compatibility:
 *   Aliases:
 *     Tilemap:        update
 *     Window_Options: addGeneralOptions
 *     ConfigManager:  applyData, makeData
 *   Defines new Boolean property tileAnimRate on the ConfigManager.
 *
 * Terms of use:
 *   Free to use and modify.
 *
 * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
 * Update log:
 *   1.2: Fixed saving/loading in-game animation toggle option value.
 *   1.1: Added on/off switch to the in-game options.
 *   1.0: Initial release.
 *
 * @param Tile Animation Rate
 * @text Tile Animation Rate
 * @type number
 * @min 0
 * @max 60
 * @desc Tile animation rate in frames per second.
 * Default: 2
 * @default 2
 *
 * @param Options Label
 * @text Options Label
 * @type text
 * @desc Text shown for the on/off setting in the in-game options.
 * Leave blank to not add the on/off option.
 * @default
 */

var Imported = Imported || {};			// Import namespace, var can redefine
Imported.Cae_TileAnimRate = 1.2;		// Import declaration

var CAE = CAE || {};				// Author namespace, var can redefine
CAE.TileAnimRate = CAE.TileAnimRate || {};	// Plugin namespace

(function (_) {

'use strict';

	_.params = PluginManager.parameters('Cae_TileAnimRate');			// Process user parameters

	_.rate  = Number(_.params['Tile Animation Rate']) || 2;
	_.label = String(_.params['Options Label']) || '';

	_.active = true;		// Default option value

	// Adjust animationCount prior to the standard +1 per call
	_.Tilemap_update = Tilemap.prototype.update;			// Alias
	Tilemap.prototype.update = function() {
		if (_.active) this.animationCount += _.rate / 2;	// animationCount cuts off at 30 so divide by 2 here
		this.animationCount -= 1;				// Cancel out +1 from default code
		_.Tilemap_update.call(this);				// Callback
	};

	// Add option to ConfigManager
	Object.defineProperty(ConfigManager, 'tileAnimRate', {
		get: function() 	{ return _.active;  },
		set: function(value) 	{ _.active = value; },
		configurable: true
	});

	// Load new flag
	_.ConfigManager_applyData = ConfigManager.applyData;
	ConfigManager.applyData = function(config) {
		_.ConfigManager_applyData.call(this, config);
		if (config.tileAnimRate === undefined) {
			this.tileAnimRate = true;		// Default to true
		} else {
			this.tileAnimRate = this.readFlag(config, 'tileAnimRate');
		}
	};

	// Save new flag
	_.ConfigManager_makeData = ConfigManager.makeData;
	ConfigManager.makeData = function() {
		let config = _.ConfigManager_makeData.call(this);
		config.tileAnimRate = this.tileAnimRate;
		return config;
	};

	// Adds option to Window_Options
	_.Window_Options_addGeneralOptions = Window_Options.prototype.addGeneralOptions;
	Window_Options.prototype.addGeneralOptions = function() {
		_.Window_Options_addGeneralOptions.call(this);
		if (_.label !== '') this.addCommand(_.label, 'tileAnimRate');
	};

})(CAE.TileAnimRate);