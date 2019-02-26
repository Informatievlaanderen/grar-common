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
