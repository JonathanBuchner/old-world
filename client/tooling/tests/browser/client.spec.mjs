import AxeBuilder from "@axe-core/playwright";
import { test, expect } from "./fixtures.mjs";

test("header links connect Play, Calculator, and Quiz", async ({ page }) => {
  await page.goto("/");
  await expect(page.locator("[data-board-section]")).toBeVisible();
  await expect(page.locator("[data-staging-section]")).toBeVisible();
  const navigation = page.getByRole("navigation", { name: "Main navigation" });
  await expect(navigation.getByRole("link")).toHaveText(["Play", "Calculator", "Quiz"]);
  for (const [name, target] of [
    ["Calculator", "calculator"],
    ["Quiz", "quiz"],
    ["Play", "play"],
  ]) {
    await navigation.getByRole("link", { name, exact: true }).click();
    await expect(page.locator(`[data-page-link="${target}"]`)).toHaveAttribute(
      "aria-current",
      "page",
    );
  }
});

test("events panel and modal keyboard controls work", async ({ page }) => {
  await page.goto("/");
  const events = page.getByRole("button", { name: "Toggle events panel" });
  await events.click();
  await expect(events).toHaveAttribute("aria-expanded", "true");
  await expect(page.locator("[data-game-events-section]")).toBeVisible();
  await page.locator("[data-game-events-close]").click();
  await expect(events).toHaveAttribute("aria-expanded", "false");
  const menu = page.getByRole("button", { name: "Open menu" });
  await menu.click();
  await page.getByRole("menuitem", { name: "About", exact: true }).click();
  const dialog = page.getByRole("dialog", { name: "About", exact: true });
  await expect(dialog).toBeVisible();
  const close = dialog.getByRole("button", { name: "Close About modal" });
  await expect(close).toBeFocused();
  await page.keyboard.press("Tab");
  await expect(close).toBeFocused();
  await page.keyboard.press("Escape");
  await expect(dialog).toBeHidden();
  await expect(menu).toBeFocused();
});

test("calculator searches units and retains selected options across its information views", async ({
  page,
}) => {
  await page.goto("/calculator.html");
  const left = page.getByRole("region", { name: "Left unit", exact: true });
  await left.getByRole("searchbox").fill("warriors");
  await left.getByRole("button", { name: "Dark Elf Warriors", exact: true }).click();
  const option = left.getByRole("checkbox", { name: /Lordling/ });
  await option.check();
  await page.getByRole("link", { name: "Advice", exact: true }).click();
  await expect(page.getByRole("heading", { name: "Advice", exact: true })).toBeVisible();
  await page.getByRole("link", { name: "Units", exact: true }).click();
  await expect(option).toBeChecked();
});

test("quiz preserves progress and resolves images under a deployment subpath", async ({ page }) => {
  await page.goto("/tools/quiz.html");
  await page.getByRole("button", { name: "Next slide" }).click();
  await page.getByRole("button", { name: "Show answer" }).click();
  await expect(page.locator("#answer")).toBeVisible();
  const images = page.locator(".quiz-page .gallery img");
  await expect(images).toHaveCount(2);
  for (const image of await images.all()) {
    await expect(image).toHaveJSProperty("complete", true);
    expect(await image.evaluate((node) => node.naturalWidth)).toBeGreaterThan(0);
    await expect(image).toHaveAttribute("src", /^https:\/\/client\.test\/tools\/quiz\/img\//);
  }
  await page.reload();
  await expect(page.getByRole("button", { name: "Hide answer" })).toBeVisible();
  await page.keyboard.press("ArrowUp");
  await expect(page.locator("#answer")).toBeHidden();
  await page.keyboard.press("ArrowRight");
  await expect(page.getByRole("heading", { name: "Completed!" })).toBeVisible();
});

test("pages remain usable when browser storage is disabled", async ({ page }) => {
  await page.addInitScript(() => {
    for (const name of ["localStorage", "sessionStorage"]) {
      Object.defineProperty(window, name, {
        get() {
          throw new Error("Storage disabled");
        },
      });
    }
  });
  await page.goto("/");
  await expect(page.locator("[data-board-section]")).toBeVisible();
  await page.getByRole("button", { name: "Toggle events panel" }).click();
  await expect(page.locator("[data-game-events-section]")).toBeVisible();
  await page.goto("/quiz.html");
  await page.getByRole("button", { name: "Next slide" }).click();
  await expect(page.getByRole("button", { name: "Show answer" })).toBeVisible();
});

for (const path of ["/", "/calculator.html", "/quiz.html"]) {
  test(`accessibility: ${path}`, async ({ page }) => {
    await page.goto(path);
    await expect(page.locator("[aria-current='page']").first()).toBeVisible();
    const results = await new AxeBuilder({ page })
      .withTags(["wcag2a", "wcag2aa", "wcag21aa"])
      .analyze();
    expect(results.violations).toEqual([]);
    expect(await page.evaluate(() => document.documentElement.scrollWidth)).toBeLessThanOrEqual(
      await page.evaluate(() => innerWidth),
    );
  });
}
