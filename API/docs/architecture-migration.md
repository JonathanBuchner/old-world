# Architecture migration

## Agreed scope

Move the solution to `API/OldWorld.sln`, with matching project folders, assembly names, and namespaces. Preserve existing behavior and NuGet package versions. Model redesign and database updates are separate work; database updates remain the user's responsibility.

## Code destinations

| Existing responsibility | Destination |
| --- | --- |
| Game, army, model, equipment, spell and rule types; dice; reference rule data | `OldWorld.Domain` |
| Catalog contracts and storage-facing interfaces | `OldWorld.Domain` |
| Fractions, probability results, calculator interface and calculation exceptions | `OldWorld.Calculator` |
| Existing Renegade 1.5.2 calculations | `OldWorld.Calculator/Versions/OldWorldV152Renegade` |
| Existing odds tests | `OldWorld.Calculator.Tests` |
| Blob storage, catalog loading/storage, external import parsing | `OldWorld.Infrastructure` |
| HTTP endpoints, request coordination, middleware, host configuration | `OldWorld.Api` |
| Army-building validation | `OldWorld.Validation` (initially empty; no existing army validator) |
| Existing Engine scaffolding | `OldWorld.Api/Application/GameRules/DeferredEngine` pending a future Engine project |

Configuration and import-format checks remain with the components they protect. They are not army validation. Calculator and Validation will support additional versions internally; this migration adds no new rules behavior.

## Chunks

1. Completed: establish the baseline and map code destinations.
2. Completed: rename the solution and establish Domain and Calculator. Release build succeeded; the original 59 passing tests and single known failure were preserved.
3. Completed: separate Infrastructure and Validation and connect them through API. Added Domain and API migration tests.
4. Completed: update documentation and verify dependencies, naming, data preservation, builds, and tests.

## Baseline (before migration)

- Release solution build succeeds.
- Existing calculator suite: **59 passed, 1 failed, 60 total**.
- Known failure: `ChanceToWound_tests.Subtract1Attr_4_4`, expected regular-hit numerator `1`, actual `2`. Preserve this test and calculation unchanged; the user approved proceeding with this known failure. No additional failures are acceptable.
- Existing restore/build warning: `NU1903` for transitive `Microsoft.OpenApi` 2.4.1. Package updates are outside this migration.
- The existing Dark Elves import sample produces three validation messages for a mount's `armyComposition` and `mounts`. A comparison against the original compiled parser confirmed identical messages after extraction; this migration preserves them.

## Final verification

- `dotnet build API/OldWorld.sln --configuration Release --no-restore`: succeeds.
- `dotnet test API/OldWorld.sln --configuration Release --no-build --no-restore`: **70 passed, 1 known failure, 71 total**. Calculator: 59 passed / 1 known failure; Domain: 2 passed; API: 9 passed.
- All 11 new tests pass. They cover existing points/serialization behavior, service resolution, shared catalog initialization, catalog serialization and errors, cancellation forwarding, and import response messages.
- API tests use in-memory Blob storage. Production Azure connectivity and database operations were not exercised; no database migration was created or applied.
- The project-reference graph matches the README. Domain has no package or project dependencies; Calculator, Validation, and Infrastructure reference only Domain. Source namespaces match their folders.
- All direct NuGet package versions match the baseline. Catalog data, JSON fixtures, appsettings, launch settings, and binary documentation are byte-for-byte unchanged.
- Reviewed source changes beyond moves/namespaces: dependency registration and configuration binding were split between API and Infrastructure; import exceptions now carry strings which API converts to the existing response shape; catalog exceptions now belong to Domain.
- Local documentation links have been checked. The default HTTP sample now uses the existing admin ping endpoint.

## Local checkout notes

Open `API/OldWorld.sln` instead of the former `ow_api/ow_api.sln`. Ignored Visual Studio and build caches may leave an `ow_api` directory on an existing checkout; it no longer contains project source or the solution. Those local caches are not part of the migrated source tree.
