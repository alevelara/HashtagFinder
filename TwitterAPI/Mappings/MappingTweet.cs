using AutoMapper;
using Tweetinvi.Models;
using TweetSharp;
using TwitterAPI.Dtos;
using TweetDTO = TwitterAPI.Dtos.TweetDTO;

namespace TwitterAPI.Mappings
{
    public class MappingTweet : Profile
    {
        public MappingTweet()
        {
            CreateMap<TwitterStatus, TweetStatusDto>()
                .ForMember(dest => dest.Author,
                            opts => opts.MapFrom(src => src.Author))
                .ForMember(dest => dest.Tweet,
                            opts => opts.MapFrom(src => src.TextDecoded))
                .ForMember(dest => dest.IsRetweeted,
                            opts => opts.MapFrom(src => src.IsRetweeted))
                .ForMember(dest => dest.CreatedDateTime,
                            opts => opts.MapFrom(src => src.CreatedDate));                           

            CreateMap<ITweet, TweetDTO>()                             
               .ForMember(dest => dest.Tweet,
                           opts => opts.MapFrom(src => src.FullText))
               .ForMember(dest => dest.IsRetweeted,
                           opts => opts.MapFrom(src => src.Retweeted))
               .ForMember(dest => dest.CreatedDateTime,
                           opts => opts.MapFrom(src => src.CreatedAt))               
               .Include<ITweet, TweetInviDTO>();

            CreateMap<IUser, UserInviDTO>()
                .ForMember(dest => dest.ScreenName,
                            opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProfileImageUrl,
                            opts => opts.MapFrom(src => src.ProfileImageUrl));

            CreateMap<ITweet, TweetInviDTO>()
                .ForMember(dest => dest.Author, (opts => opts.MapFrom(src => new UserInviDTO
                {
                    ProfileImageUrl = src.CreatedBy.ProfileImageUrl,
                    ScreenName = src.CreatedBy.ScreenName
                })))
                .ForMember(dest => dest.CommentsCount,
                           opts => opts.MapFrom(src => src.QuoteCount))
               .ForMember(dest => dest.RetweetedCount,
                           opts => opts.MapFrom(src => src.RetweetCount))
               .ForMember(dest => dest.FavCount,
                           opts => opts.MapFrom(src => src.FavoriteCount))
               .ForMember(dest => dest.TweetUrl,
                           opts => opts.MapFrom(src => src.Url));            

        }
    }

}
