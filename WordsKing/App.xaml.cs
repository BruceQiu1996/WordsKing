using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using WordsKing.Pages;
using WordsKing.ViewModels;
using WordsKing.Windows;

namespace WordsKing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IServiceProvider? ServiceProvider;
        internal static IHost host;
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var builder = Host.CreateDefaultBuilder(e.Args); ;

            builder.ConfigureServices((context, service) =>
            {
                service.AddSingleton<MainWindow>();
                service.AddSingleton<MainWindowViewModel>();

                service.AddTransient<BookWindow>();
                service.AddTransient<BookViewModel>();

                service.AddSingleton<BookDetailPage>();
                service.AddSingleton<StudyWordPage>();
                service.AddDbContextFactory<WordKingDbContext>(options =>
                    options.UseSqlite("Data Source = data.db"));
            });

            host = builder.Build();
            ServiceProvider = host.Services;
            await host.StartAsync();
            host.Services.GetRequiredService<MainWindow>().Show();
        }
    }

}
