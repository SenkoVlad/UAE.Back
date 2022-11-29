namespace UAE.Application.Models.User;

public sealed record LoginUserResult 
{
    public bool IsSucceed { get; init; }
    
    public string Message { get; init; }
    
    public string Result { get; init; }


    public static LoginUserResult AlreadyLogged()
    {
        return new LoginUserResult
        {
            IsSucceed = false,
            Message = "Incorrect password or email"
        };
    }    
    
    public static LoginUserResult InCorrectPasswordOrEmail()
    {
        return new LoginUserResult
        {
            IsSucceed = false,
            Message = "Incorrect password or email"
        };
    }
    
    public static LoginUserResult Succeded(string token)
    {
        return new LoginUserResult
        {
            IsSucceed = true,
            Result = token
        };
    }
}