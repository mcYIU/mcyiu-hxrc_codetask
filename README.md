# mcyiu-hxrc_codetask
 
I referred to the original model and added 3 types of obstacles: 
The first one is a direct copy of the original rotating circular obstacle, with stars placed inside it. 
The second one replaces the triangle with a rotating square, containing a color switch that alters the player's color, making it unpredictable to jump out of the obstacle (or luckily pass through it directly). 
The third one generates a series of rotating cross-shaped obstacles moving from the right side of the screen to the left. Due to the time difference, each cross-shaped obstacle reaches the center of the screen at different angles, requiring early judgment of the obstacle's state after movement and rotation (I've only succeeded once). 
Moreover, in the new model, the color sequence of the obstacles is randomly arranged in each start to prevent players from memorizing the game rhythm.

If I could add more gameplay elements, I would include a shooting function. 
I would add a "Shoot" button in the bottom left corner of the screen and turn the "Jump" input into a button placed in the bottom right corner (both independent buttons can be pressed simultaneously). 
When the shoot button is pressed, a small dot would be generated from the player's circle towards the +y axis, and holding the button would enable continuous shooting. 
The small dots would only react when hitting obstacles: 
If they hit an obstacle block of a different color from the player, that entire block would be cleared, reducing the risk of the player jumping onto an incorrect obstacle. 
However, if the dot hits an obstacle of the same color as the player, it would result in an immediate loss, so the player cannot shoot recklessly.