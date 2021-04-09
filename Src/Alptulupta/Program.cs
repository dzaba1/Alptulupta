using System;
using Alptulupta.Contracts;
using Alptulupta.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Alptulupta
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            services.RegisterAlptulupta();

            using (var provider = services.BuildServiceProvider())
            {
                using (var game = provider.GetRequiredService<IGame>())
                {
                    game.Run();
                }
            }
        }
    }
}