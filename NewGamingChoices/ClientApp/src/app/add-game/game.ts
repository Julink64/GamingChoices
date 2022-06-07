import { GameConsole } from "../models/console";

export class Game {
  iD: number;
  name: string;
  description: string;
  thumbnailPath: string;
  minPlayers: number;
  maxPlayers: number;
  steamAppId: string;
  platformPrices: PlatformPrice[];
  minRequiredPower: number;
  genre: string;
  size: number;
  isOnMac: boolean;
  isCrossPlatform: boolean;
}

export class GamingMood {
  game: Game;
  console: GameConsole;
  isOkToPlay: boolean;
  isNeverOkToPlay: boolean;
  isGameDownloadedYet: boolean;
  isFavAndNotBlacklisted: boolean | null;
}

export class PlatformPrice {
  platform: string;
  price: number;
}
