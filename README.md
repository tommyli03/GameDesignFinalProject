# SKYHOOK


# Team Members
Thomas Li, Lucas Orquiza, Eric Wang

# Game Summary
Our game is a first-person action-adventure platformer where the player utilizes a grapple hook mechanic to traverse expansive and interconnected environments such as islands in a futuristic setting. We envision the main character of our game to be a cyborg, with one of its arms dedicated to holding the grapple hook gun allowing the player to swing between floating islands and navigate through outdoor sections. The other arm wields a variety of guns that can be swapped between, each with its own functionality and unique playstyle. The game will feature fluid and dynamic movement, with the player utilizing momentum-based swings the grapple hook offers. The game will follow our main character in which he has to traverse the world all while fighting evil cyborgs and parkour to move onto the next section. 

# Genres
- First-Person Platformer/Shooter
- Action-Adventure
- Science Fiction

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
- Players can move with WASD keys, jump with the space key, and also dash by pressing shift. Dashes can be done in any direction but has a cooldown shown by the blue bar underneath health.
- Firing will be done by left-clicking, and aiming will be done by right-clicking
- Players can jump to obstacles (islands, walls, etc.) with the grapple hook arm using E.
- Other than grappling, movement involves running and jumping
- Enemies will fire projectiles that the player needs to dodge in order to stay alive, but can also be shot themselves in order to die. There will be a variety of enemies, including flying ones, larger ones, etc.
- There will be a simple narrative, about 3-4 levels at the end of the last level. We hope to allow players to unlock guns as they play, and when they die, they restart at the beginning.


# Development Plan

**Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)**

Core Gameplay Mechanics:
- ~~Implement Grappling functionality, ensuring that momentum is achieved, only certain terrain can be grappled to, and that perspective isn’t majorly affected by the grappling~~
- ~~Player movement and dashing (ensuring that the player moves correctly and smoothly)~~
- ~~Shooting and ensuring that the projectiles deal damage to targets~~ or terrain (didn't have time to implement terrain damage yet)
- Some form of wall climbing and ledge grabbing (after talking with the team, we decided against implementing this gameplay feature as it didn't make sense thematically)

**Project Part 2: 3D Scenes and Models (Ch 3+4, 10)**

3D Scenes:
- ~~Add destructible terrain (again, we didn't have time to implement terrain damage yet and will do this next iteration)~~
- ~~Add a death screen to start over (eventually this will bring the player back to the beginning of the level)~~
- ~~Create and implement some terrain textures if possible~~

Model:
- ~~Create enemy model assets (we still need to decide on what kinds of enemies we want, but the prefab and functionality are done)~~
- ~~Add an AI script to enemies~~
- ~~Apply meshes to all the assets~~

**Project Part 2 Additions**
- Added multiple guns, including a shotgun and a sniper
- Added some concepts for some enemy subtypes, like the tank and long-range type

**Project Part 3: Visual Effects (Ch 11-13)**

Visual Effects:
- ~~Add more background to the environment, preferably some brush or trees to simulate a forest/jungle environment~~
- ~~Employ more realism by using shaders and developing lighting and shadows~~
- ~~Add some kind of explosion visual ~~and knockback~~ for the shotgun~~
- ~~Handle the muzzle effects (smoke or sparks) for each weapon~~ and for enemies (not for enemies yet)
- ~~Add some visual signifier of hitting the enemy with the gun~~

Player Mechanics:
- ~~Make dashing more smooth and employ some kind of animation for it~~
- ~~Adjust movement speed until it feels smooth and playable~~

Guns:
- Add new weapons (~4) and then cull after discussion down to around 5 at most (we decided to lessen the priority of this)
- ~~Change bullet shape/look for greater realism~~
- ~~Add some knockback on the player to the shotgun for more impact~~

Enemies:
- ~~Find appropriate models/meshes for the enemies~~
- ~~Enhance enemy AI so that they don't just stand in one place while idle, and so they lose sight of player after we duck behind a wall~~
- ~~Add some knockback on the enemies to the shotgun for more impact~~

**Project Part 3 Additions**
- Added player health, we can now take damage and die, thus restarting the game
- Added animation for destructible terrain

**Project Part 4: Sound, UI, and Animation (Ch 14, 15, 17)**

Sound:
- ~~Add a sound when the gun is fired, ideally a different sound for each gun~~
- ~~Add a sound when an enemy is hit~~
- ~~Add some background music (soft instrument)~~
- ~~Add a "whoosh" when dashing or jumping~~
- ~~Add a sound when walking~~

UI:
- ~~Health Bar (position to be determined)~~
- Bullets Remaining // Decided on using infinite bullets instead
- ~~Red glare/vignette when on low health~~
- Counter for Number of Enemies Alive // Decided against tracking the number of enemies
- ~~Dash Cooldown Bar~~

Animation:
- ~~Walking animation~~
- ~~Enemy animation~~
- ~~Gun Firing animation (like rotation for recoil)~~

**Project Part 4 Additions**
- Finished up some of the Postprocessing
- Fixed the weapon flickering by switching to using number keys
- Pausing the game when in the death screen
- Fixed enemy meshes
- Added more assets for realism
- Added more islands

**Project Part 4 Final**

Background/Terrain:
- ~~Add additional islands~~
- ~~Add new enemy assets for additional archetypes~~ 
- ~~Any additional sounds needed~~

Gameplay:
- ~~Refining player movement by making it feel smoother~~
- ~~Refining weapons~~
- Adding Rocket Launcher // Decided that this was a lower priority over improving the gameplay loop
- Adding Melee Weapon    // Decided that this was a lower priority over improving the gameplay loop

Goals/Rewards:
- ~~Add rewards for killing enemies (health and bullets)~~
- ~~Vary rewards for different enemy archetypes~~
- ~~Add more enemies, focus on making each subsequent island more difficult~~
- Add some kind of border to prevent you from leaving without killing enough enemies (?) // Decided against this in the end, more freedom

**Project Part 4: Finishing Touches**
- Added visible health bars for enemies
- Improved Sniper and other gun function and playability
- Added a new grappling hook asset
- Improved the UI
- Added health packs

**Final Project Submission**

Clean Up:
Since most of the functionality is done, we just need to do the following clean-up:
- ~~Edge testing bugs~~
- ~~Win Screen upon killing all enemies~~
- ~~Improve enemy AI~~


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

**Project Part 2: 3D Scenes and Models (Ch 3+4, 10)**

Our work for this deliverable consists of these 3 mains components:
- Enemies & Shooting
- Player Movement & Grappling
- Terrain & Meshing

Enemies & Shooting:
We implemented the enemy AI and shooting. The enemies stand idle until the player enters their sight of range. After that, they should chase the player and shoot at them. They should be following the player with their gaze. We also added multiple enemy types- one tank, which is larger and has more health and one sniper, which has a thinner + longer gun and has a greater vision range. Furthermore, 2 more guns were added- the sniper and the shotgun. Assets for these two are shown below, and while the sniper has a faster bullet, it has a lower fire rate. On the other hand, the shotgun fires out multiple pellets in a cone.

Player Movement & Grappling:
The movement was tweaked so that there are no more infinite jumps. Instead, we have an adjustable number of maximum jumps (we'll set this to 2 or 3, not decided yet), where you then can't jump again until you hit the ground. The dash now is more fluid and is an actual dash instead of just a teleporting blink. It also travels a further distance. We also made the grappling more fluid, instead of simply just a teleport.

Terrain & Meshing:
Firstly, more terrain including the moving platform were added. Furthermore, floating islands were added, the player meshing was completed, and all the gun meshing was done. Here are the assets that we used for this iteration:

Terrain:
https://assetstore.unity.com/packages/3d/props/exterior/rock-package-118182
![Screenshot 2025-03-10 at 11 11 46 PM](https://github.com/user-attachments/assets/92e14188-a87c-4de2-bcec-4beeabe711ca)

Trees:
https://assetstore.unity.com/packages/3d/vegetation/big-poplar-tree-free-301037
https://assetstore.unity.com/packages/3d/vegetation/big-oak-tree-free-279431

![Screenshot 2025-03-10 at 11 09 13 PM](https://github.com/user-attachments/assets/07c39b00-3d13-4ce5-8f32-d569f9cce8ae)

Spartan:
https://sketchfab.com/3d-models/spartan-armour-mkv-halo-reach-57070b2fd9ff472c8988e76d8c5cbe66
![Screenshot 2025-03-10 at 10 41 02 PM](https://github.com/user-attachments/assets/1b90db44-6d86-42be-ab9d-12a9d66654fa)

Assault Rifle:
https://assetstore.unity.com/packages/3d/props/guns/sci-fi-gun-light-87916
![Screenshot 2025-03-10 at 10 44 44 PM](https://github.com/user-attachments/assets/e1895de7-f666-42b9-9e6d-6e5705b918e0)

Shotgun:
https://assetstore.unity.com/packages/3d/props/guns/shotgun-26685
![Screenshot 2025-03-10 at 10 45 02 PM](https://github.com/user-attachments/assets/0468d49b-c329-4c9e-8fcf-1f5f9b9ceffe)

Sniper:
https://assetstore.unity.com/packages/3d/props/weapons/mac-sci-fi-sniper-rifle-188585
![Screenshot 2025-03-10 at 10 45 17 PM](https://github.com/user-attachments/assets/0dee7dca-7b7e-4f90-830b-4ca318106f93)

**Project Part 3: Visual Effects (Ch 11-13)**

Our work for this deliverable consists of these 3 mains components:
- Visual Effects
- Player Mechanics
- Guns
- Enemies

Visual EFfects:
We added more background to the environment, especially brush or tress which allowed us to better simulate a forest. We also used shaders and lighting in order to add more realism. 

Player Mechanics:
WE added health to the player, which means that now, the player can take damage from enemies' bullets and actually die, allowing us to restart the game. We also added an asset for the grappling hook gun.

Guns:
We added muzzle effects to each weapon, but unfortunately not to each enemy. Furthermore we added the crosshair and changed the shooting script so that the bullets would travel torwards where the crosshair was pointed.

Enemies:
We created terrain that could be destroyed. This allows for players to shoot the terrain and slowly break it. The durability of the wall (number of bullets required to break it) is adjustable. We also made the bullets more visible so that players can dodge more easily.

Destructible Terrain:
[https://assetstore.unity.com/packages/3d/props/exterior/rock-package-118182](https://assetstore.unity.com/packages/3d/environments/destructible-wall-generator-18143)
![Screenshot 2025-04-02 at 11 37 14 PM](https://github.com/user-attachments/assets/4ab6cbdf-5b78-43a9-afaa-67f46c3a7636)

Muzzle Flash:
![Screenshot 2025-04-02 at 11 53 25 PM (2)](https://github.com/user-attachments/assets/35bbb69e-2896-43ef-94ed-73f77c007be2)

**Project Part 4: Sound, UI, and Animation (Ch 14, 15, 17)**

Our work for this deliverable consists of these 3 main components: 
- Sound
- UI
- Animation

Sound:
For each of the 7 sounds we created we've added them to a folder called "Sound Tests". Each one is a short, 5 to 15 second video displaying the sound.
They are labeled appropriately as: "Background_Music", "Default_Gun_Shooting", "Shotgun_Shooting", "Walking", "Dashing", "Enemy_Firing", and "Hitting_Enemy".
Background_Music:

[▶️ Watch Background Music Demo](Sound%20Tests/Background_Music.mov)

Default_Gun_Shooting: 

https://github.com/user-attachments/assets/f079f6d2-2eac-4680-8343-ebf5697ee8ad

Shotgun_Shooting: 

[▶️ Watch Shotgun Shooting Demo](Sound%20Tests/Shotgun_Shooting.mov)

Walking: 

[▶️ Watch Walking Demo](Sound%20Tests/Walking.mov)

Dashing: 

https://github.com/user-attachments/assets/ed38f21b-c26d-4783-b4c5-c8edcd6c6593

Enemy_Firing: 

[▶️ Watch Enemy Firing Demo](Sound%20Tests/Enemy_Firing.mov)

Hitting_Enemy: 

[▶️ Watch Hitting Enemy Demo](Sound%20Tests/Hitting_Enemy.mov)

These are the assets used for the sound:
1. Default Gun and Shotgun Shooting: https://assetstore.unity.com/packages/audio/sound-fx/shooting-sound-177096
2. Walking: https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879
3. Dashing: https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116#content
4. Enemy Firing and hitting Enemy: https://assetstore.unity.com/packages/audio/sound-fx/heavy-8-bit-explosions-sounds-289649
5. Background Music: https://assetstore.unity.com/packages/audio/music/space-threat-free-action-music-205935

UI:
We focused on three parts of the UI for this iteration- the Health Bar, the Stamina Bar, and the Gun Changing.
Health_Bar: 

[▶️ Watch Health Bar Demo](UI%20Tests/Health_Bar.mov)

Stamina_Bar: 

[▶️ Watch Stamina Bar Demo](UI%20Tests/Stamina_Bar.mov)

Gun_Changing:

[▶️ Watch Gun Changing Demo](UI%20Tests/Gun_Changing.mov)

These are the assets used for the UI:
1. https://assetstore.unity.com/search#q=GUI&nf-ec_price_filter=0...0

Animation:
We were able to get tree and grass animations correctly.
Tree animation: 

[▶️ Watch Tree Swaying Demo](Animation%20Test/Tree_Swaying.mov)

Grass animation: 

https://github.com/user-attachments/assets/6a651406-7245-415d-ae2f-5714bcdbfec9

These are the assets used for the animation:
1. https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-free-medieval-fantasy-series-187052

**Project Part 4: Finishing Touches**

Our work for this deliverable consists of these 3 main components:
- Background/Terrain
- Enemies
- Gameplay
- Goals/Rewards

Background/Terrain:
We added muliple islands, then populated each island with additional enemies to increase the challenge and engagement. We've also made the game music a little louder.

Enemies:
We enhanced the enemy AI behavior for smarter, more dynamic encounters. We also added different kinds of enemy archetypes. We also imported new animations for enemy robots. We put health bars on top of enemies to showcase how low health they are.

New Enemy Animation:
[▶️ Watch New Enemy Animation Demo](Animation%20Test/New_Enemy_Animation.mov)

Enemy Health Bar:
[▶️ Watch Enemy Health Bar Demo](UI%20Tests/Enemy_Health_Bar.mov)


Gameplay:
We tweaked bullet and gun mechanics for smoother gameplay; this includes fixing the sniper for better accuracy and reliability. We also fixed the bug to the restart screen. Finally, we removed the grappling hook asset and fixed the UI issues.


Goals/Rewards:
We were able to make it so that killing enemies makes them drop health packs that you can pick up to increase your health. We added a health pack to enemy drops in order to reward players. Finally, we added a new cutscene at the beginning of the game to give an overall objective to the game.

Cutscene at the Beginning of the Game:
[▶️ Watch Cutscene Demo](Animation%20Test/Cutscene.mov)

Health Packs:
[▶️ Watch Health Packs Demo](Animation%20Test/Health_Pack.mov)


**Final Project Submission**
Our work included touching up a few things. We added a new grappling hook asset.

New Grappling Hook Asset:
![Grappling_Gun](https://github.com/user-attachments/assets/973f8cbc-8bf2-4289-bfca-f2b0dea3c935)

Furthermore, we added the Zoom effect. The Zoom effect works only on the assault rifle and the sniper. 
For the Sniper, zooming slows down time by 90% in order to aim.
For the Assault Rifle, zooming focuses the line of fire.

Zooming Video:
[▶️ Watch Zooming Demo](UI%20Tests/Zoom.mov)

We made it so that Sniper is not available at the beginning of the game and instead only unlockable after killing a specific enemy.

Unlocking Sniper:
[▶️ Watch Unlocking Sniper Demo](UI%20Tests/Unlock_Sniper.mov)

Also, we added 'Ramiels', or checkpoints where the player can grapple onto. They serve as a way to traverse across to the other islands.
<img width="258" alt="Ramiel" src="https://github.com/user-attachments/assets/a2709304-7e59-4593-b85b-07f27efe4f55" />




# Instructions for Testing the Project

- General 2D Movement: WASD
- Jump: Space Bar (can be spammed up to 5 times)
- Dash: Shift (cooldown between dashes, but can also be spammed)
- Grappling: E
- Swap between Guns: Mouse Scroll (up or down) or clicking 1, 2, 3
- Assault Rifle (Default Gun): Left-click (can be held down)
- Shotgun: Left-click (cannot be held down, must manually re-click to fire again)
- Sniper: Left-click (can be held down)
- Zooming/Aiming: Right-click

# Demo

[![Video Title](https://img.youtube.com/vi/bOJq_Z83Hoc/0.jpg)](https://www.youtube.com/watch?v=bOJq_Z83Hoc)


# Download

Here is the itch.io link to our game containing the WebGL version:
https://tommyli03.itch.io/skyhook 

# Future Work

If we had more time, we would add:
- More enemies
- More islands with enemies
- A Final Boss
- More weapons, including a melee weapon and a rocket launcher

# Member Contributions

Eric:

Lucas:

Tommy:
