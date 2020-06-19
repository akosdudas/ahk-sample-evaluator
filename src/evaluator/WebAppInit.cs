using System;
using System.Net.Http;
using System.Threading.Tasks;
using ahk.common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace evaluator
{
    /// <summary>
    /// Initializes the web application in a "hybrid" mode: the host is the evaluator application in order
    /// to inject services into the web application runtime using DI.
    /// </summary>
    internal class WebAppInit
    {
        public const string AhkExerciseName = @"Start web app";

        private const string WebAppBaseUrl = @"http://localhost:5000";
        private static WebApplicationFactory<homework.Startup> appFactory;

        public static async Task StartWebApp(AhkResult result)
        {
            // Workaround for WebApplicationFactory, otherwise it does not start
            // https://github.com/aspnet/Hosting/blob/release/2.1/src/Microsoft.AspNetCore.TestHost/WebHostBuilderExtensions.cs#L61
            System.IO.File.CreateText(@"/app/dummy.sln").Close();

            Console.WriteLine("Starting web application...");

            try
            {
                appFactory = new WebApplicationFactory<homework.Startup>()
                    .WithWebHostBuilder(builder =>
                    {
                        // override the web host startup with custom settings
                        builder.UseContentRoot(@"/app");
                        builder.UseUrls(WebAppBaseUrl);

                        builder.ConfigureServices(services =>
                        {
                            // e.g. you can replace DI services or change their configuration
                            // the sample below removes an Entity Framework DbContext and replaces it with custom data source
                            // services.RemoveAll(typeof(MyDbContext));
                            // services.AddDbContext<MyDbContext>(options => options.UseSqlite("Data Source=temp.db"));
                        });
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot start web application. This might be a bug in the solution.");
                Console.WriteLine("Nem indithato el a webalkalmazas. Ez egy bug lehet a megoldasban.");

                throw new Exception("Cannot start web application. This might be a bug in the solution.", ex);
            }

            Console.WriteLine("Web application started.");

            // verifies connection to the web application
            using (var scope = GetRequestScope())
            {
                try
                {
                    var pingResult = await scope.HttpClient.GetAsync("/api/ping");
                    pingResult.EnsureSuccessStatusCode();

                    Console.WriteLine("Web app responding to PING.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Web app not responding to ping. This might be a bug in the solution.");
                    Console.WriteLine("Webalkalmazas nem valaszol a ping keresre. Ez egy bug lehet a megoldasban.");

                    throw new Exception("Web app not responding to ping. This might be a bug in the solution.", ex);
                }
            }
        }

        /// <summary>
        /// Creates a scope for executing http calls.
        /// </summary>
        public static TestRequestScope GetRequestScope()
        {
            var httpClient = appFactory.CreateClient(new WebApplicationFactoryClientOptions() { BaseAddress = new Uri(WebAppBaseUrl) });
            var scope = appFactory.Server.Host.Services.CreateScope();
            return new TestRequestScope(httpClient, scope);
        }
    }

    internal class TestRequestScope : IDisposable
    {
        private readonly IServiceScope scope;

        public HttpClient HttpClient { get; }
        public IServiceProvider ServiceProvider => scope.ServiceProvider;

        public TestRequestScope(HttpClient httpClient, IServiceScope scope)
        {
            this.HttpClient = httpClient;
            this.scope = scope;
        }

        public void Dispose()
        {
            scope?.Dispose();
            HttpClient?.Dispose();
        }
    }
}
