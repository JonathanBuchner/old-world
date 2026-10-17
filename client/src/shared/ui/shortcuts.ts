import { closeAllModals } from "./modal";
import type { Cleanup } from "./lifecycle";

export function initGlobalShortcuts(): Cleanup {
  const controller = new AbortController();
  document.addEventListener(
    "keydown",
    (event) => {
      if (event.key === "Escape") closeAllModals();
    },
    { signal: controller.signal },
  );
  return () => controller.abort();
}
