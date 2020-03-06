using Common;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UtilitiesHandler
{
    public class Utility : ReactiveObject
    {
        [JsonProperty("utility-name")]
        public string Name { get; set; }
        public string Help { get; set; }
        public bool Enabled
        {
            get => enabled;
            set
            {
                this.RaiseAndSetIfChanged(ref enabled, value);
            }
        }

        MethodInfo _runMethod;
        object _instance;
        private bool enabled;

        public Utility(Type type)
        {
            _instance = CreateInstance(type);
            UtilityAttribute attributes = GetAttributes(type);

            Name = GetName(attributes);
            Help = GetHelpMethodMessage(type);
            Enabled = true;

            _runMethod = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Instance);
        }

        private static string GetName(UtilityAttribute attributes)
        {
            if (attributes != null)
                try
                {
                    return attributes.Name;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Cannot get 'Name' attribute", ex);
                }
            else
                throw new Exception($"There is no any attribute");
        }

        public string Execute()
        {
            try
            {
                if (_runMethod != null)
                    return _runMethod.Invoke(_instance, null) as string;
                else
                    return "Can't detect 'Run' method of the utility";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private object CreateInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);

            }
            catch (Exception ex)
            {
                throw new Exception($"Can't create instance of '{type}'", ex);
            }
        }

        private UtilityAttribute GetAttributes(Type type)
        {
            try
            {
                return type.GetCustomAttribute<UtilityAttribute>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't get attributes for '{type}' utility", ex);
            }
        }

        private string GetHelpMethodMessage(Type type)
        {
            try
            {
                return type.GetMethod("Help", BindingFlags.Public | BindingFlags.Instance).Invoke(_instance, null) as string;
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't get or invoke Help method of '{type}' utility", ex);
            }
        }
    }
}
