#r "paket:
version 7.0.2
framework: net6.0
source https://api.nuget.org/v3/index.json
nuget Be.Vlaanderen.Basisregisters.Build.Pipeline 6.0.3 //"

#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO.FileSystemOperators
open ``Build-generic``

let assemblyVersionNumber = (sprintf "%s.0")
let nugetVersionNumber = (sprintf "%s")

let buildSource = build assemblyVersionNumber
let buildTest = buildTest assemblyVersionNumber
let publishSource = publish assemblyVersionNumber
let pack = packSolution nugetVersionNumber

supportedRuntimeIdentifiers <- [ "linux-x64" ]

// Library ------------------------------------------------------------------------
Target.create "Lib_Build" (fun _ ->
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Common"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Contracts"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Extracts"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Import"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Legacy"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Oslo"
    buildSource "Be.Vlaanderen.Basisregisters.GrAr.Provenance"
    buildTest "Be.Vlaanderen.Basisregisters.GrAr.Tests"
)

Target.create "Lib_Test" (fun _ ->
    [
        "test" @@ "Be.Vlaanderen.Basisregisters.GrAr.Tests"
    ] |> List.iter testWithDotNet
)

Target.create "Lib_Publish" (fun _ ->
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Common"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Contracts"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Extracts"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Import"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Legacy"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Oslo"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Provenance"
)

Target.create "Lib_Pack" (fun _ -> pack "Be.Vlaanderen.Basisregisters.GrAr")

// --------------------------------------------------------------------------------
Target.create "PublishAll" ignore
Target.create "PackageAll" ignore

// Publish ends up with artifacts in the build folder
"DotNetCli"
==> "Clean"
==> "Restore"
==> "Lib_Build"
==> "Lib_Test"
==> "Lib_Publish"
==> "PublishAll"

// Package ends up with local NuGet packages
"PublishAll"
==> "Lib_Pack"
==> "PackageAll"

Target.runOrDefault "Lib_Test"
