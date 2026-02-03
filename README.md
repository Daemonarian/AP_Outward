# Outward Archipelago

A mod for **Outward: Definitive Edition** that adds support for the *[Archipelago](https://archipelago.gg)** multiworld multi-game randomizer.
Our philosophy thus far has been to attempt to add as many Archipelago location checks as we can without changing the base game experience too much.
This way, an experienced Outward player should be able to figure their way through their first randomizer without needing to consult our documentation.
We have, however, made the following changes to the base game:
- Added an **Archipelago** connection status icon overlay throughout the game, including menus.
- Added an *AP Item* item to the Outward game to serve as a placeholder for items that have been randomized.
- Added *Quest License* passive skills that serve as progression items.
    - Main quest NPCs now refuse to talk to you about the main quest unless you have the corresponding *Quest License* skill.
- **Archipelago** messages, including the chat, now appear in the in-game chat window.
    - Players may send messages to the **Archipelago** chat via the in-game chat window by prefixing their message with `/ap ` (the space is important).
    - **Archipelago** chat commands, such as `!hint`, may also be sent this way (e.g. `/ap !hint`).
- Added support for **Archipelago**'s *death-link* mode.
    - *Death-links* recieved will result in the player's death.
    - Any player deaths, for any reason (outside of another *death-link*), will send a *death-link* to the Archipelago server.
- Replaced most unique items in **Outward** with **Archipelago** location checks.
- Also, added **Archipelago** location checks behind the first completion (pass or fail) of *every* quest in the game.

So far, we have only minimally tested the randomizer mod in a 2-player co-op scenario.
While it seems to work so far, we make no guarantees.

## How to play

There are two files that may be found in the latest [releases](https://github.com/Daemonarian/AP_Outward/releases).
- `outward.apworld` is the **Archipelago** APWorld (basically a mod for **Archipelago**).
- `OutwardArchipelago.zip` is the mod package for **Outward: Definitive Edition** compatible with **R2ModMan**.

The `outward.apworld` is only needed by the **Archipelago** host.
It is not needed by the person playing the game, except to give it to whoever is hosting your **Archipelago**.
If you change the name of `outward.apworld` to `outward.zip`, you can find a file called `Outward Definitive Edition.yaml` which contains our recommended default settings for the randomizer.

To install the mod, we recommend using **R2ModMan**.
1. Create a new profile for **Outward: Definitive Edition**.
2. Under `Other` > `Settings` > `Profile`, click on `Import local mod`, and select the `OutwardArchipelago.zip` file.
3. Under `Mods` > `Installed`, expand `OutwardArchipelago` and click `Install Dependency` until the button disappears.
4. Click `Start modded` in the upper left corner. (This generates the config file below.)
5. Exit the game immediately at the main menu.
6. In **R2ModMan** go to `Other` > `Config editor`, select `BepInEx\config\com.daemonarium.apoutward.cfg`, and choose `Edit Config`.
7. Enter the connection details for the **Archipelago** server and hit `Save`.
8. Click `Start modded` again, and enjoy!

If you did everything correctly, you should see a small circular icon with an image of a chain link in the lower-left corner.
This is the **Archipelago** server connection status.
A yellow-ish icon with a full chain link indicates that you are connected!
You can now proceed to create a new character!
A red icon with a broken link indicates some sort of connection problem with the server.
When this happens, you will need to consult the mod's log file to get a more detailed message of what is going wrong.

## Developer Instructions

There is a Visual Studio file `AP_Outward.slnx` at the root of the repo.
As long as you have the pre-requisites installed, you should be able to open that file, and build in Visual Studio.
Alternatively, the projects and solutions are SDK-style projects, and can be built from the command line using `dotnet` .NET Core.

Building the project yourself will require the C# application developer workflow in Visual Studio, as well as Python 3.13.
Preferably, on Windows, you should have the `py` Python Install Manager with Python 3.13 installed in it.
Otherwise, ensure that `python3.13` is available in your system Path when building from Visual Studio.
