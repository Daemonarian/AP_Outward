# Outward Archipelago

*Outward Archipelago* is a mod for *Outward: Definitive Edition* that adds support for playing as an *Archipelago* client.
For more information about Archipelago, see the [*Archipelago* website](https://archipelago.gg/). For the corresponding **APWorld**, see the [APWorld repository](https://github.com/Daemonarian/AP_Outward).

## Installation

After installing this mod and its dependencies, you will need to create a configuration file with the connection information for the *Archipelago* server you want to connect to.
The easiest way to do this is to start the game once after installing the mod, which will generate a default configuration file at `{Outward Install Directory}\BepInEx\config\com.daemonarium.apoutward.cfg`.
If you are using *R2ModMan*, you can find this config under the config editor for your *Outward* profile.

In the future, we plan to have the *Archipelago* APWorld client generate this configuration file for you, but for now, you will need to edit it manually.

## Gameplay

In order to better integrate with *Archipelago*, this mod makes several changes to the base game:
- A connection status indicator is displayed in the game's HUD in the lower-left corner.
- Archipelago system messages and chat messages are displayed in the game's chat window.
- A new set of skills have been added to the game called *Quest Licenses*, which are used to gate access to the main questline content.
  - *Quest Licenses* are randomized throughout the Archipelago network, so players will need to find them in order to progress through the main story.
  - Players can check which *Quest Licenses* they have obtained by checking the *Passive Skills* tab in the character menu.
  - Completing main story quests will additionally reward *Archipelago* items possibly for other players.
