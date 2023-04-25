export interface Spot {
  id: string;
  tag: string;
  active: boolean;
  maximumSpot: number;
  availableSpot: number;
  space: string;
  spaceId: string;
  vehicleType: number[];
}
