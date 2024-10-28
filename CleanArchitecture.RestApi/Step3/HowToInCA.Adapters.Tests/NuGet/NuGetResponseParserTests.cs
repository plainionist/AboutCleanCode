namespace HowToInCA.Adapters.NuGet.Tests;

using HowToInCA.Application.FeatureA;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public void GetLatestVersionAsync()
    {
        var parser = new NuGetResponseParser();

        var response = parser.GetLatestVersion(VersionResponse);

        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Value.Major, Is.GreaterThanOrEqualTo(12));
    }

    [Test]
    public void GetCatalogUri()
    {
        var parser = new NuGetResponseParser();

        var response = parser.GetCatalogUri(CatalogUriResponse);

        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Value, Is.EqualTo(new Uri("https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json")));
    }

    [Test]
    public void GetSupportedFrameworksAsync()
    {
        var parser = new NuGetResponseParser();

        var response = parser.GetSupportedFrameworks(SupportedFrameworksResponse);

        var supportedFrameworks = response
            .Select(x => x.Select(x => x, y => null!))
            .Where(x => x != null)
            .ToList();

        Assert.That(supportedFrameworks, Contains.Item(new TargetFramework(FrameworkType.NetStandard, new Version(2, 0))));
    }

    private const string VersionResponse =
        """
        {
            "versions": [
                "3.5.8",
                "4.0.1",
                "4.0.2",
                "4.0.3",
                "4.0.4",
                "4.0.5",
                "4.0.6",
                "4.0.7",
                "4.0.8",
                "4.5.1",
                "4.5.2",
                "4.5.3",
                "4.5.4",
                "4.5.5",
                "4.5.6",
                "4.5.7",
                "4.5.8",
                "4.5.9",
                "4.5.10",
                "4.5.11",
                "5.0.1",
                "5.0.2",
                "5.0.3",
                "5.0.4",
                "5.0.5",
                "5.0.6",
                "5.0.7",
                "5.0.8",
                "6.0.1-beta1",
                "6.0.1",
                "6.0.2",
                "6.0.3",
                "6.0.4",
                "6.0.5",
                "6.0.6",
                "6.0.7",
                "6.0.8",
                "7.0.1-beta1",
                "7.0.1-beta2",
                "7.0.1-beta3",
                "7.0.1",
                "8.0.1-beta1",
                "8.0.1-beta2",
                "8.0.1-beta3",
                "8.0.1-beta4",
                "8.0.1",
                "8.0.2",
                "8.0.3",
                "8.0.4-beta1",
                "9.0.1-beta1",
                "9.0.1",
                "9.0.2-beta1",
                "9.0.2-beta2",
                "10.0.1-beta1",
                "10.0.1",
                "10.0.2",
                "10.0.3",
                "11.0.1-beta1",
                "11.0.1-beta2",
                "11.0.1-beta3",
                "11.0.1",
                "11.0.2",
                "12.0.1-beta1",
                "12.0.1-beta2",
                "12.0.1",
                "12.0.2-beta1",
                "12.0.2-beta2",
                "12.0.2-beta3",
                "12.0.2",
                "12.0.3-beta1",
                "12.0.3-beta2",
                "12.0.3",
                "13.0.1-beta1",
                "13.0.1-beta2",
                "13.0.1",
                "13.0.2-beta1",
                "13.0.2-beta2",
                "13.0.2-beta3",
                "13.0.2",
                "13.0.3-beta1",
                "13.0.3"
            ]
        }
        """;

    private const string CatalogUriResponse =
        """
        {
            "@id": "https://api.nuget.org/v3/registration5-semver1/newtonsoft.json/12.0.3.json",
            "@type": ["Package", "http://schema.nuget.org/catalog#Permalink"],
            "catalogEntry": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json",
            "listed": true,
            "packageContent": "https://api.nuget.org/v3-flatcontainer/newtonsoft.json/12.0.3/newtonsoft.json.12.0.3.nupkg",
            "published": "2019-11-09T01:27:30.723\u002B00:00",
            "registration": "https://api.nuget.org/v3/registration5-semver1/newtonsoft.json/index.json",
            "@context": {
                "@vocab": "http://schema.nuget.org/schema#",
                "xsd": "http://www.w3.org/2001/XMLSchema#",
                "catalogEntry": {
                "@type": "@id"
                },
                "registration": {
                "@type": "@id"
                },
                "packageContent": {
                "@type": "@id"
                },
                "published": {
                "@type": "xsd:dateTime"
                }
            }
        }
        """;

    private const string SupportedFrameworksResponse =
        """
        {
            "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json",
            "@type": ["PackageDetails", "catalog:Permalink"],
            "authors": "James Newton-King",
            "catalog:commitId": "0e847b40-06dc-481b-9dee-fd9e9991bee8",
            "catalog:commitTimeStamp": "2022-12-08T16:43:03.7673271Z",
            "copyright": "Copyright \u00A9 James Newton-King 2008",
            "created": "2019-11-09T01:27:30.723Z",
            "description": "Json.NET is a popular high-performance JSON framework for .NET",
            "iconFile": "packageIcon.png",
            "id": "Newtonsoft.Json",
            "isPrerelease": false,
            "lastEdited": "2022-12-08T16:42:44.683Z",
            "licenseExpression": "MIT",
            "licenseUrl": "https://licenses.nuget.org/MIT",
            "listed": true,
            "minClientVersion": "2.12",
            "packageHash": "aTRmXwR5xYu\u002BmWxE8r8W1DWnL02SeV8LwdQMsLwTWP8OZgrCCyTqvOAe5hRb1VNQYXjln7qr0PKpSyO/pcc19Q==",
            "packageHashAlgorithm": "SHA512",
            "packageSize": 2596051,
            "projectUrl": "https://www.newtonsoft.com/json",
            "published": "2019-11-09T01:27:30.723Z",
            "repository": "",
            "requireLicenseAcceptance": false,
            "title": "Json.NET",
            "verbatimVersion": "12.0.3",
            "version": "12.0.3",
            "dependencyGroups": [
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netframework2.0",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETFramework2.0"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netframework3.5",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETFramework3.5"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netframework4.0",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETFramework4.0"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netframework4.5",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETFramework4.5"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netportable0.0-profile259",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETPortable0.0-Profile259"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netportable0.0-profile328",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETPortable0.0-Profile328"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.0",
                "@type": "PackageDependencyGroup",
                "dependencies": [
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.0/microsoft.csharp",
                    "@type": "PackageDependency",
                    "id": "Microsoft.CSharp",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.0/netstandard.library",
                    "@type": "PackageDependency",
                    "id": "NETStandard.Library",
                    "range": "[1.6.1, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.0/system.componentmodel.typeconverter",
                    "@type": "PackageDependency",
                    "id": "System.ComponentModel.TypeConverter",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.0/system.runtime.serialization.primitives",
                    "@type": "PackageDependency",
                    "id": "System.Runtime.Serialization.Primitives",
                    "range": "[4.3.0, )"
                    }
                ],
                "targetFramework": ".NETStandard1.0"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3",
                "@type": "PackageDependencyGroup",
                "dependencies": [
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/microsoft.csharp",
                    "@type": "PackageDependency",
                    "id": "Microsoft.CSharp",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/netstandard.library",
                    "@type": "PackageDependency",
                    "id": "NETStandard.Library",
                    "range": "[1.6.1, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/system.componentmodel.typeconverter",
                    "@type": "PackageDependency",
                    "id": "System.ComponentModel.TypeConverter",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/system.runtime.serialization.formatters",
                    "@type": "PackageDependency",
                    "id": "System.Runtime.Serialization.Formatters",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/system.runtime.serialization.primitives",
                    "@type": "PackageDependency",
                    "id": "System.Runtime.Serialization.Primitives",
                    "range": "[4.3.0, )"
                    },
                    {
                    "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard1.3/system.xml.xmldocument",
                    "@type": "PackageDependency",
                    "id": "System.Xml.XmlDocument",
                    "range": "[4.3.0, )"
                    }
                ],
                "targetFramework": ".NETStandard1.3"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#dependencygroup/.netstandard2.0",
                "@type": "PackageDependencyGroup",
                "targetFramework": ".NETStandard2.0"
                }
            ],
            "packageEntries": [
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#LICENSE.md",
                "@type": "PackageEntry",
                "compressedLength": 658,
                "fullName": "LICENSE.md",
                "length": 1104,
                "name": "LICENSE.md"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#Newtonsoft.Json.nuspec",
                "@type": "PackageEntry",
                "compressedLength": 754,
                "fullName": "Newtonsoft.Json.nuspec",
                "length": 2487,
                "name": "Newtonsoft.Json.nuspec"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#packageIcon.png",
                "@type": "PackageEntry",
                "compressedLength": 8158,
                "fullName": "packageIcon.png",
                "length": 8956,
                "name": "packageIcon.png"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net20/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 221586,
                "fullName": "lib/net20/Newtonsoft.Json.dll",
                "length": 570792,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net20/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 48092,
                "fullName": "lib/net20/Newtonsoft.Json.xml",
                "length": 604413,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net35/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 203710,
                "fullName": "lib/net35/Newtonsoft.Json.dll",
                "length": 505776,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net35/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 43997,
                "fullName": "lib/net35/Newtonsoft.Json.xml",
                "length": 549505,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net40/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 223386,
                "fullName": "lib/net40/Newtonsoft.Json.dll",
                "length": 574376,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net40/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 44964,
                "fullName": "lib/net40/Newtonsoft.Json.xml",
                "length": 561424,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net45/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 263924,
                "fullName": "lib/net45/Newtonsoft.Json.dll",
                "length": 700336,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/net45/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 51508,
                "fullName": "lib/net45/Newtonsoft.Json.xml",
                "length": 707721,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard1.0/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 253613,
                "fullName": "lib/netstandard1.0/Newtonsoft.Json.dll",
                "length": 669104,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard1.0/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 50235,
                "fullName": "lib/netstandard1.0/Newtonsoft.Json.xml",
                "length": 686366,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard1.3/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 259976,
                "fullName": "lib/netstandard1.3/Newtonsoft.Json.dll",
                "length": 688040,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard1.3/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 50550,
                "fullName": "lib/netstandard1.3/Newtonsoft.Json.xml",
                "length": 694572,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard2.0/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 261338,
                "fullName": "lib/netstandard2.0/Newtonsoft.Json.dll",
                "length": 693680,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/netstandard2.0/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 51456,
                "fullName": "lib/netstandard2.0/Newtonsoft.Json.xml",
                "length": 706433,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/portable-net40\u002Bsl5\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 190162,
                "fullName": "lib/portable-net40\u002Bsl5\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.dll",
                "length": 468912,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/portable-net40\u002Bsl5\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 41273,
                "fullName": "lib/portable-net40\u002Bsl5\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.xml",
                "length": 519414,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/portable-net45\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.dll",
                "@type": "PackageEntry",
                "compressedLength": 253422,
                "fullName": "lib/portable-net45\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.dll",
                "length": 668584,
                "name": "Newtonsoft.Json.dll"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#lib/portable-net45\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.xml",
                "@type": "PackageEntry",
                "compressedLength": 50235,
                "fullName": "lib/portable-net45\u002Bwin8\u002Bwp8\u002Bwpa81/Newtonsoft.Json.xml",
                "length": 686366,
                "name": "Newtonsoft.Json.xml"
                },
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#.signature.p7s",
                "@type": "PackageEntry",
                "compressedLength": 18492,
                "fullName": ".signature.p7s",
                "length": 18492,
                "name": ".signature.p7s"
                }
            ],
            "tags": ["json"],
            "vulnerabilities": [
                {
                "@id": "https://api.nuget.org/v3/catalog0/data/2022.12.08.16.43.03/newtonsoft.json.12.0.3.json#vulnerability/GitHub/183985",
                "@type": "Vulnerability",
                "advisoryUrl": "https://github.com/advisories/GHSA-5crp-9r3c-p9vr",
                "severity": "2"
                }
            ],
            "@context": {
                "@vocab": "http://schema.nuget.org/schema#",
                "catalog": "http://schema.nuget.org/catalog#",
                "xsd": "http://www.w3.org/2001/XMLSchema#",
                "dependencies": {
                "@id": "dependency",
                "@container": "@set"
                },
                "dependencyGroups": {
                "@id": "dependencyGroup",
                "@container": "@set"
                },
                "packageEntries": {
                "@id": "packageEntry",
                "@container": "@set"
                },
                "packageTypes": {
                "@id": "packageType",
                "@container": "@set"
                },
                "supportedFrameworks": {
                "@id": "supportedFramework",
                "@container": "@set"
                },
                "tags": {
                "@id": "tag",
                "@container": "@set"
                },
                "vulnerabilities": {
                "@id": "vulnerability",
                "@container": "@set"
                },
                "published": {
                "@type": "xsd:dateTime"
                },
                "created": {
                "@type": "xsd:dateTime"
                },
                "lastEdited": {
                "@type": "xsd:dateTime"
                },
                "catalog:commitTimeStamp": {
                "@type": "xsd:dateTime"
                },
                "reasons": {
                "@container": "@set"
                }
            }
        }
        """;
}