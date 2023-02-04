namespace OnlineStore.Services.LogInService
{
    public interface ILogInService
    {
        Task<ResponseInfo<User>> GetUser(LogInDto userData);
    }
}
