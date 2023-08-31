
namespace SellIt.Test.Mock
{
    using Moq;
    using SellIt.Core.Contracts.User;

    public class UserServiceMock
    {
        public static IUserService Instance
        {
            get
            {
                var userServiceMock = new Mock<IUserService>();

                userServiceMock
                    .Setup(u => u.CurrentUserAccessor())
                    .Returns("UserId1");

                userServiceMock
                    .Setup(u => u.CurrentUserName())
                    .Returns("User1");

                return userServiceMock.Object;
            }
        }
    }
}
