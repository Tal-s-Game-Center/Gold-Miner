
# Gold Miner Game 🎮

## Overview
Gold Miner is a nostalgic game where a miner sitting at the top of the screen sends a hook to collect gold nuggets scattered below. The hook extends, grabs the gold, and brings it upwards for the player to claim.

---

## Features
- **Interactive Hook**: Extends to grab objects like gold nuggets.
- **Dynamic Nugget Spawning**: Randomly scattered nuggets with varying scales and positions.
- **Rotating Hook**: Oscillates left and right to help aim the hook.

---

## Game Structure

### Assets
The **Assets** folder contains all the required game components:
- **Prefabs**: Reusable object templates for nuggets, hook, etc.
- **Scripts**: Logic for game behavior (e.g., spawning, movement).
- **Art**: Visual assets for gold nuggets, miner, and background.
- **Scenes**: The main scene where gameplay takes place.

### Scripts

#### `HookTrigger.cs`
Handles interactions when the hook collides with other objects.
- Attaches nuggets to the hook on contact.
- Stops the hook when it hits a background object.

#### `Rotator.cs`
Controls the oscillation of the hook.
- Allows the hook to rotate around a pivot point for precise aiming.
- Sine wave logic ensures smooth rotation.

#### `RandomSpawner2D.cs`
Spawns gold nuggets in the scene.
- Ensures nuggets don’t overlap and respects the miner's bounds.
- Scales nuggets randomly for variety.

#### `RopeController.cs`
Manages the hook's extension and retraction.
- Allows rope expansion and shrinking on keypress.
- Resets the rope after grabbing a nugget.

---

## Installation
1. Open the project in **Unity**.
2. Ensure **WebGL** is installed if building for the web.
3. Load the main scene from the `Scenes` folder.
4. Press Play to start the game!

---

## Future Enhancements
- Add scoring and timer for competitive gameplay.
- Introduce new levels with varying layouts.
- Include power-ups or obstacles for added challenge.

---

## License
This game is free to use and modify for personal projects. For commercial use, ensure assets comply with their respective licenses.

---

## Credits
Developed with ❤️ using Unity by Tal Sahar.
