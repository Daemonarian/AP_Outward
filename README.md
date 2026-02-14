# Outward Archipelago

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
- Replaced most skills and unique items in **Outward** with **Archipelago** location checks.
- Added **Archipelago** location checks behind the first completion (pass or fail) of *every* quest in the game.
- Modified the friendly Immaculate side quest so that the friend immaculate never dies, can give a reward at every location, and can give an extra reward for visiting him in all four original zones.
- Added a Skillsanity mode where skills offered by skill trainers are now randomized.
- Optionally added location checks for activating the Cabal of Wind altars in each region, and added their boons to the item pool.
- Option to add *Breakthough Points* to the item pool, which also adds location checks for speaking to each of the skill trainers that teach *Breakthrough Skills*.
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

There are two important files to download from [releases](https://github.com/Daemonarian/AP_Outward/releases).
- `outward.apworld` is the **Archipelago** APWorld (basically a mod for **Archipelago**).
- `OutwardArchipelago.zip` is the mod package for **Outward: Definitive Edition** compatible with **R2ModMan**.

The `outward.apworld` is only needed by the **Archipelago** host.
It is not needed by the person playing the game, except to give it to whoever is hosting your **Archipelago**.
If you change the name of `outward.apworld` to `outward.zip`, you can find a file inside called `Outward Definitive Edition.yaml` which contains our recommended default settings for the randomizer.
We defer instructions on how to install the APWorld on the **Archipelago** server and generating a multi-world to the **Archipelago** [website](https://archipelago.gg/tutorial/Archipelago/setup_en).

To install the mod, you can find general instructions for installing mods in **Outward** [here](https://outward.fandom.com/wiki/Installing_Mods).
We recommend using **[R2ModMan](https://thunderstore.io/package/ebkr/r2modman/)**, and provide specific instructions below.
1. Create a new profile for **Outward: Definitive Edition**.
2. Under `Other` > `Settings` > `Profile`, click on `Import local mod`, and select the `OutwardArchipelago.zip` file.
3. Under `Mods` > `Installed`, expand `OutwardArchipelago` and click `Install Dependency` until the button disappears.
4. Click `Start modded` in the upper left corner. (This generates the config file below.)
5. Exit the game immediately at the main menu.
6. In **R2ModMan** go to `Other` > `Config editor`, select `BepInEx\config\com.daemonarium.apoutward.cfg`, and choose `Edit Config`.
7. Enter the connection details for the **Archipelago** server and hit `Save`.
8. Click `Start modded` again, and enjoy!

If everything is working as intended, you should see a connection status icon in the upper-right hand corner of the screen that looks circular with a depiction of a chain link inside.
A full chain link means that you are connected to the **Archipelago** server and are good to start playing!
A broken chain link means that we could not login to the **Archipelago** server.
Actual images of the two icons are as follows:

![Archipelago connection status icon for a good connection](https://github.com/Daemonarian/AP_Outward/blob/main/Mod/assets/plugins/assets/archipelago_connected.png?raw=true) = connected
![Archipelago connection status icon for a no connection](https://github.com/Daemonarian/AP_Outward/blob/main/Mod/assets/plugins/assets/archipelago_disconnected.png?raw=true) = not connected

If you have trouble connecting to the server, you will need to consult the mod's log file to get a more detailed message of what is going wrong.
In **R2ModMan**, the log file can be found by going to `Other` > `Settings` > `Locations` > `Browse profile folder`, then navigating to `BepInEx\LogOutput.log`.

## Developer Instructions

There is a Visual Studio solution file `AP_Outward.slnx` at the root of the repo.
As long as you have the pre-requisites installed, you should be able to open that file, and build in Visual Studio.
Alternatively, the projects and solutions are SDK-style projects, and can be built from the command line using `dotnet` .NET Core.

Building the project yourself will require the C# application developer workflow in Visual Studio, as well as Python 3.13.
Preferably, on Windows, you should have the `py` Python Install Manager with Python 3.13 installed in it.
Otherwise, ensure that `python3.13` is available in your system Path when building from Visual Studio.
