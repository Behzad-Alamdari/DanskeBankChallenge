using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DBC.Infrastructure.AutoMapperConfigs
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            var types = new List<Type>();
            types.AddRange(Assembly.Load(new AssemblyName("DBC.Contracts"))
                .GetExportedTypes()
                .Where(t => !t.GetTypeInfo().IsInterface)
                .ToList());

            var config = new MapperConfiguration(cfg =>
            {
                LoadCustomMappings(types, cfg);
                LoadMutualMappings(types, cfg);
                LoadIMapFromMappings(types, cfg);
                LoadIMapToMappings(types, cfg);
            });
            config.AssertConfigurationIsValid();
            return config;
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                            !t.GetTypeInfo().IsAbstract &&
                            !t.GetTypeInfo().IsInterface
                        select t)
                         .Distinct()
                         .Select(t => (IHaveCustomMappings)Activator.CreateInstance(t))
                         .ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(cfg);
            }
        }

        private static void LoadMutualMappings
            (IEnumerable<Type> types, IProfileExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapMutual<>) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        })
                        .Distinct()
                        .ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private static void LoadIMapFromMappings
            (IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        })
                        .Distinct()
                        .ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadIMapToMappings
            (IEnumerable<Type> types, IProfileExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        })
                        .Distinct()
                        .ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination);
            }
        }
    }
}
