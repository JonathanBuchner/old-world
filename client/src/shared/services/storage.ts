export type StorageKind = "local" | "session";

export function readStorage(kind: StorageKind, key: string): string | null {
  try {
    return window[kind === "local" ? "localStorage" : "sessionStorage"].getItem(key);
  } catch {
    return null;
  }
}

export function writeStorage(kind: StorageKind, key: string, value: string): void {
  try {
    window[kind === "local" ? "localStorage" : "sessionStorage"].setItem(key, value);
  } catch {
    // Persistence is optional; disabled storage must not prevent using the page.
  }
}
