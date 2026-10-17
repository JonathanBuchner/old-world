import { startPage } from "../../app/page";
import { requireElement } from "../../shared/ui/dom";
import { createRoot } from "../../shared/ui/template";
import { findUnits, optionLabel, type Unit } from "./units";
import html from "./calculator.html?raw";
import "./calculator.css";

function line(text: string, className = ""): HTMLParagraphElement {
  const paragraph = document.createElement("p");
  paragraph.textContent = text;
  paragraph.className = className;
  return paragraph;
}

function createSelector(side: string, signal: AbortSignal): HTMLElement {
  const panel = document.createElement("section");
  panel.className = "unit-panel";
  panel.setAttribute("aria-label", `${side} unit`);
  const input = document.createElement("input");
  input.type = "search";
  input.placeholder = "Select a unit…";
  input.autocomplete = "off";
  input.setAttribute("aria-label", `Search ${side.toLowerCase()} unit`);
  const matches = document.createElement("div");
  matches.className = "unit-matches";
  matches.id = `${side.toLowerCase()}-matches`;
  input.setAttribute("aria-controls", matches.id);
  const details = document.createElement("div");
  details.className = "unit-details";
  details.setAttribute("aria-live", "polite");
  let selected: Unit | null = null;
  let matchesByName = new Map<string, Unit>();

  function select(unit: Unit): void {
    selected = unit;
    input.value = unit.Name;
    details.replaceChildren(
      line(unit.Name, "unit-name"),
      line(`${unit.Composition} · ${unit["Troop Type"]}`),
      line(unit.Description, "unit-description"),
      line(unit["Special Rules"].join(", ")),
      line(unit.Equipment.join(", ")),
    );
    const options = document.createElement("div");
    options.className = "unit-options";
    unit.Options.forEach((option) => {
      const label = document.createElement("label");
      const checkbox = document.createElement("input");
      checkbox.type = "checkbox";
      const text = document.createElement("span");
      text.textContent = optionLabel(option);
      label.append(checkbox, text);
      options.append(label);
    });
    details.append(options);
    input.focus();
    matches.hidden = true;
  }

  function showMatches(): void {
    const remaining = findUnits(input.value);
    matchesByName = new Map(remaining.map((unit) => [unit.Name, unit]));
    matches.replaceChildren();
    matches.hidden = false;
    if (!remaining.length) matches.append(line("No matching units."));
    remaining.forEach((unit) => {
      const button = document.createElement("button");
      button.type = "button";
      button.textContent = unit.Name;
      button.dataset.unitName = unit.Name;
      matches.append(button);
    });
  }

  matches.addEventListener(
    "click",
    (event) => {
      if (!(event.target instanceof Element)) return;
      const name = event.target.closest<HTMLButtonElement>("button")?.dataset.unitName;
      const unit = name ? matchesByName.get(name) : undefined;
      if (unit) select(unit);
    },
    { signal },
  );

  input.addEventListener(
    "input",
    () => {
      if (selected && input.value !== selected.Name) {
        selected = null;
        details.replaceChildren();
      }
      showMatches();
    },
    { signal },
  );
  input.addEventListener("focus", showMatches, { signal });
  input.addEventListener(
    "keydown",
    (event) => {
      if (event.key === "ArrowDown") {
        showMatches();
        matches.querySelector<HTMLButtonElement>("button")?.focus();
        event.preventDefault();
      }
      if (event.key === "Enter" && !matches.hidden) {
        matches.querySelector<HTMLButtonElement>("button")?.click();
        event.preventDefault();
      }
      if (event.key === "Escape") matches.hidden = true;
    },
    { signal },
  );
  panel.addEventListener(
    "focusout",
    (event) => {
      if (!(event.relatedTarget instanceof Node) || !panel.contains(event.relatedTarget))
        matches.hidden = true;
    },
    { signal },
  );
  panel.append(input, matches, details);
  showMatches();
  return panel;
}

export function mountCalculator(root: HTMLElement): () => void {
  const controller = new AbortController();
  const page = createRoot(html);
  root.replaceChildren(page);
  const selectors = requireElement(page, "[data-unit-selectors]");
  const information = requireElement(page, "[data-information]");
  const title = requireElement(page, "[data-information-title]");
  selectors.append(
    createSelector("Left", controller.signal),
    createSelector("Right", controller.signal),
  );

  function renderPage(): void {
    const route = window.location.hash.slice(1) || "/";
    selectors.hidden = route !== "/";
    information.hidden = route === "/";
    title.textContent =
      route === "/advice" ? "Advice" : route === "/about" ? "About" : "Page not found";
    page.querySelectorAll<HTMLAnchorElement>(".calculator-page__nav a").forEach((link) => {
      if (link.hash === `#${route}`) link.setAttribute("aria-current", "page");
      else link.removeAttribute("aria-current");
    });
  }

  window.addEventListener("hashchange", renderPage, { signal: controller.signal });
  renderPage();
  return () => controller.abort();
}

void startPage("calculator", mountCalculator);
