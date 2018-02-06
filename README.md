# WAES Assignment
========================
The Web API can be accessed through these 3 Urls:

- Post: <host>/v1/diff/<ID>/left
- Post: <host>/v1/diff/<ID>/right
- Get: <host>/v1/diff/<ID>
 
## Configuring the project:

After installing all nuget packages, you need to do the following:
- Install Redis if you don't have it already installed (Optional) (https://github.com/MicrosoftArchive/redis/releases)
- If you choose not to install Redis, or just don't want to use it, you will have to edit the file Waes.App\App_Start\IoCConfig.cs
and follow the instructions inside the method RegisterDependencies.
- If you choose to install Redis, or already have it installed, you will have to open the App.config file from the Waes.Test project
and the Web.config from the Waes.App project and check if the CacheConnection key contains the correct value for your Redis server.

## Consuming the service:

As required in the assignment, there are three endpoints.
First, you have to provide the base64 for both left and right endpoints, sending a json via POST in the following format:
```coffee
{
    Base64: '<Your base64 string>'
}
```
After providing the right and left base64 files, you will be able to access the result of the comparison
through the GET url: <host>/v1/diff/<ID>