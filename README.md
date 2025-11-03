"# jay-nc-workflow-webapi" 

dotnet ef migrations add xxx --project ./src/Jay.Workflow.WebApi.Storage/Jay.Workflow.WebApi.Storage.csproj --startup-project ./src/Jay.Workflow.WebApi.Service/Jay.Workflow.WebApi.Service.csproj

dotnet ef database update 0 --project ./src/Jay.Workflow.WebApi.Storage/Jay.Workflow.WebApi.Storage.csproj --startup-project ./src/Jay.Workflow.WebApi.Service/Jay.Workflow.WebApi.Service.csproj

dotnet ef migrations remove --project ./src/Jay.Workflow.WebApi.Storage/Jay.Workflow.WebApi.Storage.csproj --startup-project ./src/Jay.Workflow.WebApi.Service/Jay.Workflow.WebApi.Service.csproj
