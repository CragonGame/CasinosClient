// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;

namespace SharpConfig
{
    [Serializable]
    internal sealed class SettingValueCastException : Exception
    {
        public SettingValueCastException(string stringValue, Type destType, Exception innerException) :
            base(CreateMessage(stringValue, destType), innerException)
        { }

        private static string CreateMessage(string stringValue, Type destType)
        {
            return string.Format("Failed to convert value '{0}' to type {1}.", stringValue, destType.FullName);
        }
    }
}