using NETMP.Module12.Caching.NorthwindDb;
using System.Collections.Generic;

namespace NETMP.Module12.Caching.DbCaching.Categories
{
	public interface ICategoriesCache
	{
		IEnumerable<Category> Get(string user);

		void Set(string user, IEnumerable<Category> categories);
	}
}
