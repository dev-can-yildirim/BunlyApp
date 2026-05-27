using AutoMapper;
using BunlyWebApi.Dtos.AboutDtos;
using BunlyWebApi.Dtos.CategoryDtos;
using BunlyWebApi.Dtos.FeatureDtos;
using BunlyWebApi.Dtos.ImageDtos;
using BunlyWebApi.Dtos.MessageDtos;
using BunlyWebApi.Dtos.NotificationDtos;
using BunlyWebApi.Dtos.ProductDtos;
using BunlyWebApi.Dtos.RezervationDtos;
using BunlyWebApi.Entities;

namespace BunlyWebApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();

            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();
            CreateMap<Message, ResultMessageDto>().ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.CategoryName)).ReverseMap();
        
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
            CreateMap<Notification, GetNotificationByIdDto>().ReverseMap();
            CreateMap<Notification, ResultNotificationDto>().ReverseMap();


            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();


            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();


            CreateMap<Image, ResultImageDto>().ReverseMap();
            CreateMap<Image, UpdateImageDto>().ReverseMap();
            CreateMap<Image, GetImageByIdDto>().ReverseMap();
            CreateMap<Image, CreateImageDto>().ReverseMap();




        }
    }
}
