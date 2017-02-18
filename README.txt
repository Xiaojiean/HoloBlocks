README

################################################################################

Instructions on building the project:

1. Pull the code. I mean you already did. lol

2. Open the project in Unity

3. Change Unity performance settings:
	(1) Select Edit > Project Settings > Quality
	(2) Select the dropdown under the Windows Store logo and select Fastest.
	    You'll know the setting is applied correctly when the box in the Windows
	    Store column and Fastest row is green.

4. Unity build settings:
	(1) Select File > Build Settings...
	(2) Select Windows Store in the Platform list
	(3) Select Switch Platform. You might need to reopen the Build Settings window
	    after this step.
	(4) Set SDK to Universal 10
	(5) Set Build Type to D3D
	(6) Set Target device to HoloLens

5. Specify the export for Windows Holographic:
	(1) From the Build Settings... window, open Player Settings...
	(2) Select the Settings for Windows Store tab
	(3) Expand the Other Settings group
	(4) In the Rendering section, check the Virtual Reality Supported checkbox to add
	    a new Virtual Reality Devices list and confirm "Windows Holographic" is listed
	    as a supported device.

6. Export the Visual Studio solution:
	(1) Return to the Build Settings window.
	(2) Add all scenes
	(3) Check Unity C# Projects under "Windows Store" build settings.
	(4) Click Build
	(5) Click New Folder and name the folder "App".
	(6) With the App folder selected, click the Select Folder button.
	(7) When Unity is done building, a Windows File Explorer window will appear.
	(8) Open the App folder in File Explorer.
	(9) Open the generated Visual Studio solution HoloClay.sln

7. Build and Deploy:
	Refer to the instuctions in the following website to build and deploy onto the
	HoloLens. Make sure to enable Microphones under Package.appxmanifest > Capabilites.

	https://developer.microsoft.com/en-us/windows/holographic/Using_Visual_Studio.html

################################################################################

How to use the App:

1. Upon launch, the app will show a start menu. Click on the start button.

2. After clicking on the start button, you will be able to use the following voice commands:
	(1) "Create sphere" will create a sphere about one meter away from you in the direction you are looking at.
	(2) "Create cube" will create a cube about one meter away from you in the direction you are looking at.
	(3) "Delete all spheres" will delete all spheres from your workspace.
	(4) "Delete all cubes" will delete all cubes from your workspace.
	(5) "Clear canvas" will delete all objects from your workspace.
	(6) "Start menu" will delete all objects from your workspace and take you back to the start menu.

3. In addition, you can click on any object to pick it up and move it around using your gaze.
   You can let go of the object by clicking a second time.

################################################################################
