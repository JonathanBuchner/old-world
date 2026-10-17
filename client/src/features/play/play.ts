import { startPage } from "../../app/page";
import { createSection } from "./sections";
import { init as initActions } from "./sections/game-actions/game-actions";
import { init as initEvents } from "./sections/game-events/game-events";
import { init as initNotifications } from "./sections/notifications/notifications";
import { init as initTable } from "./sections/table/table";
import type { Cleanup } from "../../shared/ui/lifecycle";
import "./sections/game-actions/game-actions.css";
import "./sections/game-events/game-events.css";
import "./sections/notifications/notifications.css";
import "./sections/table/table.css";
import "./sections/board/board.css";
import "./sections/staging/staging.css";

export function mountPlay(root: HTMLElement): Cleanup {
  const controller = new AbortController();
  const actions = createSection("game-actions");
  const events = createSection("game-events");
  const notifications = createSection("notifications");
  const table = createSection("table");
  root.replaceChildren(actions, events, notifications, table);
  initTable(table);
  initActions(actions, root, controller.signal);
  const stopEvents = initEvents(events, root, controller.signal);
  const stopNotifications = initNotifications(notifications);
  return () => {
    controller.abort();
    stopEvents();
    stopNotifications();
  };
}

startPage("play", mountPlay);
