# [Outward Archipelago](https://github.com/Daemonarian/AP_Outward)

A mod for **[Outward: Definitive Edition](https://store.steampowered.com/app/794260/)** that adds support for the **[Archipelago](https://archipelago.gg)** multiworld multi-game randomizer.
Our philosophy thus far has been to add as many **Archipelago** location checks as we can without changing the base game experience too much.
This way, an experienced **Outward** player should be able to figure their way through their first randomizer without needing to consult our documentation.
We have, however, made the following changes to the base game:
- Added an **Archipelago** connection status icon overlay throughout the game, including menus.
- **Archipelago** messages, including the chat, now appear in the in-game chat window.
    - Players may send messages to the **Archipelago** chat via the in-game chat window by prefixing their message with `/ap ` (the space is important).
    - **Archipelago** chat commands, such as `!hint`, may also be sent this way (e.g. `/ap !hint`).
- Added an *AP Item* item to the Outward game to serve as a placeholder for items that have been randomized.
- Added *Quest License* passive skills that serve as progression items.
    - Main quest NPCs now refuse to talk to you about the main quest unless you have the corresponding *Quest License* skill.
- Replaced most unique items in **Outward** with **Archipelago** location checks.
- Added **Archipelago** location checks behind the first completion (pass or fail) of *every* quest in the game.
- Added support for **Archipelago**'s *death-link* mode.
    - *Death-links* recieved will result in the player's death.
    - Any player deaths, for any reason (outside of another *death-link*), will send a *death-link* to the **Archipelago** server.

The intended experience for this mod is to player a single save file (perhaps with legacy chest items) from beginning to end after successfully connecting to the **Archipelago** server.
However, we do not restrict you to only playing one save file.
If you create a new save file, that new character should be sent all the items that you previously unlocked in that **Archipelago** multi-world.
You can then switch back and forth between the save files and both saves will be sent any items that were obtained in the other.
You may find this behavior useful if you somehow find yourself accidentally soft-locked.
We do intend to remove all potential soft-locks that we find, but this will still be useful for early versions of the game.
However, we advise caution with attempting to play this mod with old save files.
While we do not prevent you from doing so, you may find that save file altered in a way that makes it un-playable without the mod.

Regarding 2-player co-op, we have not fully tested this scenario.
Joining a friend's game who is playing in an **Archipelago** multiworld will require installing the mod yourself, but only the host needs to connect to the **Archipelago** server.
We make no promises, though, about what will happen to your location checks if you let the guest do the work.

## How to play

First, you will need to host or join an **Archipelago** multi-world.
For this, you will need the corresponding **Archipelago** APWorld, `outward.apworld`, available from this project's main repository, [here](https://github.com/Daemonarian/AP_Outward/releases).
We defer further instructions to the **Archipelago** [website](https://archipelago.gg/tutorial/Archipelago/setup_en).

After installing this mod and getting your **Archipelago** server connection details, you will need to provide these details to the mod.
This can be done through the config file `BepInEx\config\com.daemonarium.apoutward.cfg`.
You can find this config file through **[R2ModMan](https://thunderstore.io/package/ebkr/r2modman/)** by navigating to `Other` > `Config editor`, or by navigating to your **Outward** install directory.
If the config file does not exist, you can create it by launching the game with this mod installed and then immediately exiting from the main menu.

If everything is working as intended, you should see a connection status icon in the upper-right hand corner of the screen that looks circular with a depiction of a chain link inside.
A full chain link means that you are connected to the **Archipelago** server and are good to start playing!
A broken chain link means that we could not login to the **Archipelago** server.
Actual images of the two icons are as follows:

![Archipelago connection status icon for a good connection](https://github.com/Daemonarian/AP_Outward/blob/main/Mod/assets/plugins/assets/archipelago_connected.png?raw=true) = connected
![Archipelago connection status icon for a no connection](https://github.com/Daemonarian/AP_Outward/blob/main/Mod/assets/plugins/assets/archipelago_disconnected.png?raw=true) = not connected

If you have trouble connecting to the server, you will need to consult the mod's log file to get a more detailed message of what is going wrong.
In **R2ModMan**, the log file can be found by going to `Other` > `Settings` > `Locations` > `Browse profile folder`, then navigating to `BepInEx\LogOutput.log`, or by navigating to your **Outward** install directory.
