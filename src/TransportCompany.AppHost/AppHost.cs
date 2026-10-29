var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TransportCompany_Api>("api");

builder.Build().Run();