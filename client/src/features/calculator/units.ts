import army from "./army.json";

export interface UnitOption {
  Name: string;
  Points: number;
  "Cost Basis"?: string;
  "Maximum Points"?: number;
}

export interface Unit {
  Name: string;
  Composition: string;
  Description: string;
  "Troop Type": string;
  "Special Rules": string[];
  Equipment: string[];
  Options: UnitOption[];
}

export const units: Unit[] = army.filter((entry) => entry.Composition !== "Mount");

export function findUnits(query: string): Unit[] {
  const normalized = query.trim().toLowerCase();
  return units.filter((unit) => unit.Name.toLowerCase().includes(normalized));
}

export function optionLabel(option: UnitOption): string {
  if (option["Maximum Points"]) return `${option.Name} — up to ${option["Maximum Points"]} points`;
  if (option.Points === 0) return `${option.Name} — Free`;
  if (option.Points > 0) {
    const basis = option["Cost Basis"]?.toLowerCase() ?? "";
    return `${option.Name} — +${option.Points} ${option.Points === 1 ? "point" : "points"} ${basis}`.trim();
  }
  return option.Name;
}
