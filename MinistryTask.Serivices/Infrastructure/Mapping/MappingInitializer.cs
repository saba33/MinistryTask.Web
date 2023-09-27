using AutoMapper;
using MinistryTask.Domain;
using MinistryTask.Domain.Models;
using MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels;

namespace MinistryTask.Serivices.Infrastructure.Mapping
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorDisplayModel>().ReverseMap();
            CreateMap<FilteringModel, FilteringAuthorModel>().ReverseMap();
            CreateMap<FilteredDataResponseModel, GetAuthorsWithFiltersResponse>().ReverseMap();

            //        CreateMap<FilteredDataResponseModel, GetAuthorsWithFiltersResponse>()
            //.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.AsQueryable()));
        }
    }
}
