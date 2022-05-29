import { GameConsole } from "../models/console";

export class Game {
  ID: number;
  Name: string;
  Description: string;
  ThumbnailPath: string;
  MinPlayers: number;
  MaxPlayers: number;
  SteamAppId: string;
  PlatformPrices: PlatformPrice[];
  MinRequiredPower: number;
  Genre: string;
  Size: number;
  IsOnMac: boolean;
  IsCrossPlatform: boolean;
}

export class GamingMood {
  game: Game;
  console: GameConsole;
  isOkToPlay: boolean;
  isNeverOkToPlay: boolean;
  isGameDownloadedYet: boolean;
}

export class PlatformPrice {
  Platform: string;
  Price: number;
}
