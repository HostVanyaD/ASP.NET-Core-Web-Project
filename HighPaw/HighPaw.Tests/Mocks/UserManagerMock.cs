namespace HighPaw.Tests.Mocks
{
    using HighPaw.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public class UserManagerMock
    {
        public static UserManager<User> Instance
        {
            get
            {
                var store = new Mock<IUserStore<User>>();
                var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
                userManager.Object.UserValidators.Add(new UserValidator<User>());
                userManager.Object.PasswordValidators.Add(new PasswordValidator<User>());

                userManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
                userManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

                return userManager.Object;
            }
        }
    }
}
