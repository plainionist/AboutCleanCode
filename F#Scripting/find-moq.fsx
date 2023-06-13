
open System
open System.IO

let referencesMoq (file:string) =
    File.ReadAllLines(file)
    |> Seq.exists(fun line -> line.Contains("\"Moq\""))

let isUnitTest (file:string) =
    file.IndexOf("_uTest.", StringComparison.OrdinalIgnoreCase) <> -1

Directory.EnumerateFiles(".", "*.csproj", SearchOption.AllDirectories)
|> Seq.map Path.GetFullPath
|> Seq.filter referencesMoq
|> Seq.filter (isUnitTest >> not)
|> Seq.iter (printfn "%s")

