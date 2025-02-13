# Game Design Final Project


# Team Members
Thomas Li, Lucas Orquiza, Eric Wang

# Game Summary
Our game is a first-person action-adventure platformer where the player utilizes a grapple hook mechanic to traverse expansive and interconnected environments such as islands in a futuristic setting. We envision the main character of our game to be a cyborg, with one of its arms dedicated to deploying the grapple hook allowing the player to swing between floating islands, climb walls, and navigate through indoor and outdoor sections. The other arm can be launched out as a weapon to hit targets, solve puzzles that involve interacting with the environment, and fight enemies. The game will feature fluid and dynamic movement, with the player utilizing momentum-based swings the grapple hook offers. The game will follow our main character in which he has to traverse the world all while fighting evil cyborgs, parkour, and solving puzzles to move onto the next section. 

# Genres
- First-Person Platformer/Shooter
- Action-Adventure
- Science Fiction
- Puzzle

# Inspiration

**Echo Point Nova**

![image](https://github.com/user-attachments/assets/318b87cc-bf4f-44c1-a8b6-65f2c810163c)

https://www.youtube.com/watch?v=kL7dc5mrw1I

EPN is described as a cross between Doom and Titanfall 2, and is a mobility based shooter which gives the player a lot of freedom with how to traverse the environment. The player uses fast-paced fluid movement options of a frictionless hoverboard, grappling, and a double jump while facing off against enemies. The game is set on a floating archipelago and gives the player freedom on where to go next: they’re all a few jumps away. Our game intends to use some of the movement types seen in EPN, and will have a similar gameplay pace. Level design might also bear similarities.

**A Story About My Uncle**

![image](https://github.com/user-attachments/assets/89415d58-7b96-46c0-a7f1-e3545a359a1a)

https://www.youtube.com/watch?v=Aug1M1_DhNc

A Story About My Uncle is a narrative-driven first-person platforming game with a focus on first-person swinging mechanics. It emphasizes player precision/timing with the electromagnetic grapple hook, making it a valuable reference for refining our own grapple-based movement. The futuristic/outdoor/Jurassic design of levels also align with what we envision for our own environment. 

# Gameplay
- Players can move with WASD keys, jump with the space key, and also dash by pressing a movement key twice in quick succession. Dashes can be done in any direction (such as double jumping when clicking space twice) but have a cooldown.
- Players can swing between obstacles (islands, walls, etc.) with the grapple hook arm using right click on the mouse. Players can grapple to specified locations on the terrain denoted by some symbol.
- Other than grappling, movement involves running, jumping, and wall-running
- There will be two kinds of terrain: interactable (buttons to shoot that move certain parts, able to be walked on, or able to be grappled to) and non-interactable (background)
- Enemies will fire projectiles that the player needs to dodge in order to stay alive, but can also be shot themselves in order to die. There will be a variety of enemies, including flying ones, larger ones, etc.

# Development Plan

**Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)**

Core Gameplay Mechanics:
- Implement Grappling functionality, ensuring that momentum is achieved, only certain terrain can be grappled to, and that perspective isn’t majorly affected by the grappling
- Player movement and dashing (ensuring that the player moves correctly and smoothly)
- Shooting and ensuring that the projectiles deal damage to targets or terrain
- Some form of wall climbing and ledge grabbing
