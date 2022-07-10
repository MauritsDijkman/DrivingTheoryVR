# VR Teleport package

An easy-to-implement teleport package for your Unity + Oculus VR project.

## VERSION

v1.2

### Release notes

v1.2

- Added Button Mappings. Assign any of the TP-functions as your desired button.
- Added some more psuedocode here and there.

v1.1

- Fixed an issue regarding the "TELEPORT_RIG"-prefab being made up of Missing-Prefabs
- Left controller' MeshRenderer is now disabled by default. The GameObject itself hasn't changed.

v1.0

- Initial version

## TOC

- [VR Teleport package](#vr-teleport-package)
  - [VERSION](#version)
    - [Release notes](#release-notes)
  - [TOC](#toc)
  - [Requirements](#requirements)
  - [Implementation](#implementation)
    - [From empty to testable](#from-empty-to-testable)
  - [Usage](#usage)
  - [Issues](#issues)

## Requirements

- This Package (`path to the .unitypackage`)
- Oculus' VR package for Unity, downloadable for free in Unity's Asset Store.
<div style="page-break-after: always;"></div>

## Implementation

### **From empty to testable**
-Import the required packages into Unity

**Use Ctrl+Shift+B to open the Build-Settings**\
-Register your scene or the ExampleScene in the build-settings.\
-Switch platform to _Android_.\

**Open the Player-Settings**\
-Enter your company name and product name.

**Open the Other settings**\
-Remove _Vulkan Graphics API_\
-Set the _Minimum API Level_ to at least 19.

**Open XR Settings**\
-Add the _Oculus SDK_\
-Tick the _V2 Signing (Quest)_ box.

**Put on the Oculus Quest**\
-Make sure that the Oculus Quest unit is connected to your PC and is active.\
-Make sure that the Oculus Quest unit has Developer-Mode enabled.

Go back to the Build-Menu and select _Build and Run_.\
**Images**\
![alt text](D:\Sep\Overig\VR_Teleport-Package\Images\Graphics-API_Preview.png "API-Level Image")
![alt text](D:\Sep\Overig\VR_Teleport-Package\Images\API-Level_Preview.png "API-Level Image")
![alt text](D:\Sep\Overig\VR_Teleport-Package\Images\XR-Settings_Preview.png "XR Settings Image")

<div style="page-break-after: always;"></div>

## Usage


**How to Teleport**\
By _pressing and holding_ the trigger on the _back of the controller_, using your index-finger, a line will appear from your controller -- thickening as it goes on. 

If this line hits a surface, it should place an object with an arrow pointing forward. This object is called the TPTarget. 
TPTarget shows you where you’re about to teleport to. By letting go of the trigger you’ll instantly be placed there. 

The TPTarget, along with the line should be _blue_, but if it’s _red_, it simply means that you’re not allowed to teleport to that location because the floor is _too steep_. The maximum allowed amount of steepness can be adjusted in the _TP_Core component (~0.98f is recommended)_. 

**Cancelling the Teleport-Laser**\
If you started pressing and holding the Teleport-Trigger, but regret it, you can cancel it by pressing the Laser-Cancel Button (assigned to both A and B by default).

**Custom rotation**\
The arrow in TPTarget is controllable. By holding the right-controller’s stick in any direction the arrow will mimic the exact direction and hold it when you let go of the stick. 
The arrow shows you what your next central rotation will be after teleporting. For example: If you hold the stick downwards, you’ll be facing the other way after teleporting. 

**Undo**\
Once you’ve teleported, you might want to go back to a specific point. This is why all teleport-positions (and rotations) are saved and by pressing B you can go back 1 step at a time. It works almost exactly like Ctrl+Z in that sense.
</div\>

**Controller visibility**\
Don’t like the seeing the controller the VR-app? Press the Visibility-Toggle Button stick to toggle visibility of both controllers. By default, this is assigned to the right controller's stick. 
<div style="page-break-after: always;"></div>

## Issues

**The TPTarget-Disallowed -object isn’t properly oriented on walls or slanted terrain.**\
This is a known issue and I’m having some trouble fixing it, as I don’t understand why it does what it does. It’s not a big problem, though, as it doesn’t break the application. It is pretty annoying, though.\
(Recreate: Try teleporting to different walls) 
 
**The TP-Laser doesn’t turn blue when moving from steep-terrain to flat terrain. (Doesn’t happen when they’re different objects)**\
This is a known issue. This happens because the line-color updates every time a new object is hit or when the line starts missing. If you keep pointing at the same object, the line will never update – which means that it won’t update even if that one object has both steep and non-steep angles. It doesn’t have any impact on gameplay, luckily.\
(Recreate: Try teleporting on top of a sphere and move from the outside to the top).