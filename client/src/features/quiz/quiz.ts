import { startPage } from "../../app/page";
import { requireElement } from "../../shared/ui/dom";
import { createRoot } from "../../shared/ui/template";
import { categories, filterQuestions, restoreProgress, safeUrl, type QuizImage } from "./questions";
import html from "./quiz.html?raw";
import "./quiz.css";
import { readStorage, writeStorage } from "../../shared/services/storage";

const progressKey = "tow-rules-quiz-progress";

function element<K extends keyof HTMLElementTagNameMap>(
  tag: K,
  className = "",
  text?: string,
): HTMLElementTagNameMap[K] {
  const node = document.createElement(tag);
  node.className = className;
  if (text !== undefined) node.textContent = text;
  return node;
}

export function mountQuiz(root: HTMLElement): () => void {
  const controller = new AbortController();
  const { signal } = controller;
  const page = createRoot(html);
  root.replaceChildren(page);
  const card = requireElement(page, "#card");
  const category = requireElement<HTMLSelectElement>(page, "#category");
  const previous = requireElement<HTMLButtonElement>(page, "#previous");
  const next = requireElement<HTMLButtonElement>(page, "#next");
  const count = requireElement(page, "#count");
  // Keep authored image URLs relative to their original docs/data.json location.
  const base = new URL("quiz/docs/data.json", new URL(import.meta.env.BASE_URL, document.baseURI));
  let saved: unknown;
  try {
    saved = JSON.parse(readStorage("session", progressKey) ?? "null");
  } catch {
    /* Ignore invalid saved progress. */
  }
  const progress = restoreProgress(saved);
  let filtered = filterQuestions(progress.category);
  let index = progress.index;
  let revealed = progress.revealed;

  for (const value of categories) {
    const option = element("option", "", value.charAt(0).toUpperCase() + value.slice(1));
    option.value = value;
    category.append(option);
  }
  category.value = progress.category;

  function contentSection(
    label: string,
    paragraphs: string | string[],
    images: QuizImage[] = [],
  ): HTMLElement {
    const section = element("section", "content-section");
    section.append(element("h3", "section-label", label));
    const layout = element("div", `content-layout${images.length ? "" : " text-only"}`);
    if (images.length) {
      const gallery = element("div", "gallery");
      for (const item of images) {
        const src = safeUrl(item.url, base);
        if (!src) continue;
        const image = element("img");
        image.src = src;
        image.alt = item.name;
        gallery.append(image);
      }
      layout.append(gallery);
    }
    const text = element("div", "prose");
    for (const paragraph of Array.isArray(paragraphs) ? paragraphs : [paragraphs]) {
      text.append(element("p", "", paragraph));
    }
    layout.append(text);
    section.append(layout);
    return section;
  }

  function render(): void {
    writeStorage(
      "session",
      progressKey,
      JSON.stringify({ category: category.value, index, revealed }),
    );
    card.replaceChildren();
    previous.hidden = index === 0;
    next.hidden = index === filtered.length + 1 || !filtered.length;
    count.textContent = `${String(Math.min(index, filtered.length)).padStart(2, "0")} / ${String(filtered.length).padStart(2, "0")} questions`;
    if (index === 0) {
      const welcome = element("section", "slide-message");
      welcome.append(
        element("h2", "", "Welcome to TOW Learn the rules"),
        element(
          "p",
          "muted",
          "Explore questions about Warhammer: The Old World, reveal the answers, and follow links to the relevant rules. Choose a question type to focus on a category, then use the arrows to navigate.",
        ),
      );
      if (!filtered.length)
        welcome.append(element("p", "muted", "No questions in this category yet."));
      card.append(welcome);
      return;
    }
    if (index === filtered.length + 1) {
      const completed = element("section", "slide-message completed");
      completed.append(element("h2", "", "Completed!"));
      card.append(completed);
      return;
    }
    const item = filtered[index - 1];
    if (!item) return;
    const heading = element("div", "card-heading");
    const titleRow = element("div", "title-row");
    titleRow.append(element("h2", "", item.title || "Rules question"));
    heading.append(element("span", "badge", item.category), titleRow);
    const button = element("button", "reveal", revealed ? "Hide answer −" : "Show answer +");
    button.type = "button";
    button.setAttribute("aria-expanded", String(revealed));
    button.setAttribute("aria-controls", "answer");
    titleRow.append(button);
    card.append(heading, contentSection("Question", item.question, item.question_images));
    const answer = element("div", "answer");
    answer.id = "answer";
    answer.hidden = !revealed;
    if (revealed) {
      answer.append(contentSection("Answer", item.answer, item.answer_images));
      const commentary = (
        Array.isArray(item.commentary) ? item.commentary : [item.commentary]
      ).filter(
        (paragraph): paragraph is string => typeof paragraph === "string" && !!paragraph.trim(),
      );
      if (commentary.length) {
        const section = contentSection("Commentary", commentary);
        section.classList.add("commentary");
        answer.append(section);
      }
      if (item.relevant_links?.length) {
        const links = element("div", "links");
        links.append(element("h3", "section-label", "Relevant rules"));
        for (const itemLink of item.relevant_links) {
          const url = safeUrl(itemLink.url, base);
          if (!url) continue;
          const link = element("a", "", `${itemLink.name} ↗`);
          link.href = url;
          link.target = "_blank";
          link.rel = "noopener noreferrer";
          links.append(link);
        }
        answer.append(links);
      }
    }
    card.append(answer);
  }

  function move(direction: number): void {
    const newIndex = index + direction;
    if (!filtered.length || newIndex < 0 || newIndex > filtered.length + 1) return;
    index = newIndex;
    revealed = false;
    render();
    if (document.activeElement === next && next.hidden) previous.focus();
    if (document.activeElement === previous && previous.hidden) next.focus();
    card.animate(
      [
        { opacity: 0, transform: `translateX(${direction * 18}px)` },
        { opacity: 1, transform: "translateX(0)" },
      ],
      { duration: matchMedia("(prefers-reduced-motion: reduce)").matches ? 0 : 180 },
    );
  }

  card.addEventListener(
    "click",
    (event) => {
      if (!(event.target instanceof Element) || !event.target.closest(".reveal")) return;
      revealed = !revealed;
      render();
      card.querySelector<HTMLButtonElement>(".reveal")?.focus();
    },
    { signal },
  );
  previous.addEventListener("click", () => move(-1), { signal });
  next.addEventListener("click", () => move(1), { signal });
  document.addEventListener(
    "keydown",
    (event) => {
      if (event.defaultPrevented || document.querySelector(".app-modal:not([hidden])")) return;
      const target = event.target;
      if (
        target instanceof HTMLElement &&
        (target.isContentEditable || ["SELECT", "INPUT", "TEXTAREA"].includes(target.tagName))
      )
        return;
      if (["ArrowLeft", "ArrowRight", "ArrowUp", "ArrowDown"].includes(event.key))
        event.preventDefault();
      if (event.key === "ArrowLeft") move(-1);
      if (event.key === "ArrowRight") move(1);
      if (
        (event.key === "ArrowUp" || event.key === "ArrowDown") &&
        index > 0 &&
        index <= filtered.length
      ) {
        const showAnswer = event.key === "ArrowDown";
        if (revealed !== showAnswer) {
          const restoreFocus = document.activeElement === card.querySelector(".reveal");
          revealed = showAnswer;
          render();
          if (restoreFocus) card.querySelector<HTMLButtonElement>(".reveal")?.focus();
        }
      }
    },
    { signal },
  );
  category.addEventListener(
    "change",
    () => {
      filtered = filterQuestions(category.value);
      index = 0;
      revealed = false;
      render();
    },
    { signal },
  );
  render();
  return () => controller.abort();
}

void startPage("quiz", mountQuiz);
