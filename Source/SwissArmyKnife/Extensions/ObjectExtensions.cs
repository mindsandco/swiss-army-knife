namespace SCM.SwissArmyKnife.Extensions
{
    using System;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates an object and turns it into a compact JSON string
        /// Converts Enums to their string values
        /// </summary>
        public static string AsJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, new StringEnumConverter());
        }
        
        /// <summary>
        /// Creates an object and turns it into an indented JSON string
        /// Converts Enums to their string values
        /// </summary>
        public static string AsIndentedJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize, Formatting.Indented, new StringEnumConverter());
        }

        /// <summary>
        /// Shortcut for Console.Writeline(object.AsJson())
        /// Use only for debugging purposes
        /// </summary>
        public static void PrintAsJson(this object objectToPrint)
        {
            Console.WriteLine(objectToPrint.AsJson());
        }
    }
}