NOTE : This README is Obsolete, i'll update it very soon.

Title: Visual Novel in Unity

Details: We can use the experience of creating a visual novel in order to learn about UI interface and game development in Unity, both in C# and UnityScript.

Objetives: 

- Learn about games life cycle in Unity.
- Games flow and persistence.
- Save and Load of games.
- Main Menu UI, options and settings.
- In-Game UI.
- Write a cool story ! :D ( this will be in the final steps, we want to build an empty framework first and them put the story into it ).
- Music, sound effects and art.
- Bonus : Animations and cinematics. But is not required at all.

Comments: 

There are tons of visual novels that tend to be very interesting or really funny, very easy to do and very short. I'll add some examples of those, but we can also begin this with just a Text-Based game. Here is two very different examples that fall on the same category :

https://www.youtube.com/watch?v=zuGJOdyoLo4 ( basic, bizarre and original in it's aesthetic, a kickstarter success that you should give it a try unless you hate France... cats.. birds... or animals... really ? And sepia... there is tons of sepia ).

https://www.youtube.com/watch?v=869azSXO1BI ( Survival Game where you have to cooperate and understand your partners motives in order to reach the end of the game alive... if possible together. Perfect atmosphere and very immersive, love the sounds and dialogs, the animations are basic and some of the puzzle also are simple but others very complex. I really recommend it to try it. ). 

Here is more info about them : 

 - http://www.usgamer.net/articles/visual-novels-in-america

Prerequirements: 

None, this will be the very basics and programing knowledge is not required because we are gonna do it from the very basic.

We will use Github for this one, so we need a gitignore file that covers our necessities.

Tecnologies:

Unity 5.x

Milestones :

0.1v : Basic menu - game life cycle

 - Create a Main menu that contains :
   - An exit button. When you click this button it will close the game.
   - A new game button. This will open a new scene which will only have a back button to the main menu.

Objective : Learn about scenes life cycle and how you can make reference to each one in order to jump from one to the other.

0.2v : Add background image/cinematic for main menu.

 - Add a cinematic image or background that will be played while you are on the main menu.

Objective : Learn how to handle multimedia content as part of the ui and learn about the singleton pattern. This pattern will be use since we need to keep the same animation during each part of the menu ( load game, save game, new game and options ).

0.3v : Make an dummy in-game event and add save functionality

- Add a counter, random text generator or anything that generates something distinctive each time you play a new game. This distinctive event will be played in the new game scene.
- Add a save button and will save that event inside a file or any alternative that we find more suitable. There are ways to save all game objects into a file and them use that to load them again, this will make things a lot easier for us but. Once i'll find it how i'll add the link here.

Objective : Learn how to save game state each time it runs. This is a very basic functionality that we need for any adventure game.

0.4v : Add Continue Game functionality.

- Add Continue game button that will load the latest saved game.

Objective : See if we are able to load the game that we did save. In future steps we would like to have multiple saved games but for now we can do with just one.

0.5v : Tomorrow, i'm tired and i need to sleep...
