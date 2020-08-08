# SABER ALPHA

### CONTROLS

##### Restart Game:
		- Keyboard: R
		- Gamepad: Button 6 (SELECT)

##### Pause Game:
		- Keyboard: ESC
		- Gamepad: Button 7 (START)

##### Move Right:
		- Keyboard: Right Arrow
		- Keyboard: D
		- Gamepad: X Axis Right

##### Move Left:
		- Keyboard: Left Arrow
		- Keyboard: Q
		- Gamepad: X Axis Left

##### Crawl:
		- Keyboard: Down Arrow
		- Keyboard: S
		- Gamepad: Y Axis Down

##### Jump:
		- Keyboard: Space Bar
		- Gamepad: Button 0 (Xbox: A / Playstation: X)

### DEVELOPPER MODE
Open the Project (requires **Unity 2019.4.4f1** or more). If *'TestScene'* isn't opened on start up, go to **/Assets/Scenes** and open *'TestScene'*. Once you are in *'TestScene'*, go to the **Hierarchy** window (usually the left side panel), select the *'SceneManager'* **GameObject** and navigate to its *'Game Parameters Script'* **Component** in the **Inspector** window (usually the right side panel). Here you will find a series of variables that you'll be able tweak in order to test the game.

##### Warnings:
	!!! You should only work within the scope of the 'Game Parameters Script'. Do not change anything else in the project outside of that Component.

	!!! Changes made to the scene will only be applied once it is saved AND if you are using the 'Test Game' mode. Remember to work on your own git branch.

### GAME VARIABLES

##### Game Type:
	- Normal Game: Regular game using the original default parameter values
	- Test Game: Test game using the modified parameter values

##### Time Scale:
	- Default Value: 1
	- Increase: Game runs faster
	- Decrease: Game runs slower

##### Gravity Scale:
	- Default Value: 46.5
	- Increase: Game has earth-like physics
	- Decrease: Game has moon-like physics

##### Platform Friction:
	- Default Value: 4
	- Increase: All characters won't be able to move when colliding with platforms
	- Decrease: All characters will ice skate when colliding with platforms

### CHARACTER VARIABLES

##### Character Friction:
	- Default Value: 4
	- Increase: The character won't be able to move when colliding with platforms
	- Decrease: The character will ice skate when colliding with platforms

##### Run Movement Speed_fixed :
	- Default Value: 40
	- Increase: The character runs faster
	- Decrease: The character runs slower

##### Run Stop Slide Time_timer :
	- Default Value: 0.13 (seconds)
	- Increase: The character will slide during a longer period of time after runing or forward jumping (doesn't change the speed of the animation)
	- Decrease: The character will slide during a shorter period of time after runing or forward jumping (doesn't change the speed of the animation)

##### Idle Jump Vertical Force_impulse :
	- Default Value: 1200
	- Increase: Character jumps higher (vertically, applies to Forward Jump as well)
	- Decrease: Character jumps lower (vertically, applies to Forward Jump as well)

##### Idle Jump Movement Speed_fixed :
	- Default Value: 10
	- Increase: The character moves faster (horizontally) while in mid-air (doesn't affect Forward Jump or Wall Jump speeds)
	- Decrease: The character moves slower (horizontally) while in mid-air (doesn't affect Forward Jump or Wall Jump speeds)

##### Forward Jump Horizontal Force_impulse :
	- Default Value: 250
	- Increase: Character forward jumps further (horizontally, doesn't apply to wall jumps)
	- Decrease: Character forward jumps closer (horizontally, doesn't apply to wall jumps)

##### Forward Jump Stop Slide Force_impulse :
	- Default Value: 1500
	- Increase: The character will slide further (horizontally) while landing on the ground after forward jumping or wall jumping
	- Decrease: The character will slide closer (horizontally) while landing on the ground after forward jumping or wall jumping

##### Forward Jump Horizontal Air Drag_ratio :
	- Default Value: 0.97 (min: 0 - max: 1)
	- Increase: While forward jumping and not holding any movement input, the character will loose less proportional horizontal speed every frame
	- Decrease: While forward jumping and not holding any movement input, the character will loose more proportional horizontal speed every frame

##### Fall Max Speed Velocity Value_threshold :
	- Default Value: 60
	- Increase: 'Fall Maxspeed' animation happens less often. The character will need a higher vertical speed when falling from higher places to trigger the animation state
	- Decrease: 'Fall Maxspeed' animation happens more often. The character will need a lower vertical speed when falling from higher places to trigger the animation state

##### On The Ground Duration_timer :
	- Default Value: 2 (seconds)
	- Increase: After hitting the ground (while in 'Fall Maxspeed' animation state), the character remains lying down for a longer period of time (doesn't change the speed of the animation)
	- Decrease: After hitting the ground (while in 'Fall Maxspeed' animation state), the character remains lying down for a shorter period of time (doesn't change the speed of the animation)

##### On The Ground Stand Up Time_timer :
	- Default Value: 0.5 (seconds)
	- Increase: the duration of the 'Stand Up' animation state (triggered after the 'On The Ground' animation state is over) is longer (doesn't change the speed of the animation)
	- Decrease: the duration of the 'Stand Up' animation state (triggered after the 'On The Ground' animation state is over) is shorter (doesn't change the speed of the animation)

##### Crawl Movement Speed_fixed :
	- Default Value: 10
	- Increase: Character moves faster while crawling
	- Decrease: Character moves slower while crawling

##### Run Slide Horizontal Force_impulse :
	- Default Value: 2500
	- Increase: The character will slide further (horizontally) while doing a run slide
	- Decrease: The character will slide closer (horizontally) while doing a run slide

##### Run Slide Start Time_timer :
	- Default Value: 0.75 (seconds)
	- Increase: Increases the amount of time the player needs to wait (after starting to run) before doing a run slide. Resets after a run slide has happened
	- Decrease: Decreases the amount of time the player needs to wait (after starting to run) before doing a run slide. Resets after a run slide has happened

##### Run Slide Duration_timer :
	- Default Value: 0.25 (seconds)
	- Increase: If triggered, the character will run slide during a longer period of time (doesn't change the speed of the animation)
	- Decrease: If triggered, the character will run slide during a shorter period of time (doesn't change the speed of the animation)

##### Wall Slide Hold Gravity_ratio :
	- Default Value: 0.125
	- Increase: While wall sliding and holding the movement input towards the wall, the character will fall faster towards the ground
	- Decrease: While wall sliding and holding the movement input towards the wall, the character will fall slower towards the ground

##### Wall Jump Vertical Force_impulse :
	- Default Value: 800
	- Increase: Character jumps higher (vertically) if wall sliding
	- Decrease: Character jumps lower (vertically) if Wall Sliding

##### Wall Jump Horizontal Force_impulse :
	- Default Value: 1500
	- Increase: Character jumps further (horizontally) if wall sliding
	- Decrease: Character jumps closer (horizontally) if wall sliding 

##### Wall Jump Restrain Duration_timer :
	- Default Value: 0.25 (seconds)
	- Increase: Increases the amount of time the player needs to wait after doing a wall jump before doing a new one
	- Decrease: Decreases the amount of time the player needs to wait after doing a wall jump before doing a new one