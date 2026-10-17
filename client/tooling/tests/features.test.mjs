import assert from "node:assert/strict";
import test from "node:test";
import { fileURLToPath } from "node:url";
import { build } from "vite";

// Compile the same TypeScript and JSON imports used in the browser, entirely in memory.
async function loadFeature(path) {
  const result = await build({
    configFile: false,
    publicDir: false,
    logLevel: "silent",
    build: {
      write: false,
      minify: false,
      lib: { entry: fileURLToPath(new URL(path, import.meta.url)), formats: ["es"] },
    },
  });
  const bundle = Array.isArray(result) ? result[0] : result;
  const entry = bundle.output.find((item) => item.type === "chunk" && item.isEntry);
  return import(`data:text/javascript;base64,${Buffer.from(entry.code).toString("base64")}`);
}

const [calculator, quiz, storage] = await Promise.all([
  loadFeature("../../src/features/calculator/units.ts"),
  loadFeature("../../src/features/quiz/questions.ts"),
  loadFeature("../../src/shared/services/storage.ts"),
]);

test("calculator search ignores case and whitespace and excludes mounts", () => {
  assert.ok(calculator.units.length > 0);
  assert.ok(calculator.units.every((unit) => unit.Composition !== "Mount"));
  assert.deepEqual(
    calculator.findUnits("  WARRIORS ").map((unit) => unit.Name),
    ["Dark Elf Warriors"],
  );
  assert.deepEqual(calculator.findUnits("no such unit"), []);
  assert.deepEqual(calculator.findUnits(""), calculator.units);
});

test("storage keeps local and session values separate and tolerates disabled access", (context) => {
  const previousWindow = Object.getOwnPropertyDescriptor(globalThis, "window");
  context.after(() => {
    if (previousWindow) Object.defineProperty(globalThis, "window", previousWindow);
    else delete globalThis.window;
  });
  const createStorage = () => {
    const values = new Map();
    return {
      getItem: (key) => values.get(key) ?? null,
      setItem: (key, value) => values.set(key, value),
    };
  };
  const localStorage = createStorage();
  const sessionStorage = createStorage();
  Object.defineProperty(globalThis, "window", {
    configurable: true,
    value: { localStorage, sessionStorage },
  });
  storage.writeStorage("local", "progress", "local-value");
  storage.writeStorage("session", "progress", "session-value");
  assert.equal(storage.readStorage("local", "progress"), "local-value");
  assert.equal(storage.readStorage("session", "progress"), "session-value");
  assert.equal(storage.readStorage("session", "missing"), null);
  for (const kind of ["localStorage", "sessionStorage"]) {
    Object.defineProperty(globalThis.window, kind, {
      get() {
        throw new Error("Storage disabled");
      },
    });
  }
  assert.equal(storage.readStorage("local", "progress"), null);
  assert.equal(storage.readStorage("session", "progress"), null);
  assert.doesNotThrow(() => storage.writeStorage("local", "progress", "value"));
  assert.doesNotThrow(() => storage.writeStorage("session", "progress", "value"));
});

test("option labels retain free, per-model, and maximum allowance costs", () => {
  assert.equal(calculator.optionLabel({ Name: "Handbows", Points: 0 }), "Handbows — Free");
  assert.equal(
    calculator.optionLabel({ Name: "Shields", Points: 1, "Cost Basis": "Per model" }),
    "Shields — +1 point per model",
  );
  assert.equal(
    calculator.optionLabel({ Name: "Lordling", Points: 5, "Cost Basis": "Per unit" }),
    "Lordling — +5 points per unit",
  );
  assert.equal(
    calculator.optionLabel({ Name: "Magic standard", Points: -1, "Maximum Points": 50 }),
    "Magic standard — up to 50 points",
  );
});

test("quiz filters authored categories and handles an empty result", () => {
  assert.ok(quiz.questions.length > 0);
  assert.equal(quiz.filterQuestions("all").length, quiz.questions.length);
  assert.ok(quiz.filterQuestions("pregame").every((question) => question.category === "pregame"));
  assert.deepEqual(quiz.filterQuestions("unknown"), []);
});

test("saved quiz progress is clamped and cannot reveal welcome or completion slides", () => {
  assert.deepEqual(quiz.restoreProgress(null), { category: "all", index: 0, revealed: false });
  assert.deepEqual(quiz.restoreProgress({ category: "deleted", index: 1, revealed: true }), {
    category: "all",
    index: 0,
    revealed: false,
  });
  assert.equal(
    quiz.restoreProgress({ category: "all", index: -4, revealed: true }).revealed,
    false,
  );
  assert.equal(quiz.restoreProgress({ category: "all", index: 1.5 }).index, 0);
  const completed = quiz.restoreProgress({ category: "all", index: 9999, revealed: true });
  assert.equal(completed.index, quiz.questions.length + 1);
  assert.equal(completed.revealed, false);
  assert.deepEqual(quiz.restoreProgress({ category: "all", index: 1, revealed: true }), {
    category: "all",
    index: 1,
    revealed: true,
  });
});

test("quiz images resolve under the deployed base path and unsafe links are rejected", () => {
  const base = new URL("https://example.com/tools/quiz/docs/data.json");
  assert.equal(
    quiz.safeUrl("../img/question.png", base),
    "https://example.com/tools/quiz/img/question.png",
  );
  assert.equal(quiz.safeUrl("https://tow.whfb.app/rules", base), "https://tow.whfb.app/rules");
  assert.equal(quiz.safeUrl("javascript:alert(1)", base), null);
  assert.equal(quiz.safeUrl("data:text/html,hello", base), null);
  assert.equal(quiz.safeUrl("https://[invalid", base), null);
});
