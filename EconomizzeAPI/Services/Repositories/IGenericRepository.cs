namespace EconomizzeAPI.Services.Repositories
{
	public interface IGenericRepository<T>
	{
		Task<T> CreateAsync(T entity);
	}
}
