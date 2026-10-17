import { notifyNotice, registerNotificationRoot } from "../../../../shared/ui/notifications";
import type { Cleanup } from "../../../../shared/ui/lifecycle";

export function init(root: HTMLElement): Cleanup {
  const cleanup = registerNotificationRoot(root);
  notifyNotice("Join our free Patreon tier to play Battle March games up to 750pts.");
  return cleanup;
}
