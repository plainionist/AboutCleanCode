namespace Athena.TDK

open System
open System.Reflection
open System.Runtime.ExceptionServices
open System.Text.RegularExpressions
open TickSpec
open NUnit.Framework

// https://github.com/fsprojects/TickSpec/blob/master/Examples/ByFeature/FunctionalInjection/NunitWiring.fs
type AbstractFeatures() =

    static member GetScenarios(assembly:Assembly, ?featureSource) =
        let createTestCaseData (feature:Feature) (scenario:Scenario) =
            let scenarioName =
                scenario.Parameters
                |> Seq.fold (fun (acc:string) p -> acc.Replace("<" + fst p + ">", snd p)) scenario.Name
                |> fun x -> Regex.Replace(x, "^Scenario: ", "")

            (new TestCaseData(scenario))
                .SetName(scenarioName)
                .SetProperty("Feature", feature.Name)
            |> Seq.foldBack(fun (tag:string) data -> data.SetProperty("Tag", tag)) scenario.Tags

        let definitions = new StepDefinitions(assembly.GetTypes())

        let createFeature (featureFile:string) =
            let feature = definitions.GenerateFeature(featureFile, assembly.GetManifestResourceStream(featureFile))
            feature.Scenarios
            |> Seq.map (createTestCaseData feature)

        let featureFilter = 
            match featureSource with
            | None -> fun _ -> true
            | Some x -> fun (resource:string) -> resource.EndsWith("Specs." + x, StringComparison.OrdinalIgnoreCase)

        assembly.GetManifestResourceNames()
        |> Seq.filter(fun x -> x.EndsWith(".feature", StringComparison.OrdinalIgnoreCase))
        |> Seq.filter featureFilter
        |> Seq.collect createFeature
        |> List.ofSeq

    [<TestCaseSource("Scenarios")>]
    member this.Bdd (scenario:Scenario) = 
        if scenario.Tags |> Seq.exists ((=) "ignore") then
            raise (new IgnoreException("Ignored: " + scenario.ToString()))
        try
            scenario.Action.Invoke()
        with
        | :? TargetInvocationException as ex -> ExceptionDispatchInfo.Capture(ex.InnerException).Throw()



