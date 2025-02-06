This project includes **two** Unity projects. One is the server,
and the other is the client. This runs locally (127.0.0.1)

# Usage
1. Open both projects
2. Navigate to the `SampleScene` scene under `Assets/_Project/Scenes/SampleScene.unity`
3. Start the client first
4. Start the server (make sure the `receiving` bool is checked in the server object)
5. Use WASD on the client. The server should reflect the same movements
6. Close the server, followed by the client. The server instance will
hang if you don't (you can restart the client if you accidentally turned 
it off first, then turn off the server)

