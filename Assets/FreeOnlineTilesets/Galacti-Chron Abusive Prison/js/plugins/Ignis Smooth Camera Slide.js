//=============================================================================
// Ignis Smooth Camera Slide.js
//=============================================================================

/*:
 * @plugindesc Adds a smooth camera sliding when moving
 * @author Raizen
 *
 * @help This plugin does not provide plugin commands.
 *
  * @param Scroll X
 * @desc The speed in which the camera will move in the X axis (Higher = slower)
 * @default 20
  * @param Scroll Y
 * @desc The speed in which the camera will move in the Y axis (Higher = slower)
 * @default 20
 */


//(function() {
(function() {
var ignisParameters = PluginManager.parameters('Ignis Smooth Camera Slide');
 var ignisScrollX = parseInt(ignisParameters['Scroll X'] || 20);
 var ignisScrollY = parseInt(ignisParameters['Scroll Y'] || 20);
Game_Player.prototype.updateScroll = function(lastScrolledX, lastScrolledY) { 
    var x1 = lastScrolledX;
    var y1 = lastScrolledY;
    var x2 = this.scrolledX();
    var y2 = this.scrolledY();
	console.log(x2);
    if (y2 > this.centerY()) {
        $gameMap.scrollDown((y2 - this.centerY())/ignisScrollY);
    }
    if (x2 < this.centerX()) {
        $gameMap.scrollLeft((this.centerX() - x2)/ignisScrollX);
    }
    if (x2 > this.centerX()) {
        $gameMap.scrollRight((x2 - this.centerX())/ignisScrollX);
    }
    if (y2 < this.centerY()) {
        $gameMap.scrollUp((this.centerY() - y2)/ignisScrollY);
    }
};}

	
)();

