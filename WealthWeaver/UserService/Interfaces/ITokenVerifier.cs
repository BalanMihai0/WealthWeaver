namespace UserService.Interfaces
{
    public interface ITokenVerifier
    {
        Task<bool> VerifyTokenAsync(string token);
    }
}
