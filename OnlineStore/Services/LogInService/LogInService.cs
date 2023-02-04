using Microsoft.EntityFrameworkCore;
using OnlineStore.DTOs.User;

namespace OnlineStore.Services.LogInService
{
    public class LogInService : ILogInService
    {
        private IMapper mapper;
        private DataContext context;
        public LogInService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ResponseInfo<User>> GetUser(LogInDto userData)
        {
            var user = await context.Users.Include(u=>u.Orders)
                .FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == userData.Password);
            if (user == null) return new ResponseInfo<User>(null, false, "user not found");

            return new ResponseInfo<User>(user, true, $"user with id {user.Id}");
        }
        //cud
    }
}
