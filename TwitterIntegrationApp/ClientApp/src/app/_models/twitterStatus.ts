export interface TwitterStatus {
    Author: string;
    ProfileImageUrl: string;      
    Tweet: string;
    IsRetweeted: boolean;
    CreatedDateTime: Date;
    RetweetCount: number;
    FavCount: number;
    commentCount: number;
}
