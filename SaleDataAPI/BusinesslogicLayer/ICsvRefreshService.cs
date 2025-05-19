namespace SaleDataAPI.BusinesslogicLayer
{
    public interface ICsvRefreshService
    {
        Task RefreshDataAsync(CancellationToken cancellationToken = default);
    }
}
