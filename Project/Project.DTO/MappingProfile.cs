﻿using AutoMapper;
using Project.Model;

namespace Project.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author mappings
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();

        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.HashPassword));
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.PasswordHash));

        // Movie mappings
        CreateMap<Movie, MovieDto>();
        CreateMap<MovieDto, Movie>();

        // Review mappings
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, Review>();

        // MovieMark mappings
        CreateMap<MovieMark, MovieMarkDto>();
        CreateMap<MovieMarkDto, MovieMark>();
    }
}

