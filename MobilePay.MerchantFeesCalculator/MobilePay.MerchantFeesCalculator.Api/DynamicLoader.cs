using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public static class DynamicLoader
    {
        public static TModuleType LoadModuleType<TModuleType>(string assemblyName) where TModuleType : class
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(assemblyName);

            }
            catch (Exception exception)
            {
                throw new ArgumentException($"Provided assembly '{assemblyName}' could not be loaded.", exception);
            }

            var moduleType = assembly.GetTypes().FirstOrDefault(type => typeof(TModuleType).IsAssignableFrom(type) && !type.IsAbstract);
            if (moduleType == null)
            {
                throw new ArgumentException($"Provided assembly '{assemblyName}' does not contain implementations of {nameof(TModuleType)}.");
            }

            try
            {
                return (TModuleType)Activator.CreateInstance(moduleType);
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"Provided assembly '{assemblyName}' type {moduleType.Name} does not contain parameter-less constructor.", exception);
            }
        }
    }
}
