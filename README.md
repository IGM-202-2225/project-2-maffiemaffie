# Project Mateo

### Student Info

-   Name: Mateo Ramos
-   Section: 01

## Simulation Design

It has a bunch of cars they do stuff (usually car-related stuff but not exclusively)

### Controls

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_

## Normal (boring) Car

Drives. Sucks. Isn't cool. No one likes it. Isn't even quirky.

### Stupid freeway car

**Objective:** Car tries to drive on the freeway even though the ramp is blocked off.

#### Steering Behaviors

- Seek - Nearest freeway ramp
- Obstacles - Traffic cones
- Seperation - Other cars
   
#### State Transistions

- This car is sufficiently far from any given freeway ramp
- This car is bored (has driven too long without a goal)
   
### Depression (realized the freeway was inaccessible)

**Objective:** Car drives away from the freeway with the knowledge that the freeway ramp is closed.

#### Steering Behaviors

- Flee - the targeted freeway ramp
- Obstacles - Traffic cones
- Seperation - Other cars
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## _Agent 2 Name_

_A brief explanation of this agent._

### _State 1 Name_

**Objective:** _A brief explanation of this state's objective._

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   
### _State 2 Name_

**Objective:** _A brief explanation of this state's objective._

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

