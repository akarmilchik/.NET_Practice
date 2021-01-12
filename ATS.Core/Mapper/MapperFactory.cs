using AutoMapper;

namespace ATS.Core.Mapper
{
    public static class MapperFactory
    {
        public static AutoMapper.Mapper InitMapper()
        {
            var config = new MappingProfile();

            var mapper = new AutoMapper.Mapper((IConfigurationProvider)config);

            return mapper;
        }
    }
}
