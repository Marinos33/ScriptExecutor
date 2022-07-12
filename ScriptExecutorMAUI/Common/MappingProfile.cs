﻿using AutoMapper;
using System.Reflection;

namespace ScriptExecutorMAUI.Common;

public class MappingProfile : Profile
{
    private const string METHOD_NAME = "Mapping";

    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(METHOD_NAME)
                ?? type.GetInterface("IMapFrom`1").GetMethod(METHOD_NAME);

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}