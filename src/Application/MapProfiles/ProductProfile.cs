using Kolmeo.Domain;
using DbEntities = Kolmeo.Domain.DbEntities;

namespace Kolmeo.Application.MapProfiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            #region Map Product Models
            CreateMap<DbEntities.Product, Product>();
            CreateMap<Product, DbEntities.Product>();
            #endregion
        }
    }
}
