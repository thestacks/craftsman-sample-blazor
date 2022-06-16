# About

This project has been initially generated using [Craftsman](https://github.com/pdevito3/craftsman).
Frontend has been created with Blazor, [Radzen Blazor component library](https://blazor.radzen.com/) and [Fluxor State Management](https://github.com/mrpmorris/Fluxor).

To launch frontend application, simply start backend first and then use `dotnet watch run` or `dotnet run` on RecipeManagement.UI project level.

## Projects modifications

All DTOs were extracted from the backend to new project called **RecipeManagement.Contracts**
This action was necessary to share types between frontend and backend applications.

Project **RecipeManagement.UI** contains simple frontend application enabling CRUD features on generated entities with some nice UI thanks to Radzen Blazor comoponents.
State management in Frontend applications is really important, that's why Fluxor has been used here.
For now, there are no tests, as this is rather PoC.

## Screenshots

![Home screen](/Screenshots/desktop-home.png?raw=true "Home screen")
![Recipes](/Screenshots/desktop-recipes.png?raw=true "Recipes")
![Recipes (no sidebar)](/Screenshots/desktop-recipes-no-sidebar.png?raw=true "Recipes (no sidebar)")
![New recipe (empty)](/Screenshots/desktop-new-recipe-empty.png?raw=true "New recipe (empty)")
![New recipe (validation)](/Screenshots/desktop-recipe-validation.png?raw=true "New recipe (validation)")
![Recipes (mobile)](/Screenshots/mobile-recipes.png?raw=true "Recipes (mobile)")
![Edit recipe (mobile)](/Screenshots/mobile-edit-recipe.png?raw=true "Edit recipe (mobile)")

## ======== Craftsman generated content below ========
### CarbonKitchen

This project was created with [Craftsman](https://github.com/pdevito3/craftsman).

#### Getting Started
###### Set Up Your Database
This project is configured to reference a live database instead of an in-memory one for more robust development. 
By default, the database will be configured to run in a docker container and already has the connection 
string configured in your launch settings.

To set up your database(s):
1. Run `docker-compose up --build` from your `sln` directory to spin up your database(s) (and RabbitMQ, if needed).

After you have your database(s) running in docker, make sure you apply your migrations:
1. Make sure you have a migrations in your boundary project (there should be a `Migrations` directory in the project directory). 
If there isn't see [Running Migrations](##running-migrations) below.
2. Confirm your environment (`ASPNETCORE_ENVIRONMENT`) is set to `Development` using 
`$Env:ASPNETCORE_ENVIRONMENT = "Development"` for powershell or `export ASPNETCORE_ENVIRONMENT=Development` for bash.
3. `cd` to the boundary project root (e.g. `cd RecipeManagement/src/RecipeManagement`)
4. Run `dotnet ef database update` to apply your migrations. 

> You can also stay in the `sln` root and 
run something like `dotnet ef database update --project RecipeManagement/src/RecipeManagement`

###### Running Your Project(s)
Once you have your database(s) running, you can run your API(s), BFF, and Auth Servers by using 
the `dotnet run` command or running your project(s) from your IDE of choice.   

#### Running Integration Tests
To run integration tests:

1. Ensure that you have docker installed.
2. Go to your src directory for the bounded context that you want to test.
3. Confirm that you have migrations in your infrastructure project. If you need to add them, see the [instructions below](##running-migrations).
4. Run the tests

> ⏳ If you don't have the database image pulled down to your machine, they will take some time on the first run.

###### Troubleshooting
-If you have trouble with your tests, try removing the container and volume marked for your integration tests.
- If your entity has foreign keys, you'll likely need to adjust some of your tests after scaffolding to accomodate them.

#### Running Migrations
To create a new migration, make sure your environment is set to `Development`:

###### Powershell
```powershell
$Env:ASPNETCORE_ENVIRONMENT = "Development"
```

###### Bash
```bash
export ASPNETCORE_ENVIRONMENT=Development
```

Then run the following:

```shell
cd YourBoundedContextName/src/YourBoundedContextName
dotnet ef migrations add "MigrationDescription"
```

To apply your migrations to your local db, make sure your database is running in docker run the following:

```bash
cd YourBoundedContextName/src/YourBoundedContextName
dotnet ef database update
```
