using BasicAuthentication.Users;

namespace BasicAuthentication.WebTest.Models
{
    public class TestUser : ICoreIdentityUser
    {
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        //public virtual UserStatus UserStatus { get; set; }

        public TestUser()
        {
            //UserStatus = Models.UserStatus.Active;
        }
    }
}