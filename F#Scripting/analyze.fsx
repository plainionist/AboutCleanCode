#r "nuget: Plotly.NET, 4.0.0"

open System
open System.IO
// https://plotly.com/fsharp/
open Plotly.NET

let file = fsi.CommandLineArgs[1]

// [2023-05-16 15:30:38 INF] FTrace.RawData.TestCaseReader: GetTestCases(CodeBase: JupiterV2, SourceFile: \\andariel\transfer2\TCDataCollector\ReadyForImport\ATeam\1416183__TSE_sw_serv_service_logs_20230414_044519.7z)

File.ReadAllLines(file)
|> Seq.map(fun line -> line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
|> Seq.map(fun tokens -> tokens[3].TrimEnd(':'))
|> Seq.groupBy(fun x -> x)
|> Seq.map(fun (componentName, lines) -> componentName, lines |> Seq.length)
|> Seq.sortBy snd
|> List.ofSeq
|> List.unzip
|> fun (componentNames, counts) ->
    Chart.Bar(counts, componentNames)
    |> Chart.show
    
