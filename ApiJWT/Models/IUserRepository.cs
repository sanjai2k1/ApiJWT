namespace ApiJWT.Models
{
    public interface IUserRepository
    {

        User Create(User user);
        User GetByEmail(String email);
        User GetById(int id);

    }
}
