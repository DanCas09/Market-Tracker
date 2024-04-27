﻿using market_tracker_webapi.Application.Domain;

namespace market_tracker_webapi.Application.Repository.Operations.List;

public interface IListRepository
{
    Task<IEnumerable<ShoppingList>> GetListsAsync(Guid clientId, string? listName = null,
        DateTime? createdAfter = null, bool? isArchived = null, bool? isOwner = null
    );

    Task<IEnumerable<Guid>> GetClientIdsByListIdAsync(int  listId);
    
    Task<bool> IsClientInListAsync(int listId, Guid clientId);
    
    Task<ShoppingList?> GetListByIdAsync(int id);

    Task<int> AddListAsync(string listName, Guid ownerId);

    Task<ShoppingList?> UpdateListAsync(int id, DateTime? archivedAt, string? listName = null);

    Task<ShoppingList?> DeleteListAsync(int id);
    
    Task<ListClient> AddListClientAsync(int listId, Guid clientId);
    
    Task<ListClient?> DeleteListClientAsync(int listId, Guid clientId);
}