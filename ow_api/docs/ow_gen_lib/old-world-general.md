# Old world general

## Goal
Create a library that supports the other old world projects, specifically providing general information about the game that other projects will need regarding game mechanics.
 
## Summary

## Design

### Dice 

#### Dice types
Dice types - All dice are:
- d6 - The most frequently used dice is a regular six-sided dice, marked 1 to 6..
- d3 - representing the six possibilities 1 - 3 each of equal chance.
- da - Artillary dice six sides with the possible results marked 2, 4, 6, 8, 10 and Misfire.
- ds - Scatter dice with 4 sides marked with arrows and 2 hit symbols

#### Dice interface
In the general project, the dice models should have a base class die that all the other types of dice inherit. Each dice should be able ro return a list of strings that that represent it's possible faces. The abstract class should have a field DiceTypeof the enum Dice. Each dice should also have a 2 roll functions, one that returns an int for the roll and the other that returns a enum for the roll (this requires adding additional enums for each dice).  The roll method will provide a randomly generated result of one of the die faces.

On a d6, the int would be 1, and the enum would be 1.  For DA dice, a misfire would return a -1 for the int and "Misfire" for the enum.  For Scatter dice, int method would return 0 for an arrow and 1 for a hit.

##### Dice notes
da and ds dice: These are often used together to represent the effects of war machines. Sometimes, the Scatter dice is used with one or more D6 to determine a random direction and distance.

Multiple dice: rules may require you to roll 2D6, 3D6 and so forth. In such cases, simply roll the number of D6 indicated and add the results together. This is a multiple dice roll.

dice roll modifications: To modify a dice roll, simply roll the dice and then add or subtract the modifier(s) shown, effectively changing the result of the roll. If the rules ever instruct you to divide a dice roll, any fractions are rounded up, unless the rules state otherwise. Modifiers are applied after division or multiplication.

dice rolls have a natural roll; The term 'natural' roll describes the actual number shown once a dice has been rolled. In other words, a natural roll is the result before any modifiers are applied.

roll off: The rules may call for players to 'roll-off'. To do this, each player rolls a dice (usually a D6) and the highest score wins. In the case of a tie, roll again unless otherwise instructed.

rerolls:  rules may allow you to re-roll a dice. If taken, a reroll must be accepted even if it is worse than the first. No single dice can be re-rolled more than once, regardless of the source of the re-roll. If you re-roll a multiple dice roll, you must re-roll all of the dice, unless the rule granting the re-roll specifies otherwise.

### Equipment
There should be a base class for equipment that all other equipment inherits. This class should have the fields:
- PointCost
- Type
- Name
and a method to ovveride
- GetSpecialRules (which returns the type of equipments special rules)

#### Weapons
All weapons have the profile
- MinRange	        // An int. 0 for Combat.
- MaxRange          // An int. 0 for Combat.
- StrengthType
- Strength	        // -
- Strength_+        // An int that 
- Armour Piercing	// An int
- Armor Bane        // An int
- Special Rules     // This should be a list of the SpecialRulesEnum

StrengthTypeEnum 
 - User 
 - Weapon

SpecialRulesEnum
- ArmorBane1
- ArmorBane2
- ArmorBane3
- ArmorBane1Charge
- ArmorBane2Charge
- ArmorBane3Charge
- RequireToHands
- AdditionalHandWeapon
- Strength+1Charge
- Strength+2Charge
- Strength+3Charge
- StrikeLast
- StrikeFirst
- FightExtraRank
- VollyFire
- Ponderous
- QuickShot
- MultipleShots2
- MultipleShots3
- MultipleShots4
- ExtraAttacks1
- ExtraAttacks1
- MoveAndShoot

#### Armor
Class with
- ArmorValue
- Special rules


##### Weapon Special rules


### Models 

#### Troop Types:
All models have a troop type given as part of their rules. There are five broad categories of troop type: Infantry, Cavalry, Chariots, Monsters and War Machines, each of which is further divided into sub-categories.

Additionally, some models have the word 'Character' in brackets after their troop type. 


| Type | Subtype | Models per Rank* | Maximum Rank Bonus** | Unit Strength per Model |
|---|---|---:|---:|---:|
| Infantry | Regular Infantry | 5 | +2 | 1 |
| Infantry | Heavy Infantry | 4 | +2 | 1 |
| Infantry | Monstrous Infantry | 3 | +2 | 3 |
| Infantry | Swarms | — | — | 3 |
| Cavalry | Light Cavalry | 5 | +1 | 2 |
| Cavalry | Heavy Cavalry | 4 | +1 | 2 |
| Cavalry | Monstrous Cavalry | 3 | +1 | 3 |
| Cavalry | War Beasts | 5 | +1 | 1 |
| Chariots | Light Chariots | 3 | +1 | 3 |
| Chariots | Heavy Chariots | — | — | 5 |
| Monsters | Monstrous Creatures | — | — | As Starting Wounds |
| Monsters | Behemoths | — | — | As Starting Wounds |
| War Machines | War Machines | — | — | As Starting Wounds |
|

#### Base sizes:
With a few exceptions, all models used in a game of Warhammer: the Old World should be mounted upon a square or rectangular base, the dimensions of which are are in millimetres (mm). Sometimes, a range of sizes will be given. In such cases, the base the model is provided with is the correct base to use.

- 25by25
- 25by50
- 30by30
- 30by60
- 40by40
- 40by60
- 50by50
- 50by75
- 50by100
- 60by100
- 75by50
- 100by100
- 100by150

#### Characteristics Profile
Models may have two or more rows on their characteristics profile, often with gaps in each (shown as a dash ' - '). Each row represents a different model, combined together into a single profile. For example, one row might represent a rider, the next their mount.

#### Profile Attributes

- Movement (M)
- Weapon Skill (WS)
- Ballistic Skill (BS)
- Strength (S)
- Toughness (T)
- Wounds (W)
- Initiative (I)
- Attacks (A)
- Leadership (Ld)

#### Other Model information:
- Base Points Value (int)
- Troop Type
- Base Size
- Equipment
- Magic
- Options
- Special Rules
- Magic Items
- Unique

#### Calculated model information
A model also has:
    Total Point Value (which includes the cost of all 



#### Model Notes
All units in the game have a profile

All characteristics: rated on a scale from 0 to 10 – they cannot go below 0 and only in the rarest of cases will they rise above 10. 

If a model has a characteristic of '0', it has no ability whatsoever in what the characteristic represents. This is seen most often with Ballistic Skill, as many models simply lack the ability to make any form of ranged attack.

If any model or object has a Weapon Skill of 0 then it is unable to defend itself in combat, and any blows struck against it will therefore automatically hit. If at any time a model's Strength, Toughness or Wounds characteristic is reduced to 0, it is slain and removed from play.

The rules will often call for a characteristic to be modified. To do this, simply add or subtract the modifier(s) shown to the characteristic, effectively increasing or decreasing the value.

### Characteristics Tests
A model will sometimes be called upon to make a characteristic test. Such a test could be made against any characteristic the model has. For example, a Toughness test is a characteristic test.

To make a characteristic test, roll a D6 and compare the result to the relevant characteristic on the model's profile. If the result is equal to or less than the value of the characteristic, the test is passed. If, however, the result is greater, the test has been failed.

Where a model (or unit) has more than one value for the same characteristic, use the highest value.

#### Leadership tests
At various times, a model or unit might be called upon to make a Leadership test, or to otherwise test against Leadership in some way.

To make a Leadership test, roll 2D6. If the result is equal to or less than the model's Leadership value, then the test has been passed. If the result is greater than the model's Leadership value, the test has been failed. This will all too often result in the unit fleeing.

Whenever Leadership is used, a unit that contains models with different Leadership values will always use the highest – warriors naturally look to the most steadfast of their number for guidance.

When making a Leadership test a natural roll of 12 (i.e., rolling a double 6) is always considered to be a fail, regardless of any modifiers that might apply, whereas a natural roll of 2 (i.e., rolling a double 1) is always considered to be a pass.


#### Notes:
When making a characteristic test a natural roll of 6 is always a failure, and a natural 1 is always a success, regardless of any other modifiers. Additionally, if the model has a characteristic of 0 or ' - ' it automatically fails the test.