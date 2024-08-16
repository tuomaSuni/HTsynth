# Hand Tracking supported Virtual Instrument

Play songs with your hands on air using Web Cam.

## Getting Started

These instructions will help you set up the project on your local machine for development and testing.

### Prerequisites

Unity: Version 2022.3.42f1<br/>
Main.exe: Build from source

### Installation

1. **Download or clone the GitHub repository to your local machine.**

2. **Navigate to the External Assets Directory:<br/>**
Open a terminal and navigate to the project's HTsynth/Assets/External directory.<br/><br/>
*cd HTsynth/Assets/External*

3. **Create the Executable:<br/>**
Use pyinstaller to bundle the Python script into an executable. Replace [PATH-TO-MEDIAPIPE-PACKAGE] with the actual path to your installed Mediapipe package.<br/><br/>
*pyinstaller --add-data "C.\[PATH-TO-MEDIAPIPE-PACKAGE];mediapipe" --noconsole --one-file Main.py*<br/><br/>
Move the .exe file from a created dist folder into the current directory

## Built With

* [Unity3D](https://unity.com/) - The game engine used
* [OpenCV](https://pypi.org/project/opencv-python/) - Computer Vision library
* [Mediapipe](https://pypi.org/project/mediapipe/) - Hand Recognition library
* [socket](https://docs.python.org/3/library/socket.html) - Python Networking interface for sending and receiving data

## Additional Notes

Ensure all dependencies (e.g., Python packages, Unity version) are correctly installed on your machine.
If you encounter any issues during installation or execution, consult the relevant documentation for Unity, OpenCV, Mediapipe, or Socket.
