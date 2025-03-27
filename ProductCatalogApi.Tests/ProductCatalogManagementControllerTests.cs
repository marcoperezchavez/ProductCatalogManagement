using Microsoft.AspNetCore.Mvc;
using ProductCatalogManagement.Server.Controllers;
using ProductCatalogManagement.Server.Models;
using ProductCatalogManagement.Server.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalogApi.Tests
{
    [TestClass]
    public class ProductCatalogManagementControllerTests
    {
        private Mock<IProductRepository> _mockRepository;
        private ProductCatalogManagementController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
            _controller = new ProductCatalogManagementController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Product 1" } };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedProducts = okResult.Value as IEnumerable<Product>;
            Assert.AreEqual(products, returnedProducts);
        }

        [TestMethod]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedProduct = okResult.Value as Product;
            Assert.AreEqual(product, returnedProduct);
        }

        [TestMethod]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostProduct_ReturnsCreatedAtAction_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1" };
            _mockRepository.Setup(repo => repo.AddAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostProduct(product);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnedProduct = createdAtActionResult.Value as Product;
            Assert.AreEqual(product, returnedProduct);
        }

        [TestMethod]
        public async Task PutProduct_ReturnsNoContent()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1" };
            _mockRepository.Setup(repo => repo.UpdateAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutProduct(1, product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutProduct_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1" };

            // Act
            var result = await _controller.PutProduct(2, product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
