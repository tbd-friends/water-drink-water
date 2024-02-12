# water-drink-water

An application to help you remember to drink water.

This accompanies streams on https://twitch.tv/tbdgamer so, there are a few guidelines to follow with PRs.

Guidelines

- No major stand-alone PRs, i.e. Don't do a PR with "I made it DDD or Here's the project implemented with the 'right' architecture"
- You're more than welcome to pick up existing issues and make a fix for them.
- If you feel you found a bug or an issue, create an issue on the GH board, and create a PR for the issue.
- Welcome to make alternative UI, add under clients folder name <technology.initials>
  - Must not change API to accommodate (other than CORS)
- All PRs will be reviewed on stream
- There's no "right" or "wrong", "bad" or "good" but making a PR doesn't mean it will get merged.
- These guidelines aren't final

## Getting Started

- Fork the project.
- Open the solution file.
- If you are using Visual Studio you may need an extension to view the database, which is SQLite. [SQLiteBrowser](https://sqlitebrowser.org/dl/)
- Configure the Startup Projects.
  - Select Multiple startup projects, right click on the solution.
  - Set the 'api' and 'blazor.wa.tbd' projects to Action == Start
- Start the application
- Navigate to the api folder and open the 'createTestAccount.http' file.
- Bring the Debug Console Window into view, check it's running on the same port as what is specified in the 'createTestAccount.http' file, if not update it, save, then just above the word "POST", click "Send Request", you should see the record created.
- Now check the Blazor Debug Console and navigate to the url, log in using the username '<test@nowhere.com>' and the password 'test'
- You must be thirsty after that, drink water 😀
