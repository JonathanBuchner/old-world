import type { Cleanup } from "./lifecycle";

const dropdownSelector = "[data-dropdown]";
const triggerSelector = "[data-dropdown-trigger]";
const menuSelector = "[data-dropdown-menu]";
const menuItemSelector = "[role='menuitem']";

interface DropdownElements {
  root: HTMLElement;
  trigger: HTMLButtonElement;
  menu: HTMLElement;
}

export function initDropdowns(root: ParentNode = document): Cleanup {
  const controller = new AbortController();
  const dropdowns = root.querySelectorAll<HTMLElement>(dropdownSelector);

  dropdowns.forEach((dropdown) => {
    const elements = getDropdownElements(dropdown);

    if (!elements) {
      return;
    }

    initDropdown(elements, controller.signal);
  });
  return () => controller.abort();
}

function getDropdownElements(root: HTMLElement): DropdownElements | null {
  const trigger = root.querySelector<HTMLButtonElement>(triggerSelector);
  const menu = root.querySelector<HTMLElement>(menuSelector);

  if (!trigger || !menu) {
    return null;
  }

  return { root, trigger, menu };
}

function initDropdown(elements: DropdownElements, signal: AbortSignal): void {
  closeDropdown(elements);
  elements.root.dataset.dropdownReady = "true";

  elements.trigger.addEventListener(
    "click",
    () => {
      if (isDropdownOpen(elements)) {
        closeDropdown(elements);
        return;
      }

      openDropdown(elements);
    },
    { signal },
  );

  elements.trigger.addEventListener(
    "keydown",
    (event) => {
      if (event.key === "ArrowDown") {
        event.preventDefault();
        openDropdown(elements);
        focusFirstMenuItem(elements);
      }

      if (event.key === "ArrowUp") {
        event.preventDefault();
        openDropdown(elements);
        focusLastMenuItem(elements);
      }
    },
    { signal },
  );

  elements.menu.addEventListener(
    "keydown",
    (event) => {
      if (event.key === "Escape") {
        event.preventDefault();
        closeDropdown(elements);
        elements.trigger.focus();
      }

      if (event.key === "ArrowDown") {
        event.preventDefault();
        focusNextMenuItem(elements);
      }

      if (event.key === "ArrowUp") {
        event.preventDefault();
        focusPreviousMenuItem(elements);
      }
    },
    { signal },
  );

  elements.menu.addEventListener(
    "click",
    (event) => {
      const target = event.target;

      if (target instanceof Element && target.closest(menuItemSelector)) {
        closeDropdown(elements);

        if (target.closest("[data-modal-target]")) {
          return;
        }

        elements.trigger.focus();
      }
    },
    { signal },
  );

  document.addEventListener(
    "click",
    (event) => {
      const target = event.target;

      if (target instanceof Node && !elements.root.contains(target)) {
        closeDropdown(elements);
      }
    },
    { signal },
  );
}

function openDropdown(elements: DropdownElements): void {
  elements.root.dataset.dropdownOpen = "true";
  elements.trigger.setAttribute("aria-expanded", "true");
  elements.menu.hidden = false;
}

function closeDropdown(elements: DropdownElements): void {
  delete elements.root.dataset.dropdownOpen;
  elements.trigger.setAttribute("aria-expanded", "false");
  elements.menu.hidden = true;
}

function isDropdownOpen(elements: DropdownElements): boolean {
  return elements.trigger.getAttribute("aria-expanded") === "true";
}

function focusFirstMenuItem(elements: DropdownElements): void {
  getMenuItems(elements)[0]?.focus();
}

function focusLastMenuItem(elements: DropdownElements): void {
  const items = getMenuItems(elements);
  items.at(-1)?.focus();
}

function focusNextMenuItem(elements: DropdownElements): void {
  const items = getMenuItems(elements);
  const currentIndex = getCurrentMenuItemIndex(items);
  const nextIndex = currentIndex >= items.length - 1 ? 0 : currentIndex + 1;
  items[nextIndex]?.focus();
}

function focusPreviousMenuItem(elements: DropdownElements): void {
  const items = getMenuItems(elements);
  const currentIndex = getCurrentMenuItemIndex(items);
  const previousIndex = currentIndex <= 0 ? items.length - 1 : currentIndex - 1;
  items[previousIndex]?.focus();
}

function getCurrentMenuItemIndex(items: HTMLButtonElement[]): number {
  return items.findIndex((item) => item === document.activeElement);
}

function getMenuItems(elements: DropdownElements): HTMLButtonElement[] {
  return Array.from(elements.menu.querySelectorAll<HTMLButtonElement>(menuItemSelector));
}
