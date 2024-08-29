namespace ApiJWT.Models
{
    public class UserRepository : IUserRepository
    {

        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        User IUserRepository.Create(User user)
        {
            _context.Users.Add(user);
            user.Id=_context.SaveChanges();
            return user;

        }


        User IUserRepository.GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        User IUserRepository.GetById(int id)
        {

            return (User)_context.Users.FirstOrDefault(u => u.Id == id); 
        }

    }
}
