import type { Cleanup } from "./lifecycle";

type NotificationType = "failure" | "alert" | "notice";

const notificationClosingClass = "app-notification--closing";
const notificationListSelector = "[data-notifications-list]";
const notificationCloseSelector = "[data-notification-close]";

let listRoot: HTMLElement | null = null;

export function registerNotificationRoot(root: HTMLElement): Cleanup {
  const controller = new AbortController();
  listRoot = root.querySelector<HTMLElement>(notificationListSelector);
  const registeredRoot = listRoot;

  listRoot?.addEventListener(
    "click",
    (event) => {
      const target = event.target;

      if (!(target instanceof Element)) {
        return;
      }

      const close = target.closest<HTMLButtonElement>(notificationCloseSelector);

      if (!close) {
        return;
      }

      const notification = close.closest<HTMLElement>("[data-notification]");

      if (notification) {
        closeNotification(notification);
      }
    },
    { signal: controller.signal },
  );
  return () => {
    controller.abort();
    if (listRoot === registeredRoot) listRoot = null;
  };
}

export function notifyFailure(message: string): void {
  notify("failure", message);
}

export function notifyAlert(message: string): void {
  notify("alert", message);
}

export function notifyNotice(message: string): void {
  notify("notice", message);
}

function notify(type: NotificationType, message: string): void {
  if (!listRoot) {
    return;
  }

  const notification = document.createElement("li");
  notification.className = `app-notification app-notification--${type}`;
  notification.dataset.notification = "";

  const close = document.createElement("button");
  close.className = "app-notification__close";
  close.type = "button";
  close.ariaLabel = "Close notification";
  close.dataset.notificationClose = "";
  close.textContent = "x";

  const text = document.createElement("p");
  text.setAttribute("role", type === "failure" ? "alert" : "status");
  text.className = "app-notification__message";
  text.textContent = message;

  notification.append(close, text);
  listRoot.append(notification);
}

function closeNotification(notification: HTMLElement): void {
  if (notification.classList.contains(notificationClosingClass)) {
    return;
  }

  notification.classList.add(notificationClosingClass);

  notification.addEventListener(
    "animationend",
    () => {
      slideRemainingNotifications(notification);
    },
    { once: true },
  );
}

function slideRemainingNotifications(notification: HTMLElement): void {
  if (!listRoot) {
    return;
  }

  const remainingNotifications = Array.from(
    listRoot.querySelectorAll<HTMLElement>("[data-notification]"),
  ).filter((item) => item !== notification);
  const oldPositions = new Map<HTMLElement, number>();

  remainingNotifications.forEach((item) => {
    oldPositions.set(item, item.getBoundingClientRect().top);
  });

  notification.remove();

  remainingNotifications.forEach((item) => {
    const oldTop = oldPositions.get(item);

    if (oldTop === undefined) {
      return;
    }

    const offset = oldTop - item.getBoundingClientRect().top;

    if (offset === 0) {
      return;
    }

    item.animate([{ transform: `translateY(${offset}px)` }, { transform: "translateY(0)" }], {
      duration: 160,
      easing: "ease-out",
    });
  });
}
