namespace SellIt.Test
{
    using Moq;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Repository;
    using SellIt.Core.Services.Category;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Infrastructure.Data.Models;
    using Xunit;

    public class CategoryServiceTest
    {

        [Fact]
        public async Task Should_Return_All_Categories()
        {
            var list = new List<Category>();
            var mockImageService = new Mock<IImageService>();
            var mockCategoryRepository = new Mock<IRepository<Category>>();

            mockCategoryRepository.Setup(s => s.AllAsNoTracking()).Returns(list.AsQueryable());
            mockCategoryRepository.Setup(s => s.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var service = new CategoryService(mockImageService.Object, mockCategoryRepository.Object);

            var createCategoryViewModel = new CreateCategoryViewModel
            {
                Name = "Test Category",
            };

            await service.CreateCategoryAsync(createCategoryViewModel);
            Assert.Equal(1, list.Count());
        }
    }
}
