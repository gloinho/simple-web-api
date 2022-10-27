namespace Testes.ServiceTest
{
    [TestClass]
    public class UsuarioServiceTest
    {
        private static Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private static IMapper _mapper;
        private static IUsuarioService _usuarioService;

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
        public async Task TestCadastrarUsuarios()
        {
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapper);
            var teste = await _usuarioService.GetUsuarios();
            System.Console.WriteLine(teste.Count);
            Assert.IsNull(teste);
        }
    }
}
