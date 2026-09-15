# Deferred Engine code

These interfaces, provider, and placeholder implementation were moved from the former general library to preserve startup registration and supported-version checks. Combat resolution still returns an empty result.

Keep this code together until the design of `OldWorld.Engine` is agreed. Do not introduce the Engine project or implement new gameplay behavior as part of the architecture migration.
