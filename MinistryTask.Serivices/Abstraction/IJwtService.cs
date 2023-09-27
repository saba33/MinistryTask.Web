namespace MinistryTask.Serivices.Abstraction
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
    }
}
