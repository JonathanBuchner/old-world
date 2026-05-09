- Prefer `var` for local C# declarations.
- Prefer definitions to be one line. Do not put each parameter on it's own line.
- Put a space after casts before the value, for example `return (double) Numerator / (double) Denominator;`.
- Fractions with zeros: For addition, adding a fraction with zero should just be ignored.  For multipling fractions with at least one having a denominator with the value 0, should just set the fraction to 0/1.

- Do not update NuGet package versions unless explicitly asked.
- Do not run restore unless explicitly asked.
- Do not fix unrelated compiler errors unless asked.

- If a command fails outside the requested scope, report it and stop.
