dotnet ef migrations add InitialCreate \
  --project RootCause.Data \
  --startup-project RootCause.App

dotnet ef database update \
  --project RootCause.Data \
  --startup-project RootCause.App