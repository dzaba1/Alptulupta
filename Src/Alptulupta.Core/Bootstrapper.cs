using Alptulupta.Contracts;
using Alptulupta.Core.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace Alptulupta.Core
{
    public static class Bootstrapper
    {
        public static void RegisterAlptulupta(this IServiceCollection services)
        {
            services.AddTransient<IGame, AlptuluptaGame>();
            services.AddTransient<IShapeFactory, ShapeFactory>();
            services.AddTransient<IRandomHelper, RandomHelper>();
            services.AddTransient<IKeyboard, KeyboardWrap>();
            services.AddTransient<IMouse, MouseWrap>();
            services.AddSingleton<IRandom, Random>();
            services.AddSingleton<IKeyboardHelper, KeyboardHelper>();
            services.AddSingleton<IMouseHelper, MouseHelper>();
        }
    }
}
