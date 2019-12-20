import { ITweeter } from "./ITweeter";

export interface TwitterStatus {
    Author: ITweeter;       
    Tweet: string;
    IsRetweeted: boolean;
    CreatedDateTime: Date;
}
