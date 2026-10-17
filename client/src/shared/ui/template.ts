export function createRoot(html: string): HTMLElement {
  const template = document.createElement("template");
  template.innerHTML = html.trim();
  const root = template.content.firstElementChild;
  if (!(root instanceof HTMLElement)) {
    throw new Error("A UI template must contain a root HTML element.");
  }
  return root;
}
