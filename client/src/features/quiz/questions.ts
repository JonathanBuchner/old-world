import data from "./data/questions.json";

export interface QuizImage {
  name: string;
  url: string;
}
export interface QuizLink {
  name: string;
  url: string;
}
export interface Question {
  title: string;
  category: string;
  question: string | string[];
  answer: string | string[];
  commentary?: string | string[];
  question_images?: QuizImage[];
  answer_images?: QuizImage[];
  relevant_links?: QuizLink[];
}

export interface Progress {
  category: string;
  index: number;
  revealed: boolean;
}

export const questions: Question[] = data;
export const categories = [...new Set(questions.map((item) => item.category))].sort();

export function filterQuestions(category: string): Question[] {
  return questions.filter((item) => category === "all" || item.category === category);
}

export function restoreProgress(saved: unknown): Progress {
  const fallback = { category: "all", index: 0, revealed: false };
  if (!saved || typeof saved !== "object") return fallback;
  const value = saved as Partial<Progress>;
  if (typeof value.category !== "string" || !["all", ...categories].includes(value.category))
    return fallback;
  const length = filterQuestions(value.category).length;
  const index =
    typeof value.index === "number" && Number.isInteger(value.index)
      ? Math.max(0, Math.min(value.index, length ? length + 1 : 0))
      : 0;
  return {
    category: value.category,
    index,
    revealed: value.revealed === true && index > 0 && index <= length,
  };
}

export function safeUrl(url: string, base: URL): string | null {
  try {
    const result = new URL(url, base);
    return ["http:", "https:"].includes(result.protocol) ? result.href : null;
  } catch {
    return null;
  }
}
