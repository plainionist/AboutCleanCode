
namespace Specification

open System.Reflection
open Athena.TDK
open NUnit.Framework

[<TestFixture>]
type ``Highlight accepted BestEffort work items``() = 
    inherit AbstractFeatures()

    static member Scenarios = AbstractFeatures.GetScenarios(Assembly.GetExecutingAssembly(), "AcceptingBestEffortImprovements.feature")

