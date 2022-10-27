namespace Testes.RepositoryTest
{
    [TestClass]
    public class UsuarioRepositoryTest
    {
        private static ApplicationDbContext _dbcontext;
        private static IUsuarioRepository _sut;
        private static Faker<Usuario> faker = new Faker<Usuario>()
            .RuleFor(prop => prop.CPF, cpf => cpf.Person.Cpf())
            .RuleFor(prop => prop.Email, email => email.Person.Email)
            .RuleFor(prop => prop.Nome, nome => nome.Person.FullName)
            .RuleFor(prop => prop.Senha, senha => senha.Random.AlphaNumeric(length: 10))
            .RuleFor(prop => prop.Telefone, telefone => telefone.Phone.PhoneNumber());

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext context)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(databaseName: "dbtest")
            );
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            _sut = serviceProvider.GetRequiredService<IUsuarioRepository>();
            _dbcontext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _dbcontext.Database.EnsureCreated();
            if (await _dbcontext.Usuarios.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var _usuarioRandom = faker.Generate();
                    await _dbcontext.Usuarios.AddAsync(_usuarioRandom);
                }
                await _dbcontext.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task TestFact()
        {
            var users = await _dbcontext.Usuarios.CountAsync();
            Assert.AreEqual(10, users);
        }

        [TestMethod]
        public async Task TestAddAsync()
        {
            var user = faker.Generate();
            await _sut.AddAsync(user);
            var count = await _dbcontext.Usuarios.CountAsync();
            Assert.AreEqual(11, count);
        }

        [TestMethod]
        public async Task TestFindAsync()
        {
            var user = await _sut.FindAsync(1);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task TestFindAsyncByName()
        {
            var user = faker.Generate();
            await _sut.AddAsync(user);
            var usuarioEncontrado = await _sut.FindAsync(user.Nome);
            Assert.AreEqual(user, usuarioEncontrado);
        }

        [TestMethod]
        public async Task TestEditAsync()
        {
            var user = await _sut.FindAsync(1);
            user.Nome = "Anastacio";
            await _sut.EditAsync(user);
            var usuarioEditado = await _sut.FindAsync(1);
            Assert.AreEqual("Anastacio", usuarioEditado.Nome);
        }

        [TestMethod]
        public async Task TestListAsync()
        {
            var users = await _sut.ListAsync();
            Assert.IsNotNull(users);
            Assert.AreEqual(12, users.Count);
        }

        [TestMethod]
        public async Task TestRemoveAsync()
        {
            var user = await _sut.FindAsync(1);
            await _sut.RemoveAsync(user);
            Assert.IsNull(await _sut.FindAsync(1));
        }
    }
}
