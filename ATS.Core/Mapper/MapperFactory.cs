using AutoMapper;

namespace ATS.Core.Mapper
{
    public static class MapperFactory
    {
        public static IMapper InitMapper()
        {
            var entityToModelProfile = new EntityToModelMappingProfile();
            var modelToEntityProfile = new ModelToEntityMappingProfile();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfiles(new Profile[] { entityToModelProfile, modelToEntityProfile}));

            //return mapperConfig.CreateMapper();

            return new AutoMapper.Mapper(mapperConfig);

        }
    }
}
