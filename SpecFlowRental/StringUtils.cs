using System.Text.Json;
using System;

namespace SpecFlowRental
{
    public class StringUtils {
        public static string ToJson(object obj) {
            return JsonSerializer.Serialize(obj);
        }

        public static void Debug(object obj) {
            Console.Write(ToJson(obj));
        }
    }

}
