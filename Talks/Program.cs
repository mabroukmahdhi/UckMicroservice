using Talks.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Talks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureTalks();

            builder.RunTalks();
        }
    }
}