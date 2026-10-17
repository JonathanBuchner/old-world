import { readStorage, writeStorage } from "../../../../shared/services/storage";
import type { Cleanup } from "../../../../shared/ui/lifecycle";

const widthStorageKey = "ow-client:game-events-width";

export function init(root: HTMLElement, page: HTMLElement, signal: AbortSignal): Cleanup {
  const savedWidth = readStorage("local", widthStorageKey);
  if (savedWidth && /^\d+px$/.test(savedWidth)) root.style.width = savedWidth;

  const observer = new ResizeObserver((entries) => {
    const entry = entries[0];
    if (!entry || !root.classList.contains("is-open")) return;
    writeStorage("local", widthStorageKey, `${Math.round(entry.contentRect.width)}px`);
  });
  observer.observe(root);
  const close = root.querySelector<HTMLButtonElement>("[data-game-events-close]");
  const toggle = page.querySelector<HTMLButtonElement>("[data-game-events-toggle]");
  close?.addEventListener(
    "click",
    () => {
      root.classList.remove("is-open");
      toggle?.setAttribute("aria-expanded", "false");
    },
    { signal },
  );
  return () => observer.disconnect();
}
