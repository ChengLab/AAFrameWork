using AA.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Testing.Mapper.Extensions
{
  public static  class MappingExtensions
    {

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        //DEMO
        //public static CategoryModel ToModel(this Category entity)
        //{
        //    return entity.MapTo<Category, CategoryModel>();
        //}
        //public static Category ToEntity(this CategoryModel model, Category destination)
        //{
        //    return model.MapTo(destination);
        //}



    }
}
