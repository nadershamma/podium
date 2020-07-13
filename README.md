# Podium Case Study

## API

The Api follows a repository pattern to fetch and manage data.

`Controller <----> Service <----> Repository <----> DB`

For simplicity I have created a very simple clean architecture pattern. I have kept everything in a single project 
for simplicities sake but ordinarily the `Core`, `Infrastructure`, `Application` and `Api` folders would all be in 
their own project.

I have setup Swagger UI in order to view and test the API endpoints. 
 
### Setup
Run EF Core migrations
```
$ cd Api/
$ dotnet ef database update --context ApplicantDbContext
$ dotnet ef database update --context MortgageDbContext
```

## Web client
I have built a simple Vuejs app with forms to create an applicant, select the property and deposit value and then 
get a list of qualified mortgages. 

### Setup
```
$ cd web
$ npm install
$ npm run serve
```

