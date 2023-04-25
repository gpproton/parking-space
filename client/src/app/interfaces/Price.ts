export interface Price {
  id: string;
  description: string;
  space: string;
  spaceId: string;
  maximumTime: number;
  chargeModel: number;
  sumPrice: boolean;
  amount: number;
  vehicleType: number[];
}
