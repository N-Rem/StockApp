using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationStockTest.ProductTest
{
    [TestClass]
    public class ProductServiceTest
    {
        // libreria moq garantiza que solo se pruebe la lógica de la clase
        // sin comprometer la base de datos o otros factoes
        private Mock<IRepositoryProduct> _mockRepository;
        private ProductService _productService;
        private Mock<IRepositorySupplier> _mockRepositorySuplier;
        private Mock<IRepositorySupplierProduct> _mockRepositorySuplierProduct;

        [TestInitialize]
        public void Setup()
        {
            //Setup es obligatorio, se necesita para simular un comportamiento del mock (por ejemplo, devolver un valor o lanzar una excepción).
            //Verify es para confirmar que el método mockeado fue utilizado correctamente.


            //crea un mock de IRepositoryPorduct
            _mockRepository = new Mock<IRepositoryProduct>();
            _mockRepositorySuplier = new Mock<IRepositorySupplier>();
            _mockRepositorySuplierProduct = new Mock<IRepositorySupplierProduct>();

            //inicializa el Servicio con el mock
            _productService = new ProductService(
            _mockRepository.Object,
            _mockRepositorySuplier.Object,
            _mockRepositorySuplierProduct.Object
            );

        }


        [TestMethod]
        public async Task TestGetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            //Arrage: inicializar las variables.
            int id = 1;
            var expectedProduct = new Product
            {
                Id = id,
                Name = "ProductTest",
                Description = "TestTestTest TestTest",
                Price = 1000,
                MinimumQuantity = 5
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(id))
                           .ReturnsAsync(expectedProduct);

            //Act: se ejecuta el metodo
            var productDTO = await _productService.GetByIdAsync(id);

            //Assert: Comprueba los valores.
            Assert.IsNotNull(productDTO);
            Assert.AreEqual(expectedProduct.Id, productDTO.Id);
            Assert.AreEqual(expectedProduct.Name, productDTO.Name);

            _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

    }
}
