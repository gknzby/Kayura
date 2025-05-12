internal class Program
{
  private static void Main(string[] args)
  {
    IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

    // Add the backend project
    _ = builder.AddProject<Projects.Kayura_Mutfak_WebApp>("kayura-mutfak-webapp");

    builder.Build().Run();
  }
}