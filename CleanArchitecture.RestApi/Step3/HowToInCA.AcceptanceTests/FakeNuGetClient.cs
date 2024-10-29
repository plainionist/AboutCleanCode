using HowToInCA.Adapters.NuGet;

namespace HowToInCA.AcceptanceTests;

public class FakeNuGetClient : INuGetApi
{
    public async Task<Result<string, Exception>> QueryVersionsAsync(string packageName)
    {
        await Task.Yield();

        if (packageName == "Newtonsoft.Json")
        {
            return
                """
                {
                    "versions": [
                        "10.0.1",
                        "11.0.1-beta1",
                        "11.0.1",
                        "12.0.3",
                        "13.0.3"
                    ]
                }
                """;
        }
        else
        {
            return new Exception("Package not found");
        }
    }

    public async Task<Result<string, Exception>> QueryRegistrationAsync(string packageName, Version version)
    {
        await Task.Yield();

        return
            $$"""
            {
                "@id": "https://api.nuget.org/v3/registration5-semver1/{{packageName}}/{{version.ToString(3)}}.json",
                "@type": ["Package", "http://schema.nuget.org/catalog#Permalink"],
                "catalogEntry": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/{{packageName}}.{{version.ToString(3)}}.json",
                "listed": true,
                "packageContent": "https://api.nuget.org/v3-flatcontainer/{{packageName}}/{{version.ToString(3)}}/newtonsoft.json.{{version.ToString(3)}}.nupkg",
                "published": "2019-11-09T01:27:30.723\u002B00:00",
                "registration": "https://api.nuget.org/v3/registration5-semver1/{{packageName}}/index.json",
                "@context": {
                    "@vocab": "http://schema.nuget.org/schema#",
                    "xsd": "http://www.w3.org/2001/XMLSchema#",
                    "catalogEntry": { "@type": "@id" },
                    "registration": { "@type": "@id" },
                    "packageContent": { "@type": "@id" },
                    "published": { "@type": "xsd:dateTime" }
                }
            }
            """;
    }

    public async Task<Result<string, Exception>> QueryCatalogAsync(Uri catalogUri)
    {
        await Task.Yield();

        if (catalogUri == new Uri("https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json"))
        {
            return
                """
                {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json",
                    "@type": ["PackageDetails", "catalog:Permalink"],
                    "title": "Json.NET",
                    "verbatimVersion": "12.0.3",
                    "version": "12.0.3",
                    "dependencyGroups": [
                        {
                            "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netframework4.5",
                            "@type": "PackageDependencyGroup",
                            "targetFramework": ".NETFramework4.5"
                        },
                        {
                            "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netportable0.0-profile328",
                            "@type": "PackageDependencyGroup",
                            "targetFramework": ".NETPortable0.0-Profile328"
                        },
                        {
                            "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard2.0",
                            "@type": "PackageDependencyGroup",
                            "targetFramework": ".NETStandard2.0"
                        }
                    ]
                }
                """;
        }
        else
        {
            return new Exception("Invalid catalog Uri");
        }
    }
}
