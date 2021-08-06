using Bogus;
using System;
using Xunit;

namespace NerdStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class AutomacaoWebFixtureCollection : ICollectionFixture<AutomacaoWebTestsFixture> { }

    public class AutomacaoWebTestsFixture : IDisposable
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public Usuario.Usuario Usuario;

        public AutomacaoWebTestsFixture()
        {
            Usuario = new Usuario.Usuario();
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, true);
        }

        public void GerarDadosUsuario()
        {
            var faker = new Faker("pt_BR");
            Usuario.Email = faker.Internet.Email().ToLower();
            Usuario.Senha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public void Dispose()
        {
            BrowserHelper.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}