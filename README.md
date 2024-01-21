# SignalR Chat App 🗨️

A simple chat application using [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction), built with ASP.NET Core.

## Features

- User authentication with the selection of a username.
- Chat room creation and joining.
- Real-time messaging to users or groups.
- Username and room name uniqueness validation.
- Notifications for attempting to send messages to non-member groups.
- Display members of the selected group.
- Real-time notifications for new user joins.

![Chat App Screenshot](https://github.com/beyzaozgur/SignalRChatApp/blob/master/SignalRChatApp.png "Chat App Screenshot")

## Technologies Used

- ASP.NET Core
- SignalR
- HTML
- JavaScript
- CSS
- Bootstrap

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- Visual Studio (or any preferred code editor)

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/beyzaozgur/SignalRChatApp.git
    ```

2. Navigate to the project directory:

    ```bash
    cd SignalRChat
    ```

3. Open the solution in Visual Studio or your preferred code editor.

4. Set SignalRChatServer and SignalRChatClient as startup projects.
  
5. Configure server connection port in client and run projects.

6. Access the client application in your browser:

    ```bash
    https://localhost:port
    ```

## Usage

1. Launch the client application in your browser.

2. Choose a username to enable chat functionalities.

3. Create a new chat room or join an existing one.

4. Start sending messages to users or groups.

5. **Validation:**
   - Usernames and room names must be unique. If they already exist, an alert will be shown.
   - You cannot send messages to a group if you are not a member. You will be notified if you attempt to do so.

6. **Display Members:**
   - When you select a room, you can see the members of that group or all users.

## Data Storage

This project does not use a database. Data is stored within classes in the application. Please note that all data will be reset upon restarting the application.

## Folder Structure

- **SignalRChatClient**: Contains the client-side code (HTML, JavaScript, CSS, Bootstrap).
- **/Hubs**: SignalR hub for real-time communication.
- **/Models**: Data models and view models.
- **/Data**: Data storage models.

## Configuration

- The SignalR hub configuration can be found in `Startup.cs`.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/beyzaozgur/SignalRChatApp/blob/master/LICENSE.txt) file for details.
