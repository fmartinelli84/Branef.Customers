export enum CustomerSize {
  Small = 1,
  Medium = 2,
  Big = 3
}

export module CustomerSize {
  export function toDisplayName(value: CustomerSize): string {
    switch (value) {
      case CustomerSize.Small:
        return `Pequena`;
      case CustomerSize.Medium:
        return `Média`;
      case CustomerSize.Big:
        return `Grande`;
      default:
        return null;
    }
  }
}


