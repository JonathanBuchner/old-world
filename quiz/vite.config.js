import { defineConfig } from 'vite';
import { cpSync } from 'node:fs';
import { resolve, relative, isAbsolute } from 'node:path';

// Keep the authored JSON and image folders as the single source of truth.
export default defineConfig({
  base: './',
  server: { port: 3000, strictPort: true, hmr: true },
  preview: { port: 3000, strictPort: true },
  build: {
    rollupOptions: {
      input: { main: resolve('index.html'), about: resolve('about.html') },
    },
  },
  plugins: [{
    name: 'copy-question-assets',
    handleHotUpdate({ file, server }) {
      const imagePath = relative(resolve('img'), file);
      if (file === resolve('docs/data.json') || (!imagePath.startsWith('..') && !isAbsolute(imagePath))) {
        server.ws.send({ type: 'full-reload' });
        return [];
      }
    },
    closeBundle() {
      for (const folder of ['docs', 'img']) {
        cpSync(resolve(folder), resolve('dist', folder), { recursive: true });
      }
    },
  }],
});
