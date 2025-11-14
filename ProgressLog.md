# 001 Making the Repository and current progress of the project 2025/10/28
I still had to ask for help for uploading and creating this darn repository but finally actually made the project file. All I had for the project was the old prototype from 2nd year and some progress on the Game Design Document. 
The prototype is now at least made and has some bones, Sam whose agreed to do a trade; I help him with his game on music at least 3 deliverables and he'll help me with some code specifically with Main Menu and Pause menu.
What's been done in terms of bones is that some tags have been made that will be referenced in code, and start of the PlayerCharacter script has been made, inputted the variables that will be used later in the script.
Sam made me download a unity package called Editor Attributes to help with the project.
Key takeaways I'll need to do is relearn the markdowns that GitHub uses since I've kinda forgotten, and use my tasklist more, it has the basis of things I need to do for this project.
# 002 Movement Progress 2025/10/31 
This was quite simple-ish to implement, copying and pasting from my the initial prototype. Cleaning up just some of the formatting the code so it's easier for me to read, skim or comb thorugh for possible problems. I attempted to make the clamp of the the sling shot movement with min and max values. The errors that came up weirdly was about the  'LineRenderer.SetPosition is set out of bounds'   
# 003 Camera Jitteryness and Movement Clean-up
Huzzah! The clamp on the player movement is working! The problem was literally inputing the clamp values in Unity incorrectly. I only did it in the x values and not the y. And also forgetting about the z values despite being in 2D, still matters in being changed. The player movement felt so much better and all it is now is making a test level design to figure out the correct balance of sling force and sling shot clamping.
