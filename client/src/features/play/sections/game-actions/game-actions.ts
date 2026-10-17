export function init(root: HTMLElement, page: HTMLElement, signal: AbortSignal): void {
  const toggle = root.querySelector<HTMLButtonElement>("[data-game-events-toggle]");
  const panel = page.querySelector<HTMLElement>("[data-game-events-section]");
  if (!toggle || !panel) return;
  toggle.addEventListener(
    "click",
    () => {
      const isOpen = panel.classList.toggle("is-open");
      toggle.setAttribute("aria-expanded", String(isOpen));
    },
    { signal },
  );
}
