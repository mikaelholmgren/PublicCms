This project uses Identity package without the default UI.
It also creates a default admin user. The default admin name and email is set in AppSettings. The password is a default one, so don't forget to change it!
Default password is Admin123!
/Cms pages-folder is set up to require Admin role.
Both the role and admin User is created if the identity database doesn't exist when the app starts.

The identity db is setup to use Sqlite by default. If you change it to another provider, don't forget to delete and recreate the migration.


MongoDb
---
To use mongodb locally, use the .yml file included. It requires Docker Desktop installed.
To launch the db server go to the docker folder in the terminal and type:
docker compose up -d
If not shutodwn by the below command, this server will be started automagically when docker start.
To shut the server down type docker compose down in the same folder.

The admin UI for the MongoDb is at:
http://localhost:8081
Default user is admin, and password admin123
