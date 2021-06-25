namespace WGU_ESS.Domain.Repositories
{
  public interface IRepository
  {
    IUnitOfWork UnitOfWork { get; }
  }
}