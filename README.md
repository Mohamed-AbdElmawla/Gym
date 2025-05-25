# GymGo

GymGo is a web application designed to manage various aspects of a gym environment. It provides functionalities for users with different roles, including members, coaches, and administrators.

## Key Features

* **User Management**: Secure user authentication and authorization with distinct roles (Admin, Member, Coach).
* **Coach Enrollment**: Members can apply to become coaches, with administrative review and approval.
* **Training Plan Creation**: Users can create and manage personalized training plans, defining exercises, sets, repetitions, and weights.
* **Messaging System**: Real-time communication between users, supporting attachments and message history.
* **Notifications**: Instant notifications for significant events, such as coach enrollment status updates or new messages.
* **Subscription Management**: Coaches can create and manage subscription plans with customizable durations and pricing.
* **Exercise Catalog**: A browseable catalog of exercises with descriptions and images for workout planning.
* **User Profiles**: View and manage personal profiles.

## Technologies Used

* **ASP.NET Core MVC**: Web framework for the application.
* **Entity Framework Core**: ORM for database interactions.
* **ASP.NET Core Identity**: Handles user authentication and authorization.
* **SignalR**: Enables real-time communication for features like notifications.
* **SendGrid**: Utilized for sending emails (e.g., password recovery).
