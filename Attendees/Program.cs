using Attendees.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Attendees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAttendees();

            builder.RunAttendees();
        }
    }
}