using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Dtos;

namespace TwitterAPI.Mappings
{
    public class MappingTweet : Profile
    {
        public MappingTweet()
        {
            CreateMap<TwitterStatus, TwitterStatusDto>()
                .ForMember(dest => dest.Author,
                            opts => opts.MapFrom(src => src.Author))
                .ForMember(dest => dest.Tweet,
                            opts => opts.MapFrom(src => src.TextDecoded))
                .ForMember(dest => dest.IsRetweeted,
                            opts => opts.MapFrom(src => src.IsRetweeted))
                .ForMember(dest => dest.CreatedDateTime,
                            opts => opts.MapFrom(src => src.CreatedDate));
        }
    }
}
