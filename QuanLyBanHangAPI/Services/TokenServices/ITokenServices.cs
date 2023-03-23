namespace QuanLyBanHangAPI.Services.TokenServices
{
    public interface ITokenServices
    {
        bool IsTokenExpired(string token);
        string GenerateRefreshToken();
    }
}
