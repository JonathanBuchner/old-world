export type Cleanup = () => void;
export type Mount = (root: HTMLElement) => void | Cleanup;
