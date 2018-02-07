namespace Hades.DataAccess.Contracts
{
    using SessionToken;

    public interface IDbContext<TDbToken> where TDbToken : IDbToken
    {
    }
}
