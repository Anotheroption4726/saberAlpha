# _ SABER ALPHA _

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
Open the Project (requires **Unity 2019.4.4f1** or more). If *'TestScene'* isn't opened on start up, go to **/Assets/Scenes** and open *'TestScene'*. Once you are in *'TestScene'*, go to the **Hierarchy** window (usually the left side panel), select the *'SceneManager'* **GameObject** and navigate to its *'Game Parameters Script'* **Component** (usually on the right side panel). Here you will find a series of variables that you'll be able tweak in order to test the game.

##### Warnings:
	!!! You should only work within the scope of the 'Game 
	Parameters Script'. Do not change anything else in the
	project outside of that Component.

	!!! Changes made to the scene will only be applied once 
	it is saved AND if you are using the 'Test Game' mode. 
	Remember to work on your own git branch.

### GAME VARIABLES

##### Game Type:
	- Normal Game: Regular game using the original default 
	  parameter values.
	- Test Game: Test game using the modified parameter 
	  values.

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
	- Increase: 
	- Decrease: 

##### Character Friction:
	- Default Value: 4
	- Increase: 
	- Decrease: 

### CHARACTER VARIABLES

##### Run Movement Speed_fixed :
	- Default Value: 40
	- Increase: Character runs faster
	- Decrease: Character runs slower

##### Run Stop Slide Time_timer :
	- Default Value: 0.13
	- Increase: 
	- Decrease: 

##### Idle Jump Vertical Force_impulse :
	- Default Value: 1200
	- Increase: Character jumps higher (applies to Forward Jump as well)
	- Decrease: Character jumps lower (applies to Forward Jump as well)

##### Idle Jump Movement Speed_fixed :
	- Default Value: 10
	- Increase: 
	- Decrease: 

##### Forward Jump Horizontal Force_impulse :
	- Default Value: 250
	- Increase: Character jumps further (doesn't apply to Wall Jump)
	- Decrease: Character jumps closer (doesn't apply to Wall Jump)

##### Forward Jump Stop Slide Force_impulse :
	- Default Value: 1500
	- Increase: 
	- Decrease: 

##### Forward Jump Horizontal Air Drag_ratio :
	- Default Value: 0.97
	- Increase: 
	- Decrease: 

##### Fall Max Speed Velocity Value_threshold :
	- Default Value: 60
	- Increase: 
	- Decrease: 

##### On The Ground Duration_timer :
	- Default Value: 2
	- Increase: 
	- Decrease: 

##### On The Ground Stand Up Time_timer :
	- Default Value: 0.5
	- Increase: 
	- Decrease: 

##### Crawl Movement Speed_fixed :
	- Default Value: 10
	- Increase: Character moves faster while crawling
	- Decrease: Character moves slower while crawling

##### Run Slide Horizontal Force_impulse :
	- Default Value: 2500
	- Increase: 
	- Decrease: 

##### Run Slide Start Time_timer :
	- Default Value: 0.75
	- Increase: 
	- Decrease: 

##### Run Slide Duration_timer :
	- Default Value: 0.25
	- Increase: 
	- Decrease: 

##### Wall Slide Hold Gravity_ratio :
	- Default Value: 0.125
	- Increase: 
	- Decrease: 

##### Wall Jump Vertical Force_impulse :
	- Default Value: 800
	- Increase: Character jumps higher if Wall Sliding
	- Decrease: Character jumps lower if Wall Sliding

##### Wall Jump Horizontal Force_impulse :
	- Default Value: 1500
	- Increase: Character jumps further if Wall Sliding
	- Decrease: Character jumps closer if Wall Sliding 

##### Wall Jump Restrain Duration_timer :
	- Default Value: 0.25
	- Increase: 
	- Decrease: 