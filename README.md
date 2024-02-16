# Project Maf

### Student Info

-   Name: Maffie Cohen
-   Section: 01

## Simulation Design

It has a bunch of cars they do stuff (usually car-related stuff but not exclusively)

### Controls

-   Place a traffic cone by clicking in the game window.

## Normal (boring) Car \[SUV\]

Drives. Sucks. Isn't cool. No one likes it. Isn't even quirky.

### Stupid freeway car \[yellow\]

**Objective:** Car tries to drive on the freeway even though the ramp is closed.

#### Steering Behaviors

- Seek - Random freeway ramp
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- This car is sufficiently far from its target freeway ramp
- This car is bored (has driven too long without a goal)
   
### Depression (realized the freeway was inaccessible) \[green\]

**Objective:** Car drives away from the freeway, enlightened with the knowledge that the freeway ramp is closed.

#### Steering Behaviors

- Flee - The targeted freeway ramp
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- This car is sufficiently close to it's freeway ramp target

## STUDENT DRIVER STUPID DUMB STUDENT DRIVER UH OH STUDENT DRIVER \[sedan\]

DRIVES ERRATICALLY AND IS STUPID AND AVOIDS ALL FREEWAY RAMPS BECAUSE IT'S TOO SCARED AND DUMB.

### PANIC (panic) \[purple\]

**Objective:** SURVIVE

#### Steering Behaviors

- Flee - ALL CARS
- Obstacles - ALL CARS
- Separation - ALL CARS
   
#### State Transistions

- Gets too close to another car
- Gets too close to a freeway ramp
   
### anxiety ._. \[blue\]

**Objective:** blend in and dont get hit

#### Steering Behaviors

- Coheses - Other cars 
- Obstacles - Traffic cones
- Separation - Other cars
   
#### State Transistions

- calms down (panics for sufficiently long)
- calms down (is sufficiently far from other cars)

## Sources

- me i made it its all mine i made everything im the best its mine

## Make it Your Own

- i did all the art

## Known Issues

Cars go through obstacles or off the edge a lot.
They're jittery a lot.

### Requirements not completed

i did all of it baybeee

:)
