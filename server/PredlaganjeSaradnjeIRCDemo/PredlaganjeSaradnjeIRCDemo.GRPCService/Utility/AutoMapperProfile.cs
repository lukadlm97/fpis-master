using AutoMapper;
using Google.Protobuf.Collections;
using PredlaganjeSaradnjeIRC.Data.Model;
using System.Collections.Generic;

namespace PredlaganjeSaradnjeIRCDemo.GRPCService.Utility
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<PredlaganjeSaradnjeIRC.Data.Model.Company, CompanyResponse>();
                
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));

            CreateMap<PredlaganjeSaradnjeIRC.Data.Model.City,CityResponse>();
            CreateMap<PredlaganjeSaradnjeIRC.Data.Model.Contact, ContactResponse>();
            CreateMap<PredlaganjeSaradnjeIRC.Data.Model.Location, LocationResponse>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
            CreateMap<PredlaganjeSaradnjeIRC.Data.Model.Employee,EmployeeResponse>();
            CreateMap<CompanyRequest, PredlaganjeSaradnjeIRC.Data.Model.Company>();
            CreateMap<ContactRequest, PredlaganjeSaradnjeIRC.Data.Model.Contact>();
            CreateMap<LocationRequest, PredlaganjeSaradnjeIRC.Data.Model.Location>()
                .ForPath(dest => dest.City.Id,opt=>opt.MapFrom(src=>src.CityId));



        }
    }
}