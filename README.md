# Harley
Harley is a simple discord bot built upon [Discord.Net-Labs](https://github.com/Discord-Net-Labs/Discord.Net-Labs)  

# The basics  
Handling interactions is done through hooking the `InteractionCreated` event on a discord client and appropriately creating a task that takes a `SocketInteraction` as an argument 

An example can be found [here](https://github.com/Discord-Net-Labs/Discord.Net-Labs/tree/Interactions#message-components)  

# The commands  
The commands for Harley are made upon the built-in commands framework and show how to create and send simple components  

## General Commands  
Some general purpose commands to showcase buttons

### url-button  
This command displays a simple URL button  

### simple-button  
Showcases a simple button with the primary style  

### custom-id  
This command showcases how you can handle interactions with buttons that have custom Ids. This is done in the command handler  

### multi-button  
Showcases all the styles of buttons and displays them in different rows  

## Info  
Showcase info about the bot  

### info  
Uses buttons to showcase various pieces of info relating to Harley  

## Fun  
Few fun and miscellaneous commands  

### rps  
Play Rock Paper Scissors through buttons!

## Running the Bot  
Harley has a few dependencies which are required to run (and some that are not)  

### Required  
- Latest version of [Discord.Net-Labs](https://github.com/Discord-Net-Labs/Discord.Net-Labs)


### Not required  
- [Discord.Addons.Hosting](https://github.com/Hawxy/Discord.Addons.Hosting) - This allows me to use the host builder to configure the discord bot  
    Without this the way you create and run your discord bot will be different, an example of how to do this can be found [here](https://docs.stillu.cc/guides/getting_started/first-bot.html) 
  
- Serilog - Serilog allows for customised logging but is not a requirement  

## Slash Commands?  
Slash commands are gonna be added soon! I plan on adding a few slash commands to showcase their abilities.