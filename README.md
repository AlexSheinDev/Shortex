# Shortex

Structure:
	
	1. 3-tier architecture with UoW and Repository patterns;
	2. SQL Server as DB;
	3. Blazor Server as a Client App (due to the lack of huge amount of users and 'pet-project' purposes);
	4. Docker - containerized WebApp with DB;
	5. Automapper to work with DTOs.

Functionallity:
	
	1. Shorten links;
	2. Be forwarded by providing shortened links to the original destinations;
	3. See the history of 'shortening';
	4. DbInitializer - once the app is firstly run - DB would be initialized and used further;
	5. 'Run-out of the box' with Docker compose file.

Set-up:

	Clone the project, write the command 'docker-compose up' from the solution folder.

(!) Functionallity to be added/modified:

	1. (!) Include ShortUrlValidator to logic in order to validate original urls;
	2. Refactoring;
	3. Write Notification Service;
	4. Unit tests (xUnit);
	5. Add modal pop-ups;
	6. 'Unify' logic from FE side;
	7. Add Preloaders.