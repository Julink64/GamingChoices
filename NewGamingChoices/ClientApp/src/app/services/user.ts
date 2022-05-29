import { GamingMood } from "../add-game/game";

export interface GCUser {
    email: string;
    steamId: string;
    gamingMoods: GamingMood[];
    computer: string;
    availableDiscSpace: number | null;
    computerPower: number | null;
    internetNetworkQuality: number;
    consoles: string;
    askedFriendsList: string[];
    friendsList: string[];
}
