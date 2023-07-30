namespace Shortex.BusinessLogic.Services.IServices
{
    public interface ILinkProcessStrategy
    {
        Task<IDictionary<int, string>> ProcessLinkAsync(string url);
    }
}
