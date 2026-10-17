import { test as base, expect } from "playwright/test";
import { access, readFile } from "node:fs/promises";
import { extname, resolve, sep } from "node:path";
import { fileURLToPath } from "node:url";

const dist = fileURLToPath(new URL("../../../dist/", import.meta.url));
const mimeTypes = {
  ".html": "text/html",
  ".js": "application/javascript",
  ".css": "text/css",
  ".json": "application/json",
  ".svg": "image/svg+xml",
  ".png": "image/png",
  ".jpg": "image/jpeg",
};

export const test = base.extend({
  page: async ({ page, context }, use) => {
    await access(resolve(dist, "index.html")).catch(() => {
      throw new Error("Build the client with npm run build before running browser checks.");
    });
    const failures = [];
    page.on("pageerror", (error) => failures.push(error.message));
    await context.route("**/*", async (route) => {
      const url = new URL(route.request().url());
      if (url.origin !== "https://client.test") {
        // Keep tests offline. External icon CSS is not part of the client build.
        if (
          url.href === "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/7.0.1/css/all.min.css"
        ) {
          await route.fulfill({ status: 200, contentType: "text/css", body: "" });
        } else {
          failures.push(`Unexpected external request: ${url.href}`);
          await route.abort();
        }
        return;
      }
      const path = decodeURIComponent(url.pathname).replace(/^\/tools\//, "/");
      const file = resolve(dist, `.${path === "/" ? "/index.html" : path}`);
      if (!file.startsWith(dist.endsWith(sep) ? dist : dist + sep)) {
        failures.push(`Asset path outside dist: ${path}`);
        await route.abort();
        return;
      }
      try {
        await route.fulfill({
          status: 200,
          contentType: mimeTypes[extname(file)] ?? "application/octet-stream",
          body: await readFile(file),
        });
      } catch {
        failures.push(`Missing build asset: ${path}`);
        await route.fulfill({ status: 404, body: "Missing asset" });
      }
    });
    await use(page);
    expect(failures, "Browser errors and missing assets").toEqual([]);
  },
});

export { expect };
