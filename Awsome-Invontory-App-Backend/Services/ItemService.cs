using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Models;

namespace Awsome_Invontory_App_Backend.Services;

public class ItemService
{
    private readonly DataContext _dataContext;
    private readonly Authenitcation _authenitcation;

    public ItemService(DataContext dataContext, Authenitcation authenitcation)
    {
        this._dataContext = dataContext;
        this._authenitcation = authenitcation;
    }

    public List<ItemsResponse> GetAll()
    {
        return [.. _dataContext.Items.Select(i => new ItemsResponse(i))];
    }
}