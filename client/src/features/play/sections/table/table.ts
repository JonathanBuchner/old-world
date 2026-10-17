import { createSection } from "../../sections";

export function init(root: HTMLElement): void {
  const stagingSlot = root.querySelector<HTMLElement>("[data-table-slot='staging']");
  const boardSlot = root.querySelector<HTMLElement>("[data-table-slot='board']");

  if (!stagingSlot || !boardSlot) {
    return;
  }

  const staging = createSection("staging");
  const board = createSection("board");

  stagingSlot.replaceWith(staging);
  boardSlot.replaceWith(board);
}
