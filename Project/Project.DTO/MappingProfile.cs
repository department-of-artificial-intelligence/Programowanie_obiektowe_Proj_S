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
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));
        CreateMap<MovieDto, Movie>()
            .ForMember(dest => dest.Author, opt => opt.Ignore());

        // Review mappings
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.Id));
        CreateMap<ReviewDto, Review>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore());

        // MovieMark mappings
        CreateMap<MovieMark, MovieMarkDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.Id));
        CreateMap<MovieMarkDto, MovieMark>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore());
    }
}

