# Project Maf

### Student Info

-   Name: Maffie Cohen
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

**Objective:** Car tries to drive on the freeway even though the ramp is closed.

#### Steering Behaviors

- Seek - Nearest freeway ramp
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- This car is sufficiently far from any given freeway ramp
- This car is bored (has driven too long without a goal)
   
### Depression (realized the freeway was inaccessible)

**Objective:** Car drives away from the freeway, enlightened with the knowledge that the freeway ramp is closed.

#### Steering Behaviors

- Flee - The targeted freeway ramp
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- This car is sufficiently close to it's freeway ramp target

## STUDENT DRIVER STUPID DUMB STUDENT DRIVER UH OH STUDENT DRIVER

DRIVES ERRATICALLY AND IS STUPID AND AVOIDS ALL FREEWAY RAMPS BECAUSE IT'S TOO SCARED AND DUMB.

### PANIC (panic)

**Objective:** SURVIVE

#### Steering Behaviors

- Flee - ALL CARS
- Obstacles - ALL CARS
- Separation - ALL CARS
   
#### State Transistions

- Gets too close to another car
- Gets too close to the freeway
   
### anxiety ._.

**Objective:** wander around and don't get hit

#### Steering Behaviors

- Wanders
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- calms down (panics for sufficiently long)

## Sources

- me i made it its all mine i made everything im the best its mine

## Make it Your Own

- i drew all the sprites
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

