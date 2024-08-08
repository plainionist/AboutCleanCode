open System
open System.Diagnostics
open System.IO
open System.Runtime.InteropServices

let exec workspace (cmd:string) (args:string) =
    printfn "%s> %s %s" workspace cmd args

    let info = new ProcessStartInfo(cmd, args)
    info.UseShellExecute <- false
    info.WorkingDirectory <- workspace

    use p = Process.Start(info)
    p.WaitForExit()
    if p.ExitCode <> 0 then failwith "FAILED"
  
let openBrowser file =
    let info = new ProcessStartInfo()
    info.UseShellExecute <- true

    if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then
        info.FileName <- file
    elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then
        info.FileName <- "xdg-open"
        info.Arguments <- file
    elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then
        info.FileName <- "open"
        info.Arguments <- file
    else
        failwith "Unsupported OS platform"

    Process.Start(info) |> ignore

let install() =
  exec "." "dotnet" "tool install --global dotnet-coverage"
  exec "." "dotnet" "tool install --global dotnet-reportgenerator-globaltool"

let collect workspace =
  exec workspace "dotnet" "build"

  exec workspace "dotnet-coverage" "collect --output-format cobertura --output coverage.xml dotnet test --no-build"

  exec workspace "reportgenerator" "-reports:coverage.xml -targetdir:CoverageReport -reporttypes:Html"

  Path.Combine("CoverageReport", "index.html")

if fsi.CommandLineArgs.Length < 2 then
  Console.Error.WriteLine("Specify 'install' to install required tools")
  Console.Error.WriteLine("OR a workspace to create a coverage report")
  exit 1

if fsi.CommandLineArgs.[1].Equals("install", StringComparison.OrdinalIgnoreCase) then
  install()
else
  // get root of project where we expect the project solution
  let workspace = fsi.CommandLineArgs.[1]

  workspace |> collect |> openBrowser
