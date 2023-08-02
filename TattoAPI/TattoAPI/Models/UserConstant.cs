namespace TattoAPI.Models
{
    public class UserConstant
    {
        public static List<UserModel> User = new List<UserModel>()
        {
             new UserModel() { Id = 1,Username="testAdmin",Email="testAdmin@gmail.com",Password="123",FirstName="Test",LastName="Admin",Role="Admin"},
             new UserModel() { Id = 2,Username="testUser",Email="testUser@gmail.com",Password="123",FirstName="Test",LastName="User",Role="User"},
        };
    }
}
