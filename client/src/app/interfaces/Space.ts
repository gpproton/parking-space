import { Price } from './Price';
import { Spot } from './Spot';

export interface Space {
  id: string;
  description: string;
  active: boolean;
  spots: Spot[];
  prices: Price[];
}
