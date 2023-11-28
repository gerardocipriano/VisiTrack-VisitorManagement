using System.Linq;
using System.Linq.Dynamic.Core;

namespace Template.Infrastructure
{
    public class Paging
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByDescending { get; set; }

        /// <summary>
        /// Se non è necessario avere nel dettaglio il numero di elementi totali è possiile richidere un ulteriore elemento per verificare se attivare la paginazione alla pagina successiva
        /// Viceversa, se non si è alla prima pagina è necessario attivare la paginazione alla pagina precedente
        /// </summary>
        public bool OneMoreItem { get; set; }

        public Paging()
        {
            Page = 1;
            PageSize = 128;
        }
    }

    public static class DocumentQueryPagingExtension
    {
        /// <summary>
        /// Applies both paging and ordering
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Paging paging)
        {
            if (paging == null || string.IsNullOrWhiteSpace(paging.OrderBy) == true)
            {
                return query;
            }
            else
            {
                if (paging.OrderByDescending)
                {
                    query = query.OrderBy(paging.OrderBy + " DESC");
                }
                else
                {
                    query = query.OrderBy(paging.OrderBy);
                }

                if (paging.OneMoreItem == true)
                {
                    return query.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize + 1);
                }
                else
                {
                    return query.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize);
                }
            }
        }

        /// <summary>
        /// Applies only ordering
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, Paging paging)
        {
            if (paging == null || string.IsNullOrWhiteSpace(paging.OrderBy) == true)
            {
                return query;
            }
            else
            {
                if (paging.OrderByDescending)
                {
                    return query.OrderBy(paging.OrderBy + " DESC");
                }
                else
                {
                    return query.OrderBy(paging.OrderBy);
                }
            }
        }
    }
}
