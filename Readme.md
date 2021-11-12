# Simple Launcher

This very simple tool offer a minimal replacement for the deprecated resolution
dialog Unity used to have. It can be useful for small demo/jam game where you
don't have the time/resources to build up an option screen.

It allow to display a screen at the game start that let the
user choose what resolution they want, if it should be fullscreen and the quality
level.

## Usage

- Copy the SimpleLauncher folder into your own project
- Add the SimpleLauncher scene as your first scene (index 0) in the build list.

This will automatically switch the game to windowed 1024x768 at first launch,
displaying the launcher option screen.

If the user save settings, the choosen resolution, fullscreen flag and quality
settings are saved to disk in a file in `Application.persistentDataPath`, and
those settings will be applied next time the game run, bypassing the Launcher
screen.

By holding down the "submit" input (by default joypad 0, enter, space and return)
as the game start, it will force re-displaying the screen even if the settings
where saved.

Once the Launch button pressed, this will load scene at index 1 that can be your
game loading scene.

### Implementation Note

This used a hacked together autoscrolling toggle for the dropdown (in `ScrollRectItemAutoScroll.cs`)

The scrolling isn't perfect, but it's a haslty put together hack tp at least
allow to use the dropdown with a pad.

There is a black panel on top of the whole scene that get disabled if the scene
is displayed. This allow to not have a briefly flashing Launcher UI when the game
start from saved settings and directly jump to the next scene.

## Known Limitation

Right now, this only works with the built-in Input.

If this prove useful to enough people and I find some time, I'll try updating
that to allow easily hooking it with the new Input System.
