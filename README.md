# Hand Tracking supported Virtual Instrument

Play songs with your hands on air using Web Cam or with you keyboard *[Q:P]*.

## Getting Started

These instructions will help you set up the project on your local machine for development and testing.

### Prerequisites

Unity: Version 2022.3.44f1 <br/>
Main.exe: Pre-built executable or build from source

### Installation

1. **Download Main.exe from the provided source below:** <br/>
[Main.exe](https://www.dropbox.com/scl/fi/s1cy84rrwmogtx0u2zvmb/Main.exe?rlkey=fpzn4vum7kww85uga0cqernyq&st=sf7fech6&dl=0)

2. **Move the Executable:** <br/>
Place the downloaded Main.exe into the HTsynth/Assets/External directory. <br/>

<br/>

**OR** <br/>

<br/>

1. **Clone the repository:** <br/>
`git clone https://github.com/tuomaSuni/opencv_mediapipe.git`
Open a terminal and navigate to the directory. <br/><br/>
`cd opencv_mediapipe`

2. **Create the Executable:** <br/>
Use pyinstaller to bundle the Python script into an executable. Replace [PATH-TO-MEDIAPIPE-PACKAGE] with the actual path to your installed Mediapipe package.<br/><br/>
`pyinstaller --add-data "C:\[PATH-TO-MEDIAPIPE-PACKAGE];mediapipe" --noconsole --one-file Main.py` <br/><br/>

3. **Move the Executable:** <br/>
After the build process completes, move the generated .exe file from the dist folder into the HTsynth/Assets/External directory.

## Built With

* [Unity3D](https://unity.com/) - The game engine used
* [OpenCV](https://pypi.org/project/opencv-python/) - Computer Vision library
* [Mediapipe](https://pypi.org/project/mediapipe/) - Hand Recognition library
* [socket](https://docs.python.org/3/library/socket.html) - Python Networking interface for sending and receiving data

## Additional Notes

Ensure all dependencies (e.g., Python packages, Unity version) are correctly installed on your machine.
If you encounter any issues during installation or execution, consult the relevant documentation for Unity, OpenCV, Mediapipe, or Socket.
