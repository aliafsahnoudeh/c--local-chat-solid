using System;

namespace LocalChat
{
    class ExceptionHandler
    {
       public static void HandleException(Exception exception)
       {
           Console.WriteLine(exception.Message);
       }
    }
}
