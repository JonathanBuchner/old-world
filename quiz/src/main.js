import './style.css';

const card = document.querySelector('#card');
const category = document.querySelector('#category');
const previous = document.querySelector('#previous');
const next = document.querySelector('#next');
const count = document.querySelector('#count');
const dataUrl = new URL(`${import.meta.env.BASE_URL}docs/data.json`, document.baseURI);
let questions = [];
let filtered = [];
let index = 0;
let revealed = false;
const progressKey = 'tow-rules-quiz-progress';
const element = (tag, className, text) => {
  const node = document.createElement(tag);
  if (className) node.className = className;
  if (text !== undefined) node.textContent = text;
  return node;
};
const safeUrl = (url) => {
  const result = new URL(url, dataUrl);
  return ['http:', 'https:'].includes(result.protocol) ? result.href : null;
};
function contentSection(label, paragraphs, images) {
  const section = element('section', 'content-section');
  section.append(element('h3', 'section-label', label));
  const layout = element('div', `content-layout${images?.length ? '' : ' text-only'}`);
  if (images?.length) {
    const gallery = element('div', 'gallery');
    for (const item of images) {
      const src = safeUrl(item.url);
      if (!src) continue;
      const image = element('img');
      image.src = src;
      image.alt = item.name;
      gallery.append(image);
    }
    layout.append(gallery);
  }
  const text = element('div', 'prose');
  for (const paragraph of Array.isArray(paragraphs) ? paragraphs : [paragraphs]) {
    text.append(element('p', '', paragraph));
  }
  layout.append(text);
  section.append(layout);
  return section;
}
function render() {
  try {
    sessionStorage.setItem(progressKey, JSON.stringify({ category: category.value, index, revealed }));
  } catch { /* Keep the quiz usable when browser storage is unavailable. */ }
  card.replaceChildren();
  previous.hidden = index === 0;
  next.hidden = index === filtered.length + 1 || !filtered.length;
  next.setAttribute('aria-label', 'Next slide');
  count.textContent = `${String(Math.min(index, filtered.length)).padStart(2, '0')} / ${String(filtered.length).padStart(2, '0')} questions`;
  if (index === 0) {
    const welcome = element('section', 'slide-message');
    welcome.append(element('h2', '', 'Welcome to TOW Learn the rules'), element('p', 'muted', 'Explore questions about Warhammer: The Old World, reveal the answers, and follow links to the relevant rules. Choose a question type to focus on a category, then use the arrows to navigate.'));
    if (!filtered.length) welcome.append(element('p', 'muted', 'No questions in this category yet.'));
    card.append(welcome);
    return;
  }
  if (index === filtered.length + 1) {
    const completed = element('section', 'slide-message completed');
    completed.append(element('h2', '', 'Completed!'));
    card.append(completed);
    return;
  }
  const item = filtered[index - 1];
  const heading = element('div', 'card-heading');
  const titleRow = element('div', 'title-row');
  titleRow.append(element('h2', '', item.title || 'Rules question'));
  heading.append(element('span', 'badge', item.category), titleRow);
  const button = element('button', 'reveal', revealed ? 'Hide answer −' : 'Show answer +');
  button.setAttribute('aria-expanded', String(revealed));
  button.setAttribute('aria-controls', 'answer');
  button.addEventListener('click', () => { revealed = !revealed; render(); card.querySelector('.reveal').focus(); });
  titleRow.append(button);
  card.append(heading, contentSection('Question', item.question, item.question_images));
  const answer = element('div', 'answer');
  answer.id = 'answer';
  answer.hidden = !revealed;
  if (revealed) {
    answer.append(contentSection('Answer', item.answer, item.answer_images));
    const commentaryParagraphs = (Array.isArray(item.commentary) ? item.commentary : [item.commentary])
      .filter(paragraph => typeof paragraph === 'string' && paragraph.trim());
    if (commentaryParagraphs.length) {
      const commentary = contentSection('Commentary', commentaryParagraphs);
      commentary.classList.add('commentary');
      answer.append(commentary);
    }
    if (item.relevant_links?.length) {
      const links = element('div', 'links');
      links.append(element('h3', 'section-label', 'Relevant rules'));
      for (const itemLink of item.relevant_links) {
        const url = safeUrl(itemLink.url);
        if (!url) continue;
        const link = element('a', '', `${itemLink.name} ↗`);
        link.href = url;
        link.target = '_blank';
        link.rel = 'noopener noreferrer';
        links.append(link);
      }
      answer.append(links);
    }
  }
  card.append(answer);
}
function move(direction) {
  const newIndex = index + direction;
  if (!filtered.length || newIndex < 0 || newIndex > filtered.length + 1) return;
  index = newIndex;
  revealed = false;
  render();
  if (document.activeElement === next && next.hidden) previous.focus();
  if (document.activeElement === previous && previous.hidden) next.focus();
  card.animate([{ opacity: 0, transform: `translateX(${direction * 18}px)` }, { opacity: 1, transform: 'translateX(0)' }], { duration: matchMedia('(prefers-reduced-motion: reduce)').matches ? 0 : 180 });
}
previous.addEventListener('click', () => move(-1));
next.addEventListener('click', () => move(1));
document.addEventListener('keydown', (event) => {
  if (event.target.isContentEditable || ['SELECT', 'INPUT', 'TEXTAREA'].includes(event.target.tagName)) return;
  if (event.key === 'ArrowLeft') move(-1);
  if (event.key === 'ArrowRight') move(1);
  if ((event.key === 'ArrowUp' || event.key === 'ArrowDown') && index > 0 && index <= filtered.length) {
    event.preventDefault();
    const showAnswer = event.key === 'ArrowDown';
    if (revealed !== showAnswer) {
      const restoreButtonFocus = document.activeElement === card.querySelector('.reveal');
      revealed = showAnswer;
      render();
      if (restoreButtonFocus) card.querySelector('.reveal').focus();
    }
  }
});
category.addEventListener('change', () => {
  filtered = questions.filter(item => category.value === 'all' || item.category === category.value);
  index = 0;
  revealed = false;
  render();
});
try {
  const response = await fetch(dataUrl, { cache: 'no-cache' });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  questions = await response.json();
  if (!Array.isArray(questions)) throw new Error('Expected a list of questions');
  for (const value of [...new Set(questions.map(item => item.category))].sort()) {
    const option = element('option', '', value.charAt(0).toUpperCase() + value.slice(1));
    option.value = value;
    category.append(option);
  }
  filtered = questions;
  try {
    const saved = JSON.parse(sessionStorage.getItem(progressKey));
    if (saved && [...category.options].some(option => option.value === saved.category)) {
      category.value = saved.category;
      filtered = questions.filter(item => saved.category === 'all' || item.category === saved.category);
      index = Number.isInteger(saved.index) ? Math.max(0, Math.min(saved.index, filtered.length ? filtered.length + 1 : 0)) : 0;
      revealed = saved.revealed === true && index > 0 && index <= filtered.length;
    }
  } catch { /* Ignore invalid or unavailable saved progress. */ }
  render();
} catch (error) {
  card.replaceChildren(element('p', 'loading', 'Unable to load questions. Please refresh to try again.'));
  console.error(error);
}
