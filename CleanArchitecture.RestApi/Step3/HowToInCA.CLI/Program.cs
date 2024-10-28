namespace HowToInCA.CLI;

using HowToInCA.Adapters.NuGet;
using HowToInCA.Application.FeatureA;
using HowToInCA.DataAccess.NuGet;

public class Program
{
    public static void Main()
    {
        var nuGetApi = new NuGetClient();
        var nuGetClient = new NuGetClientAdapter(nuGetApi);
        var nuGetService = new NuGetService(nuGetClient);

        // remaining composition goes here ...

    }
}
