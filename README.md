# Donations

Hi! Donations it's going to be a platform which organisations can implement for their own use. Goal is to make it easier for people to donate money to organisations. 

### What functions will the platform provide:

 - Donate (User, Anonymous)
 - View all donations
 - Projects (CRUD)
 - Login/Register
 - Profile

#### Feel free to add new feature if you think that something is missing.

# Structure

## Donations.API
**Controllers** - Contains all controllers


## Donations.Common
**DTOs** - Contains all DTO's that are going to be mapped to models and vice versa.

**Enums** - Contains all Enums within the solution.

**Extensions** - Contains all extensions that we need.

**Helpers** - Contains all helpers that we need.

**Interfaces** - Contains all interfaces that are declared within this project (ex. service interfaces) .

**Mapper** -  Contains mapper class that maps DTO's to Models and vice versa.

**Services** - Contains all services.


## Donations.Data
**Interfaces** - Contains all interfaces that are going to be inside this project (most likely ain't gonna change). As an intial interface is `IDonationsRepository`.

**Migrations** - Contains all EF Migrations.

**Models** - Contains all models of Donations, ex. `User`, `Role`, etc.

**Seeding** - Contains all data that is going to be seeded in context.

**DonationsDbContext.cs** - Is context of Donations database. So everything goes inside here.

**DonationsRepository** - Is repository of Donations (every service uses this one, since its generic).


## Donations.API.Test
Contains all Unit Tests for 	`Donation.API.` project.

## Donations.Common.Test
Contains all Unit Tests for `Donations.Common` project.
