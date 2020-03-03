## [15.0.4](https://github.com/informatievlaanderen/grar-common/compare/v15.0.3...v15.0.4) (2020-03-03)


### Bug Fixes

* bump netcore to 3.1.2 ([240bac0](https://github.com/informatievlaanderen/grar-common/commit/240bac0))

## [15.0.3](https://github.com/informatievlaanderen/grar-common/compare/v15.0.2...v15.0.3) (2020-02-26)


### Bug Fixes

* udate basisregisters api dependency ([230266a](https://github.com/informatievlaanderen/grar-common/commit/230266a))

## [15.0.2](https://github.com/informatievlaanderen/grar-common/compare/v15.0.1...v15.0.2) (2020-02-20)


### Bug Fixes

* force build ([6967256](https://github.com/informatievlaanderen/grar-common/commit/6967256))

## [15.0.1](https://github.com/informatievlaanderen/grar-common/compare/v15.0.0...v15.0.1) (2020-02-20)


### Bug Fixes

* wrap adding converters in a lock ([c0406e1](https://github.com/informatievlaanderen/grar-common/commit/c0406e1))

# [15.0.0](https://github.com/informatievlaanderen/grar-common/compare/v14.3.3...v15.0.0) (2020-02-17)


### Bug Fixes

* add PointGeometryCoordinate to have 2 decimals instead of 11 ([b512484](https://github.com/informatievlaanderen/grar-common/commit/b512484))
* correct parsing GeometryCoordinate on Culture ([81d3e7c](https://github.com/informatievlaanderen/grar-common/commit/81d3e7c))


### BREAKING CHANGES

* GeometryCoordinateValue is split into 2 classes PointGeometryCoordinateValue and
PolygonGeometryCoordinateValue

## [14.3.3](https://github.com/informatievlaanderen/grar-common/compare/v14.3.2...v14.3.3) (2020-02-11)


### Bug Fixes

* gml interior ring can be nullable ([58d0ad4](https://github.com/informatievlaanderen/grar-common/commit/58d0ad4))

## [14.3.2](https://github.com/informatievlaanderen/grar-common/compare/v14.3.1...v14.3.2) (2020-02-10)


### Bug Fixes

* json default value for nullable fields ([88b9ca1](https://github.com/informatievlaanderen/grar-common/commit/88b9ca1))

## [14.3.1](https://github.com/informatievlaanderen/grar-common/compare/v14.3.0...v14.3.1) (2020-02-03)


### Bug Fixes

* specify non nullable responses ([7529e54](https://github.com/informatievlaanderen/grar-common/commit/7529e54))

# [14.3.0](https://github.com/informatievlaanderen/grar-common/compare/v14.2.0...v14.3.0) (2020-02-01)


### Features

* upgrade netcoreapp31 and dependencies ([6901fc7](https://github.com/informatievlaanderen/grar-common/commit/6901fc7))

# [14.2.0](https://github.com/informatievlaanderen/grar-common/compare/v14.1.0...v14.2.0) (2020-01-29)


### Bug Fixes

* correct namespace ([75e8735](https://github.com/informatievlaanderen/grar-common/commit/75e8735))
* remove implicit cast from geometrycoordinatevalue ([bdc1f77](https://github.com/informatievlaanderen/grar-common/commit/bdc1f77))
* remove unused field ([617d799](https://github.com/informatievlaanderen/grar-common/commit/617d799))


### Features

* add geometrycoordinatevalue ([13cecac](https://github.com/informatievlaanderen/grar-common/commit/13cecac))
* add json serialization for points and polygons ([c958e34](https://github.com/informatievlaanderen/grar-common/commit/c958e34))

# [14.1.0](https://github.com/informatievlaanderen/grar-common/compare/v14.0.1...v14.1.0) (2019-12-25)


### Features

* add from/to bytes for shapecontent DC-144 ([df00c8e](https://github.com/informatievlaanderen/grar-common/commit/df00c8e))

## [14.0.1](https://github.com/informatievlaanderen/grar-common/compare/v14.0.0...v14.0.1) (2019-12-20)

# [14.0.0](https://github.com/informatievlaanderen/grar-common/compare/v13.2.1...v14.0.0) (2019-12-17)


### Code Refactoring

* upgrade to netcoreapp31 ([9bf3731](https://github.com/informatievlaanderen/grar-common/commit/9bf3731))


### BREAKING CHANGES

* Upgrade to .NET Core 3.1

## [13.2.1](https://github.com/informatievlaanderen/grar-common/compare/v13.2.0...v13.2.1) (2019-12-13)


### Bug Fixes

* increase timeout checking idempotency commands ([1aa58f1](https://github.com/informatievlaanderen/grar-common/commit/1aa58f1))

# [13.2.0](https://github.com/informatievlaanderen/grar-common/compare/v13.1.1...v13.2.0) (2019-11-28)


### Features

* add datetimeoffset extensions for extracts ([149c438](https://github.com/informatievlaanderen/grar-common/commit/149c438))

## [13.1.1](https://github.com/informatievlaanderen/grar-common/compare/v13.1.0...v13.1.1) (2019-11-25)


### Bug Fixes

* paket.template include upgrade previous commit ([202ec23](https://github.com/informatievlaanderen/grar-common/commit/202ec23))

# [13.1.0](https://github.com/informatievlaanderen/grar-common/compare/v13.0.5...v13.1.0) (2019-11-25)


### Features

* upgrade API package ([65a62e3](https://github.com/informatievlaanderen/grar-common/commit/65a62e3))

## [13.0.5](https://github.com/informatievlaanderen/grar-common/compare/v13.0.4...v13.0.5) (2019-11-08)


### Bug Fixes

* clarify identificator GR-1036 ([7b68d8d](https://github.com/informatievlaanderen/grar-common/commit/7b68d8d))
* clarify spatial GR-1046 ([6ecd014](https://github.com/informatievlaanderen/grar-common/commit/6ecd014))

## [13.0.4](https://github.com/informatievlaanderen/grar-common/compare/v13.0.3...v13.0.4) (2019-10-25)


### Bug Fixes

* add docs for geojson and gml point ([f8e1a79](https://github.com/informatievlaanderen/grar-common/commit/f8e1a79))

## [13.0.3](https://github.com/informatievlaanderen/grar-common/compare/v13.0.2...v13.0.3) (2019-10-24)


### Bug Fixes

* add xml for homoniemtoevoeging and fix gebouweenheidid ([6909fe1](https://github.com/informatievlaanderen/grar-common/commit/6909fe1))

## [13.0.2](https://github.com/informatievlaanderen/grar-common/compare/v13.0.1...v13.0.2) (2019-10-24)

## [13.0.1](https://github.com/informatievlaanderen/grar-common/compare/v13.0.0...v13.0.1) (2019-10-24)


### Bug Fixes

* still allow nullable versions in ctor ([240fa94](https://github.com/informatievlaanderen/grar-common/commit/240fa94))

# [13.0.0](https://github.com/informatievlaanderen/grar-common/compare/v12.1.4...v13.0.0) (2019-10-24)


### Bug Fixes

* generate xml docs for provenance ([71d0954](https://github.com/informatievlaanderen/grar-common/commit/71d0954))
* versie cannot be nullable ([e7af898](https://github.com/informatievlaanderen/grar-common/commit/e7af898))


### BREAKING CHANGES

* Versie is no longer nullable

## [12.1.4](https://github.com/informatievlaanderen/grar-common/compare/v12.1.3...v12.1.4) (2019-10-24)


### Bug Fixes

* provide xml documentation ([adbef40](https://github.com/informatievlaanderen/grar-common/commit/adbef40))

## [12.1.3](https://github.com/informatievlaanderen/grar-common/compare/v12.1.2...v12.1.3) (2019-10-21)


### Bug Fixes

* properly case language value ([d94866e](https://github.com/informatievlaanderen/grar-common/commit/d94866e))

## [12.1.2](https://github.com/informatievlaanderen/grar-common/compare/v12.1.1...v12.1.2) (2019-09-26)


### Bug Fixes

* correct namespaces in building unit legacy ([47ca945](https://github.com/informatievlaanderen/grar-common/commit/47ca945))

## [12.1.1](https://github.com/informatievlaanderen/grar-common/compare/v12.1.0...v12.1.1) (2019-09-19)


### Bug Fixes

* correct package depedency ([0c0a737](https://github.com/informatievlaanderen/grar-common/commit/0c0a737))

# [12.1.0](https://github.com/informatievlaanderen/grar-common/compare/v12.0.0...v12.1.0) (2019-09-19)


### Features

* upgrade shaperon ([c544b10](https://github.com/informatievlaanderen/grar-common/commit/c544b10))

# [12.0.0](https://github.com/informatievlaanderen/grar-common/compare/v11.1.1...v12.0.0) (2019-09-18)


### Features

* upgdated paket.template dependencies ([6118766](https://github.com/informatievlaanderen/grar-common/commit/6118766))
* upgrade nts to 2.0.0 ([6826c43](https://github.com/informatievlaanderen/grar-common/commit/6826c43))


### BREAKING CHANGES

* upgrade NTS

## [11.1.1](https://github.com/informatievlaanderen/grar-common/compare/v11.1.0...v11.1.1) (2019-09-03)


### Bug Fixes

* correctly serialise point and polygon to xml ([43a0449](https://github.com/informatievlaanderen/grar-common/commit/43a0449))

# [11.1.0](https://github.com/informatievlaanderen/grar-common/compare/v11.0.0...v11.1.0) (2019-08-22)


### Features

* bump to .net 2.2.6 ([c33b979](https://github.com/informatievlaanderen/grar-common/commit/c33b979))

# [11.0.0](https://github.com/informatievlaanderen/grar-common/compare/v10.0.1...v11.0.0) (2019-08-16)


### Features

* add setting to wait for user input or not ([2c70a4d](https://github.com/informatievlaanderen/grar-common/commit/2c70a4d))


### BREAKING CHANGES

* Add WaitForUserInput to ICommandProcessorConfig

## [10.0.1](https://github.com/informatievlaanderen/grar-common/compare/v10.0.0...v10.0.1) (2019-06-20)


### Bug Fixes

* add datamember to linear ring ([f7fff57](https://github.com/informatievlaanderen/grar-common/commit/f7fff57))

# [10.0.0](https://github.com/informatievlaanderen/grar-common/compare/v9.0.1...v10.0.0) (2019-06-20)


### Bug Fixes

* configure import feed ([1421d7e](https://github.com/informatievlaanderen/grar-common/commit/1421d7e))


### BREAKING CHANGES

* changed configuration calls

GetExecutionAssembly() returns package assembly and not the assembly of
the executing app

## [9.0.1](https://github.com/informatievlaanderen/grar-common/compare/v9.0.0...v9.0.1) (2019-06-19)


### Bug Fixes

* replace import feed id by making name lowercase ([6b4d7f2](https://github.com/informatievlaanderen/grar-common/commit/6b4d7f2))

# [9.0.0](https://github.com/informatievlaanderen/grar-common/compare/v8.2.0...v9.0.0) (2019-06-19)


### Bug Fixes

* fixing naming ([df95878](https://github.com/informatievlaanderen/grar-common/commit/df95878))


### BREAKING CHANGES

* - rename configuration calls to be inline with excisting calls
- changed type of LastStatusFor(0ImportFeed) so the ef record type is
not passed

-- Breaking change required as the last  breaking change was not picked
up by sematic versioning --

# [8.2.0](https://github.com/informatievlaanderen/grar-common/compare/v8.1.0...v8.2.0) (2019-06-18)


### Bug Fixes

* add route parameter import feed name for get status call ([1c8627f](https://github.com/informatievlaanderen/grar-common/commit/1c8627f))
* re-generate batch status migrations ([36d4608](https://github.com/informatievlaanderen/grar-common/commit/36d4608))


### Features

* add import feed to batch status ([5d698ab](https://github.com/informatievlaanderen/grar-common/commit/5d698ab))

# [8.1.0](https://github.com/informatievlaanderen/grar-common/compare/v8.0.1...v8.1.0) (2019-06-18)


### Features

* add classes for syndication polygon and point ([8cf1357](https://github.com/informatievlaanderen/grar-common/commit/8cf1357))

## [8.0.1](https://github.com/informatievlaanderen/grar-common/compare/v8.0.0...v8.0.1) (2019-06-11)


### Bug Fixes

* change reason valueobject to primitive in ProvenanceData ([f10d1ac](https://github.com/informatievlaanderen/grar-common/commit/f10d1ac))

# [8.0.0](https://github.com/informatievlaanderen/grar-common/compare/v7.2.2...v8.0.0) (2019-06-07)


### Code Refactoring

* change plan to reason and made a valueobject for it ([e47f6e0](https://github.com/informatievlaanderen/grar-common/commit/e47f6e0))


### BREAKING CHANGES

* change Plan (enum) to Reason (valueobject)

## [7.2.2](https://github.com/informatievlaanderen/grar-common/compare/v7.2.1...v7.2.2) (2019-06-03)


### Bug Fixes

* rerun semantic release due to github borked ([e9839d9](https://github.com/informatievlaanderen/grar-common/commit/e9839d9))

## [7.2.1](https://github.com/informatievlaanderen/grar-common/compare/v7.2.0...v7.2.1) (2019-06-03)


### Bug Fixes

* create HttpApiProxy base class ([862a330](https://github.com/informatievlaanderen/grar-common/commit/862a330))

# [7.2.0](https://github.com/informatievlaanderen/grar-common/compare/v7.1.0...v7.2.0) (2019-05-30)


### Features

* use default history table for crab import migrations ([34b97bd](https://github.com/informatievlaanderen/grar-common/commit/34b97bd))

# [7.1.0](https://github.com/informatievlaanderen/grar-common/compare/v7.0.0...v7.1.0) (2019-05-23)


### Features

* add crab import setup infrastructure ([52a7563](https://github.com/informatievlaanderen/grar-common/commit/52a7563))

# [7.0.0](https://github.com/informatievlaanderen/grar-common/compare/v6.2.1...v7.0.0) (2019-05-21)


### Bug Fixes

* create batch configuration interface ([bd7ba94](https://github.com/informatievlaanderen/grar-common/commit/bd7ba94))
* extract building options from CommandProcessorBuilder ([8bef304](https://github.com/informatievlaanderen/grar-common/commit/8bef304))
* remove BuildAndRun ([67216ca](https://github.com/informatievlaanderen/grar-common/commit/67216ca))
* remove Import.Xunit ([dba18f4](https://github.com/informatievlaanderen/grar-common/commit/dba18f4))
* use import batch status instead of last imported and recovery ([78cdb60](https://github.com/informatievlaanderen/grar-common/commit/78cdb60))


### Features

* add crab import status ([68873f5](https://github.com/informatievlaanderen/grar-common/commit/68873f5))
* add initialise/finalise import to apiproxy ([5c5976e](https://github.com/informatievlaanderen/grar-common/commit/5c5976e))
* add use ApiProxyFactory with builder ([baf0817](https://github.com/informatievlaanderen/grar-common/commit/baf0817))
* initialise/finalise batch based status from api ([abfddd1](https://github.com/informatievlaanderen/grar-common/commit/abfddd1))


### BREAKING CHANGES

* CHANGE
remove build and run function as fist step in splitting creating the
batch parameters from building the commandprocessor
* remove project
Import.Xunit is not used in any registry.
Removing it reduces the complextity of refactoring the Grar.Ipmort for
udates scenarios

## [6.2.1](https://github.com/informatievlaanderen/grar-common/compare/v6.2.0...v6.2.1) (2019-04-30)


### Bug Fixes

* correct nuget dependencies ([5216759](https://github.com/informatievlaanderen/grar-common/commit/5216759))

# [6.2.0](https://github.com/informatievlaanderen/grar-common/compare/v6.1.2...v6.2.0) (2019-04-30)


### Features

* update dependencies ([2d93da8](https://github.com/informatievlaanderen/grar-common/commit/2d93da8))

## [6.1.2](https://github.com/informatievlaanderen/grar-common/compare/v6.1.1...v6.1.2) (2019-04-26)

## [6.1.1](https://github.com/informatievlaanderen/grar-common/compare/v6.1.0...v6.1.1) (2019-04-23)


### Bug Fixes

* correct paket.template for Extract ([3a12bcc](https://github.com/informatievlaanderen/grar-common/commit/3a12bcc))

# [6.1.0](https://github.com/informatievlaanderen/grar-common/compare/v6.0.2...v6.1.0) (2019-04-23)


### Features

* upgraded packages ([7bf3e9e](https://github.com/informatievlaanderen/grar-common/commit/7bf3e9e))

## [6.0.2](https://github.com/informatievlaanderen/grar-common/compare/v6.0.1...v6.0.2) (2019-04-17)

## [6.0.1](https://github.com/informatievlaanderen/grar-common/compare/v6.0.0...v6.0.1) (2019-04-16)

# [6.0.0](https://github.com/informatievlaanderen/grar-common/compare/v5.3.1...v6.0.0) (2019-04-11)


### Features

* add overload provenancepipe + add position IIdempotentCHModule ([7349815](https://github.com/informatievlaanderen/grar-common/commit/7349815))


### BREAKING CHANGES

* add position to IIdempotentCommandHandlerModule

## [5.3.1](https://github.com/informatievlaanderen/grar-common/compare/v5.3.0...v5.3.1) (2019-03-04)

# [5.3.0](https://github.com/informatievlaanderen/grar-common/compare/v5.2.0...v5.3.0) (2019-03-04)


### Features

* add CaPaKey ValueObject ([e849293](https://github.com/informatievlaanderen/grar-common/commit/e849293))

# [5.2.0](https://github.com/informatievlaanderen/grar-common/compare/v5.1.1...v5.2.0) (2019-03-04)


### Features

* add transform possibility for dbfile, add shp and shx files ([9da723c](https://github.com/informatievlaanderen/grar-common/commit/9da723c))

## [5.1.1](https://github.com/informatievlaanderen/grar-common/compare/v5.1.0...v5.1.1) (2019-02-28)


### Bug Fixes

* update metadatatests to capture events ([54d823c](https://github.com/informatievlaanderen/grar-common/commit/54d823c))

# [5.1.0](https://github.com/informatievlaanderen/grar-common/compare/v5.0.1...v5.1.0) (2019-02-28)


### Features

* add interior rings for gml polygon ([4ef38cf](https://github.com/informatievlaanderen/grar-common/commit/4ef38cf))

## [5.0.1](https://github.com/informatievlaanderen/grar-common/compare/v5.0.0...v5.0.1) (2019-02-26)


### Bug Fixes

* make AddProvenance public ([224110a](https://github.com/informatievlaanderen/grar-common/commit/224110a))

# [5.0.0](https://github.com/informatievlaanderen/grar-common/compare/v4.4.0...v5.0.0) (2019-02-26)


### Code Refactoring

* remove ProvenanceCommandHandlerModule ([fd85d7e](https://github.com/informatievlaanderen/grar-common/commit/fd85d7e))


### BREAKING CHANGES

* ProvenanceCommandHandlerModule is removed, in favor of composition with pipes.

# [4.4.0](https://github.com/informatievlaanderen/grar-common/compare/v4.3.0...v4.4.0) (2019-02-26)


### Features

* add legacy building and buildingunit types ([df35fd2](https://github.com/informatievlaanderen/grar-common/commit/df35fd2))

# [4.3.0](https://github.com/informatievlaanderen/grar-common/compare/v4.2.0...v4.3.0) (2019-02-25)


### Features

* add generic extract builder functionality ([f0c63ea](https://github.com/informatievlaanderen/grar-common/commit/f0c63ea))

# [4.2.0](https://github.com/informatievlaanderen/grar-common/compare/v4.1.0...v4.2.0) (2019-02-25)


### Features

* add common extract infrastructure ([0d5f21c](https://github.com/informatievlaanderen/grar-common/commit/0d5f21c))

# [4.1.0](https://github.com/informatievlaanderen/grar-common/compare/v4.0.1...v4.1.0) (2019-02-25)


### Features

* fix the provenance pipe order ([240a041](https://github.com/informatievlaanderen/grar-common/commit/240a041))

## [4.0.1](https://github.com/informatievlaanderen/grar-common/compare/v4.0.0...v4.0.1) (2019-02-06)


### Bug Fixes

* add nuget references to dependencies ([2412d99](https://github.com/informatievlaanderen/grar-common/commit/2412d99))

# [4.0.0](https://github.com/informatievlaanderen/grar-common/compare/v3.1.3...v4.0.0) (2019-02-06)


### Code Refactoring

* move rfc3339 datetimeoffset to seperate repo ([bd90c6c](https://github.com/informatievlaanderen/grar-common/commit/bd90c6c))


### BREAKING CHANGES

* Rfc3339DateTimeOffset is now part of Be.Vlaanderen.Basisregisters.Utilities.Rfc3339DateTimeOffset

## [3.1.3](https://github.com/informatievlaanderen/grar-common/compare/v3.1.2...v3.1.3) (2019-02-06)


### Bug Fixes

* create converter for serialization issues with rfcdatetimeoffset ([20ac089](https://github.com/informatievlaanderen/grar-common/commit/20ac089))

## [3.1.2](https://github.com/informatievlaanderen/grar-common/compare/v3.1.1...v3.1.2) (2019-02-04)

## [3.1.1](https://github.com/informatievlaanderen/grar-common/compare/v3.1.0...v3.1.1) (2019-02-01)


### Bug Fixes

* use empty namespace for xml serialisation ([a21a8ca](https://github.com/informatievlaanderen/grar-common/commit/a21a8ca))

# [3.1.0](https://github.com/informatievlaanderen/grar-common/compare/v3.0.0...v3.1.0) (2019-02-01)


### Features

* identificator now serialises as an rfc3339 date ([e637bb2](https://github.com/informatievlaanderen/grar-common/commit/e637bb2))

# [3.0.0](https://github.com/informatievlaanderen/grar-common/compare/v2.0.1...v3.0.0) (2019-01-08)


### Code Refactoring

* change dependency between common and provenance ([aa43117](https://github.com/informatievlaanderen/grar-common/commit/aa43117))


### BREAKING CHANGES

* Common no longer depends on Provenance, it is the other way around.

## [2.0.1](https://github.com/informatievlaanderen/grar-common/compare/v2.0.0...v2.0.1) (2019-01-07)

# [2.0.0](https://github.com/informatievlaanderen/grar-common/compare/v1.0.0...v2.0.0) (2019-01-07)


### Features

* add agnostic version number overload to CrabProvenanceFactory ([201f574](https://github.com/informatievlaanderen/grar-common/commit/201f574))
* change version identifier to DateTimeOffset ([e734676](https://github.com/informatievlaanderen/grar-common/commit/e734676))


### BREAKING CHANGES

* change versieId for Bosa Identifier to DateTimeOffset

# 1.0.0 (2018-12-26)


### Features

* open source with EUPL-1.2 license as 'agentschap Informatie Vlaanderen' ([ea15a58](https://github.com/informatievlaanderen/grar-common/commit/ea15a58))
