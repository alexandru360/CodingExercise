using AutoMapper;
using CodingExercise.Domain;
using CodingExercise.RestModel;
using System;

namespace CodingExercise
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // mappings
            CreateMap<ProcessPaymentModel, ProcessPayment>();
            CreateMap<ProcessPayment, ProcessPaymentModel>();
        }
    }
}
