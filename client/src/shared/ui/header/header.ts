import { initDropdowns } from "../dropdown";
import type { Cleanup } from "../lifecycle";
import type { Page } from "../../../app/page";

export function init(root: HTMLElement, page: Page): Cleanup {
  root
    .querySelector<HTMLAnchorElement>(`[data-page-link="${page}"]`)
    ?.setAttribute("aria-current", "page");
  return initDropdowns(root);
}
