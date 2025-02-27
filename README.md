# SKYHOOK


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

**Doom Eternal**

![image](https://github.com/user-attachments/assets/64563437-242b-4a6c-b25e-4019fa0acc6f)

https://www.youtube.com/watch?v=QofAfUFVA_Y

Doom Eternal is a first-person platforming shooter with a focus on blasting enemies. It's less precise but more satisfying as chunks of the enemy literally shred off after shooting them. It has excellent graphics and a straightforward gameplay loop, just like what we intend for our game. The environment is also very dynamic and the enemies and weapon choices creative.

**Mirror's Edge Catalyst**

![image](https://github.com/user-attachments/assets/91b799fa-848d-42c0-a045-46917caf57f0)

https://www.youtube.com/watch?v=RHE60g2e4FI

Mirror's Edge Catalyst is a first-person action-platformer that allows for fluid parkour and traversal. It focuses mostly on momentum, precise timing, and includes many of the movement options we hope to implement, such as wall-running, running, and grappling. Furthermore, the futuristic city background is very appealing and is a backdrop that we're currently considering for our game.


# Gameplay
- Players can move with WASD keys, jump with the space key, and also dash by pressing a movement key twice in quick succession. Dashes can be done in any direction (such as double jumping when clicking space twice) but have a cooldown.
- Players can swing between obstacles (islands, walls, etc.) with the grapple hook arm using right click on the mouse. Players can grapple to specified locations on the terrain denoted by some symbol.
- Other than grappling, movement involves running, jumping, and wall-running
- There will be two kinds of terrain: interactable (buttons to shoot that move certain parts, able to be walked on, or able to be grappled to) and non-interactable (background)
- Enemies will fire projectiles that the player needs to dodge in order to stay alive, but can also be shot themselves in order to die. There will be a variety of enemies, including flying ones, larger ones, etc.
- There will be a simple narrative, about 3-4 levels, and a final boss at the end of the last level. We hope to allow players to unlock guns as they play, and when they die, they restart at the beginning of the level, allowing for levels to be checkpoints.
- We're also planning on making the final boss very large and giving the player options to traverse the surface of the boss while attacking weakpoints, similar to the fight with the Titans from God of War 3.

# Development Plan

**Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)**

Core Gameplay Mechanics:
- Implement Grappling functionality, ensuring that momentum is achieved, only certain terrain can be grappled to, and that perspective isn’t majorly affected by the grappling
- Player movement and dashing (ensuring that the player moves correctly and smoothly)
- Shooting and ensuring that the projectiles deal damage to targets or terrain
- Some form of wall climbing and ledge grabbing

**Project Part 2: 3D Scenes and Models (Ch 3+4, 10)**

3D Scenes:
- Add more terrain textures and even destructible terrain
- Add a death screen to start over (eventually this will bring the player back to the beginning of the level)
- Create and implement some terrain textures if possible

Model:
- Create enemy model assets
- Add an AI script to enemies
- Apply meshes to all the assets

# Development

**Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)**

Our work for this deliverable consists of these 3 mains components:
- Basic movement on terrain with a First-person viewpoint
- Basic shooting
- Basic grappling

Basic movement on terrain with a First-person viewpoint:
The game is implemented as a Unity scene with a simple terrain consisting of the ground and a wall. The player has a first-person viewpoint where they see the bottom half of the cylinder as well as a gun on the right arm. The player can move around with the WASD keys and jump with the Space bar. We've also implemented a double-jump as a beginning part of our dash mechanic.

Basic shooting:
Shooting is done through a Shoot Point in the Weapon, attached to the player via their "right arm". We have plans to potentially change this if we want to do grappling with our right arm and shooting with our left. Bullets come out upon left-clicking and collide with terrain. The bullets currently don't do anything else and we haven't made them done damage yet.

Basic grappling:
Basic grappling is done by launching a raycast, and when it hits something, it teleports immediately. We will replace this with jump and basic momentum. The grappling shows you a red line in order to show the path of grappling. We also freeze the player momentarily while grappling.
