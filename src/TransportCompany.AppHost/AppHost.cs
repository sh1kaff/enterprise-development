var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.TransportCompany_Api>("api");

builder.Build().Run();