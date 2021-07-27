# Generic Space Sim


https://user-images.githubusercontent.com/82777171/127158500-a50c5fb4-cec7-43f0-b617-37cb4d0d0a69.mp4



Idea of the project was to see if i can mimic movement type that is normally achieved by rigidbody physics, using only object's transform. I did not know what i'm gonna achieve in the end, but i think it turned out to be pretty fun and arcade-like movement system. All visuals are free assets from Unity assetstore.

## Download
- [Original project](https://drive.google.com/file/d/1jVkF2nSzNzuTofgM8kjBztXfi9Psl_Lg/view?usp=sharing) with heavy skybox and spaceship assets (316MB in total).
- [Lightweight project](https://github.com/ForestSquirrelDev/GenericSpaceSim/blob/master/Packages/Generic%20space%20sim%20light.unitypackage) with smaller size assets (44MB in total).

### Controls
- W/S to speed up or slow down
- Q/E to roll
- Mouse to change pitch/yaw

### Component organisation
When several components are meant to be on the same GameObject (in this case its flying controls for ship), personally i like to break down all the necessary scripts into separate self-contained parts, in order to make this player controller-related code more manageable and sustainable, and set up easy communication between components.

![CO](https://user-images.githubusercontent.com/82777171/127177990-a1c28b0e-a919-43c1-baf5-570b5a4aa0a2.png)

Ship script represents whole player structure and works like a parent for other player components: if necessary, all data (e.g. player velocity, input info) is meant to go through it, and every player class has a reference to Ship script just in case. This way it's much easier to work with scripts because every class is responsible for only one thing and you always know where you can get a reference.

However, this approach has obvious downsides: for instance, scripts still need to know about each other (only they reference one another through their parent rather than directly). Because of that, i feel like this component structure is fine for smaller projects - it reduces cross-component dependencies a lot in comparsion to doing everything in one file, but it probably won't work for big and scalable ones.

Thanks to John Stejskal for the [overview](https://youtu.be/_vj1GASSO9U?list=PLB6BAQR-fTkJX5KoODSGjtBlH_lJXBb71) of this practice.

### List of used assets
- [Spaceship](https://assetstore.unity.com/packages/3d/vehicles/space/spaceship-by-pixel-make-99120)
- [Earth](https://assetstore.unity.com/packages/3d/environments/sci-fi/planet-earth-free-23399)
- [Skybox](https://assetstore.unity.com/packages/2d/textures-materials/diverse-space-skybox-11044)
- [Asteroids](https://assetstore.unity.com/packages/3d/environments/asteroids-pack-84988)
- [Crosshair](https://opengameart.org/content/3-fps-crosshairs)
