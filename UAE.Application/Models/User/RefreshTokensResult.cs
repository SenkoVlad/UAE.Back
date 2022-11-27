namespace UAE.Application.Models.User;

public sealed record RefreshTokensResult
{
    public string ResultMessage { get; init; }
    public bool IsSucceed { get; init; }

    public static RefreshTokensResult UserEmailCookieOrRefreshTokenAreMessing()
    {
        return new RefreshTokensResult
        {
            IsSucceed = false,
            ResultMessage = "User Email cookie or refresh token are missing"
        };
    }
    
    public static RefreshTokensResult UserEmailCookieOrRefreshTokenAreIncorrect()
    {
        return new RefreshTokensResult
        {
            IsSucceed = false,
            ResultMessage = "User Email cookie or refresh token are incorrect"
        };
    }
    
    public static RefreshTokensResult Success()
    {
        return new RefreshTokensResult
        {
            IsSucceed = true,
            ResultMessage = "Token and refresh tokens are updated"
        };
    }


}
