internal class Program
{
  private static void Main(string[] args)
  {
    var builder = DistributedApplication.CreateBuilder(args);

    // Add the backend project
    var backend = builder.AddProject<Projects.Kayura_Mutfak_WebApp>("kayura-mutfak-webapp");

    builder.Build().Run();
  }
}