### RecTeam.Net
#### A site for organizing rec teams.
===========================

Add players to your team, add games to your schedule, and chat with team members, March 10, 2017

By Levi Bibo

#### Details:
* Users can register and log on
* Users can enter details such as their name, phone number, email, preferred position, and an image to represent them.
* Any user can create and manage a new team.
* Once a team is created, the manager can add players to the roster and games to the schedule.
* Players can view the schedule and note if they will be unavailable for a particular game.
* The chat thread includes all of the members of the team and no one else.

#### Setup/Installation Requirements

_Requires Windows, .Net core, SQL SERVER, and a Mailgun account_

1. Clone repository.
2. Navigate to the ArenaRoster/src/ArenaRoster directory and run 'dotnet ef database update'.
3. Enter Mailgun credentials in the EnvironmentVariablesTemplate.cs file.
4. Open Visual Studio and run IIS Express.
5. A web browser will open to the correct port.

#### Technologies used

_This site was built using C#, ASP.Net core, and the Mailgun API._

#### License

_Copyright (c) 2017_
