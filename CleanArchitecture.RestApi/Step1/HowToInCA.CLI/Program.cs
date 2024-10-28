using HowToInCA.Application.FeatureA;
using HowToInCA.DataAccess.NuGet;

namespace HowToInCA.CLI;

public class Program
{
    public static void Main()
    {
        var nuGetClient = new NuGetClient();
        var nuGetService = new NuGetService(nuGetClient);

        // remaining composition goes here ...

    }
}
