import { createRoot } from "./template";
import type { Cleanup } from "./lifecycle";

export interface ModalDefinition {
  name: string;
  html: string;
}

const focusableSelector =
  "a[href], button:not([disabled]), input:not([disabled]), select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex='-1'])";
const noop = (): void => {};
let closeCurrent = noop;

export function closeAllModals(): void {
  closeCurrent();
}

export function initModals(definitions: ModalDefinition[]): Cleanup {
  const controller = new AbortController();
  const { signal } = controller;
  const templates = new Map(definitions.map((definition) => [definition.name, definition.html]));
  const dialogs = new Map<string, HTMLElement>();
  const overlay = document.createElement("div");
  overlay.className = "app-modal-overlay";
  overlay.hidden = true;
  let active: HTMLElement | null = null;
  let returnFocus: HTMLElement | null = null;
  document.body.append(overlay);

  function close(): void {
    if (active) {
      active.hidden = true;
      active.dataset.modalOpen = "false";
    }
    active = null;
    overlay.hidden = true;
    returnFocus?.focus();
    returnFocus = null;
  }

  function open(name: string, trigger: HTMLElement): void {
    const html = templates.get(name);
    if (!html) return;
    close();
    let dialog = dialogs.get(name);
    if (!dialog) {
      dialog = createRoot(html);
      dialogs.set(name, dialog);
      document.body.append(dialog);
    }
    returnFocus =
      trigger.closest("[data-dropdown]")?.querySelector<HTMLElement>("[data-dropdown-trigger]") ??
      trigger;
    active = dialog;
    active.hidden = false;
    active.dataset.modalOpen = "true";
    overlay.hidden = false;
    (getFocusable(active)[0] ?? active).focus();
  }

  document.addEventListener(
    "click",
    (event) => {
      if (!(event.target instanceof Element)) return;
      const trigger = event.target.closest<HTMLElement>("[data-modal-target]");
      if (trigger?.dataset.modalTarget) open(trigger.dataset.modalTarget, trigger);
      else if (event.target.closest("[data-modal-close]")) close();
    },
    { signal },
  );
  overlay.addEventListener("click", close, { signal });
  document.addEventListener(
    "keydown",
    (event) => {
      if (event.key !== "Tab" || !active) return;
      const focusable = getFocusable(active);
      const first = focusable[0];
      const last = focusable.at(-1);
      if (!first || !last) {
        event.preventDefault();
        active.focus();
      } else if (!active.contains(document.activeElement)) {
        event.preventDefault();
        (event.shiftKey ? last : first).focus();
      } else if (event.shiftKey && document.activeElement === first) {
        event.preventDefault();
        last.focus();
      } else if (!event.shiftKey && document.activeElement === last) {
        event.preventDefault();
        first.focus();
      }
    },
    { signal },
  );
  closeCurrent = close;

  return () => {
    controller.abort();
    close();
    dialogs.forEach((dialog) => dialog.remove());
    dialogs.clear();
    overlay.remove();
    if (closeCurrent === close) closeCurrent = noop;
  };
}

function getFocusable(root: HTMLElement): HTMLElement[] {
  return Array.from(root.querySelectorAll<HTMLElement>(focusableSelector)).filter(
    (element) => element.offsetParent !== null,
  );
}
