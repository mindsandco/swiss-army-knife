using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Extensions that can go on every object. Such as printing the object to JSON.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates an object and turns it into a compact JSON string
        /// Converts Enums to their string values.
        /// </summary>
        [Pure]
        [Obsolete("Use ToJson instead")]
        public static string AsJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, new StringEnumConverter());
        }

        /// <summary>
        /// Creates an object and turns it into an indented JSON string
        /// Converts Enums to their string values.
        /// </summary>
        [Pure]
        [Obsolete("Use ToIndentedJson instead")]
        public static string AsIndentedJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, Formatting.Indented, new StringEnumConverter());
        }

        /// <summary>
        /// Creates an object and turns it into an indented JSON string
        /// Converts Enums to their string values.
        /// </summary>
        [Pure]
        public static string ToIndentedJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, Formatting.Indented, new StringEnumConverter());
        }

        /// <summary>
        /// Creates an object and turns it into a compact JSON string
        /// Converts Enums to their string values.
        /// </summary>
        [Pure]
        public static string ToJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, new StringEnumConverter());
        }

        /// <summary>
        /// Shortcut for Console.Writeline(object.ToIndentedJson())
        /// Use only for debugging purposes.
        /// </summary>
        public static void PrintAsJson(this object objectToPrint)
        {
            Console.WriteLine(objectToPrint.ToIndentedJson());
        }
    }
}
