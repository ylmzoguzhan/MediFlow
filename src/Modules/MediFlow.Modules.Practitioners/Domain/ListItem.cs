namespace MediFlow.Modules.Practitioners.Domain;

public record ListItem<T>(IEnumerable<T> Data, int Page, int PageSize, int TotalCount);
