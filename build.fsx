#r "paket:
version 7.0.2
framework: net6.0
source https://api.nuget.org/v3/index.json
nuget Be.Vlaanderen.Basisregisters.Build.Pipeline 6.0.5 //"

#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO.FileSystemOperators
open ``Build-generic``

let assemblyVersionNumber = (sprintf "%s.0")
let nugetVersionNumber = (sprintf "%s")

let buildSolution = buildSolution assemblyVersionNumber
let buildSource = build assemblyVersionNumber
let buildTest = buildTest assemblyVersionNumber
let publishSource = publish assemblyVersionNumber
let test = testSolution
let pack = packSolution nugetVersionNumber

supportedRuntimeIdentifiers <- [ "linux-x64" ]

// Library ------------------------------------------------------------------------
Target.create "Lib_Build" (fun _ -> buildSolution "Be.Vlaanderen.Basisregisters.GrAr")

Target.create "Lib_Test" (fun _ -> test "Be.Vlaanderen.Basisregisters.GrAr")

Target.create "Lib_Publish" (fun _ ->
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Common"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Contracts"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Edit"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Extracts"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Import"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Legacy"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Notifications"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Oslo"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Provenance"
    publishSource "Be.Vlaanderen.Basisregisters.GrAr.Provenance.AcmIdm"
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
