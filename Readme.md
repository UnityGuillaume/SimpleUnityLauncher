# Simple Launcher

This very simple tool offer a minimal replacement for the deprecated resolution
dialog Unity used to have.

It can be useful for small demo or game jam where you don't have the time or
resources to build up an option screen.

It simply allows to display a screen at the start of the app that let the
user choose what resolution they want, if it should be fullscreen and the quality
level.

## Usage

- Copy the SimpleLauncher folder into your own project
- Add the SimpleLauncher scene as your first scene (index 0) in the build list.

This will automatically switch the game to windowed 1024x768 at first launch,
displaying the launcher option screen.

If the user saves settings, the chosen resolution, fullscreen flag and quality
settings are saved to disk in a file in `Application.persistentDataPath`, and
those settings will be applied next time the game run, bypassing the Launcher
screen.

By holding down the "submit" input (by default joypad 0, enter, space and return)
as the game start, it will force re-displaying the screen even if the settings
were saved before.

Once the Launch button clicked, this will load scene at index 1 that can be your
game loading scene.

_You can personalize the look of the launcher by modifying the LauncherScene. Note
that there is a black panel on top of everything you need to disable to easily edit
the UI. Don't forget to reenable it after as it hide the launcher which otherwise
flash briefly when loading from file settings_

### Implementation Note

This used a hacked together autoscrolling toggle for the dropdown (in `ScrollRectItemAutoScroll.cs`)
which replace the Toggle in the template for both Resolution and Quality dropdown.

The scrolling isn't perfect, but it's a hasty put together hack to at least
allow to use the dropdown with a pad.

## Known Limitation

Right now, this only works with the built-in Input.

If this prove useful to enough people and I find some time, I'll try updating
that to allow easily hooking it with the new Input System.
