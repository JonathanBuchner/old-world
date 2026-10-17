import "../styles/tokens.css";
import "../styles/global.css";
import headerHtml from "../shared/ui/header/header.html?raw";
import "../shared/ui/header/header.css";
import { init as initHeader } from "../shared/ui/header/header";
import { createRoot } from "../shared/ui/template";
import { initModals } from "../shared/ui/modal";
import { modals } from "../shared/modals";
import { initGlobalShortcuts } from "../shared/ui/shortcuts";
import type { Cleanup, Mount } from "../shared/ui/lifecycle";

export type Page = "play" | "calculator" | "quiz";

export function startPage(page: Page, mount: Mount): Cleanup {
  const headerHost = document.querySelector<HTMLElement>("[data-site-header]");
  const root = document.querySelector<HTMLElement>("[data-page-root]");
  if (!headerHost || !root) throw new Error("The page layout is missing.");
  const cleanups: Cleanup[] = [];
  let disposed = false;
  const dispose = (): void => {
    if (disposed) return;
    disposed = true;
    cleanups.reverse().forEach((cleanup) => cleanup());
  };

  try {
    const header = createRoot(headerHtml);
    headerHost.replaceWith(header);
    cleanups.push(initHeader(header, page));
    cleanups.push(initModals(modals));
    cleanups.push(initGlobalShortcuts());
    const cleanup = mount(root);
    if (cleanup) cleanups.push(cleanup);
  } catch (error) {
    dispose();
    const message = document.createElement("p");
    message.className = "section-load-error";
    message.setAttribute("role", "alert");
    message.textContent = "This page could not load. Please refresh to try again.";
    root.replaceChildren(message);
    console.error(error);
  }

  // Navigation tears down the document. Module replacement also releases listeners.
  import.meta.hot?.dispose(dispose);
  return dispose;
}
