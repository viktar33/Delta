using Delta.Domain.Models;

namespace Delta.Application.Extensions;

public static class SaleExtensions
{
    public static IQueryable<Sale> Sort(this IQueryable<Sale> query, string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy)) return query;

        query = orderBy switch
        {
            "amount" => query.OrderBy(p => p.Amount),
            "amountDesc" => query.OrderByDescending(p => p.Amount),
            "date" => query.OrderBy(p => p.Date),
            "dateDesc" => query.OrderByDescending(p => p.Date),
            _ => query.OrderBy(p => p.Date)
        };

        return query;
    }

    public static IQueryable<Sale> Search(this IQueryable<Sale> query, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm)) return query;

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return query.Where(p => p.ManagerName.ToLower().Contains(lowerCaseSearchTerm) || p.Client.ToLower().Contains(lowerCaseSearchTerm));
    }

    public static IQueryable<Sale> Filter(this IQueryable<Sale> query, string managers, string clients)
    {
        var managerList = new List<string>();
        var clientList = new List<string>();

        if (!string.IsNullOrEmpty(managers))
            managerList.AddRange(managers.ToLower().Split(",").ToList());

        if (!string.IsNullOrEmpty(clients))
            clientList.AddRange(clients.ToLower().Split(",").ToList());

        query = query.Where(p => managerList.Count == 0 || managerList.Contains(p.ManagerName.ToLower()));
        query = query.Where(p => clientList.Count == 0 || clientList.Contains(p.Client.ToLower()));

        return query;
    }
}
