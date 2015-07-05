using AutoMapper;
using ContosoUniversity.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ContosoUniversity.Config
{
    /// <summary>
    /// Find and load all mappings, noted with 'IMapFrom<T>' and 'IHaveCustomMappings'
    /// </summary>
    /// <remarks>
    /// Taken from 'Fail-Tracker' by Matt Honeycutt:
    /// https://github.com/MattHoneycutt/Fail-Tracker
    /// </remarks>
    public class AutoMapperConfig
    {
        public static void Init()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            LoadStandardMappings(types);

            LoadCustomMappings(types);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where i.IsGenericType 
                          && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) 
                          && !t.IsAbstract 
                          && !t.IsInterface
                       select new
                       {
                           Source = i.GetGenericArguments()[0],
                           Destination = t
                       };

            maps.ToList().ForEach((map) =>
            {
                Mapper.CreateMap(map.Source, map.Destination);
            });
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where typeof(IHaveCustomMappings).IsAssignableFrom(t) 
                          && !t.IsAbstract 
                          && !t.IsInterface
                       select (IHaveCustomMappings)Activator.CreateInstance(t);

            maps.ToList().ForEach((map) => 
            {
                map.CreateMappings(Mapper.Configuration);
            });
        }
    }
}