namespace HowToInCA.CLI;

using HowToInCA.Application.FeatureA;
using HowToInCA.DataAccess.NuGet;

public class Program
{
    public static void Main()
    {
        var nuGetClient = new NuGetClient();
        var nuGetService = new NuGetService(nuGetClient);

        // remaining composition goes here ...

    }
}
