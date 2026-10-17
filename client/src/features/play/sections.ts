import { createRoot } from "../../shared/ui/template";

const templates = import.meta.glob<string>("./sections/*/*.html", {
  query: "?raw",
  import: "default",
  eager: true,
});

export function createSection(sectionName: string): HTMLElement {
  const html = templates[`./sections/${sectionName}/${sectionName}.html`];
  if (!html) throw new Error(`Unknown section "${sectionName}".`);
  return createRoot(html);
}
