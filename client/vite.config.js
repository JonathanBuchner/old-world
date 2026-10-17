import { defineConfig } from "vite";
import { fileURLToPath } from "node:url";

export default defineConfig({
  base: "./",
  server: { host: "127.0.0.1", port: 3000, strictPort: true },
  preview: { host: "127.0.0.1", port: 3000, strictPort: true },
  build: {
    sourcemap: true,
    rollupOptions: {
      input: {
        play: fileURLToPath(new URL("./index.html", import.meta.url)),
        calculator: fileURLToPath(new URL("./calculator.html", import.meta.url)),
        quiz: fileURLToPath(new URL("./quiz.html", import.meta.url)),
      },
    },
  },
});
