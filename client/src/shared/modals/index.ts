import about from "./about/about.html?raw";
import changeLog from "./change-log/change-log.html?raw";
import contribute from "./contribute/contribute.html?raw";
import help from "./help/help.html?raw";
import settings from "./settings/settings.html?raw";
import type { ModalDefinition } from "../ui/modal";

export const modals: ModalDefinition[] = [
  { name: "about", html: about },
  { name: "change-log", html: changeLog },
  { name: "contribute", html: contribute },
  { name: "help", html: help },
  { name: "settings", html: settings },
];
