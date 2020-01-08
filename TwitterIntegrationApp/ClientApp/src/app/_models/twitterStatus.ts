import { UserInvi } from "./userInvi";

export interface TwitterStatus {
    Author: UserInvi;       
    Tweet: string;
    IsRetweeted: boolean;
    CreatedDateTime: Date;
    RetweetCount: number;
    FavCount: number;
    commentCount: number;
}
