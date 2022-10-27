namespace Testes.ServiceTest
{
    [TestClass]
    public class UsuarioServiceTest
    {
        private static Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private static IMapper _mapper;
        private static IUsuarioService _usuarioService;
        private static Faker<UsuarioRequest> faker = new Faker<UsuarioRequest>()
            .RuleFor(prop => prop.CPF, cpf => cpf.Person.Cpf())
            .RuleFor(prop => prop.Email, email => email.Person.Email)
            .RuleFor(prop => prop.Nome, nome => nome.Person.FullName)
            .RuleFor(prop => prop.Senha, senha => senha.Random.AlphaNumeric(length: 10))
            .RuleFor(prop => prop.Telefone, telefone => telefone.Phone.PhoneNumber());

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsuarioProfile());
            });
            _mapper = configuration.CreateMapper();
        }

        [TestMethod]
        public async Task TestCadastrarUsuarioComCpfInvalido()
        {
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapper);
            var user = faker.Generate();
            user.CPF = "cpfinválido";
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _usuarioService.CadastrarUsuario(user)
            );
            Assert.AreEqual("CPF inválido.", ex.Message);
        }

        [TestMethod]
        public async Task TestCadastrarUsuarioValido()
        {
            var usuarioParaCadastro = faker.Generate();
            _usuarioRepositoryMock.Setup(
                prop => prop.AddAsync(_mapper.Map<Usuario>(usuarioParaCadastro))
            );
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapper);

            var usuarioResponse = await _usuarioService.CadastrarUsuario(
                _mapper.Map<UsuarioRequest>(usuarioParaCadastro)
            );
            Assert.AreEqual(usuarioParaCadastro, usuarioResponse);
        }

        [TestMethod]
        public async Task TestEditarUsuarioQueNaoExiste()
        {
            _usuarioRepositoryMock.Setup(prop => prop.FindAsync(It.IsAny<int>()));
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapper);
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _usuarioService.EditarUsuario(It.IsAny<int>(), faker.Generate())
            );
            Assert.AreEqual("Usuario não existe", exception.Message);
        }
    }
}
