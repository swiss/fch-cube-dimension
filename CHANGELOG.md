# Changelog

## Version 2.0.0
BREAKING CHANGE: multiple renamings
- IDimensionService.CreateDimension() -> IDimensionService.CreateTriples()
- DimensionItem.AdditionalLingualProperties -> DimensionItem.AdditionalLiteralProperties
- AdditionalLingualProperty -> AdditionalLiteralProperty
- LingualLiteral -> Literal

Static code analysis (Roslyn rules) enabled.

## Version 1.0.9
feat: introduce Uri parameter to LingualLiteral

## Version 1.0.8
chore: changed namespace to Swiss.FCh

## Version 1.0.7
chore: syntax of markdown files (README.md and CHANGELOG.md) corrected

## Version 1.0.6
BREAKING CHANGE: Namespaces of contract and extension methods corrected for consistency

## Version 1.0.5
feat: README.md included in the NuGet package

## Version 1.0.4
fix(pipeline): false release configuration parameter removed from 'nuget push' command

## Version 1.0.3
fix(pipeline): duplicate release configuration removed from 'nuget pack' command

## Version 1.0.2
feat(pipeline): applying build configuration in release mode consistently across all dotnet CLI commands
chore: all namespaces renamed from 'Bk' to 'FCh'

## Version 1.0.1
fix(pipeline): solution file specyfied for correct execution of dotnet cli commands

## Version 1.0.0
Initial publication on GitHub