import { GamingMood } from "../add-game/game";
import { Friend } from "../friends-list/Friend";

export interface GCUser {
    id: string
    userName: string;
    email: string;
    steamId: string;
    gamingMoods: GamingMood[];
    computer: string;
    availableDiscSpace: number | null;
    computerPower: number | null;
    internetNetworkQuality: number;
    consoles: string;
    askedFriendsList: Friend[];
    friendsList: Friend[];
}
