using FluentValidation;
using MediatR;
using OpenQA.Selenium.Firefox;
using stats_gamersclub.Application.Behavior;
using stats_gamersclub.Domain.Comum.Options;
using stats_gamersclub.Infra.Comum.Configs;

namespace stats_gamersclub.API.Configurations.Extensions {
    public static class ServiceCollectionExtension {
		public static void AddConfigurationOptions(this IServiceCollection servicos, IConfiguration configuration) {
			//servicos.Configure<DataOptions>(options => configuration.GetSection("ConnectionStrings").Bind(options));

			var seleniumOpcoes = configuration.GetSection("GamersClubConfig").Get<GamersClubOptions>();

            AppSettings.SetarOpcoes(seleniumOpcoes);
		}

		public static IServiceCollection AddMyMediatR(this IServiceCollection services) {
			services.AddMediatR(typeof(ValidationBehavior<,>).Assembly);
			services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}

	}
}
