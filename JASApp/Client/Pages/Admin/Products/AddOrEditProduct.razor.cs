using JAS.Shared.Dtos.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;

namespace JASApp.Client.Pages.Admin.Products;

public partial class AddOrEditProduct
{
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public bool IsEdit { get; set; } = false;

    [Inject]
    protected HttpClient HttpClient { get; set; } = default!;
    [Inject]
    protected ILogger<AddOrEditProduct> Logger { get; set; } = default!;
    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter]
    protected IMudDialogInstance MudDialog { get; set; } = default!;

    protected ProductDetailsDto ProductDetailsDto { get; set; } = new();
    protected ProductCreateOrUpdateDto ProductDto { get; set; } = new();

    private readonly List<IBrowserFile> files = [];

    private string baseUrl = "/api/product";
    protected MudForm MudForm { get; set; }
    public bool IsSaving { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        if (IsEdit)
        {
            await LoadData();
        }
    }

    private async Task LoadData()
    {
        try
        {
            ProductDetailsDto getProduct = await HttpClient.GetFromJsonAsync<ProductDetailsDto>(baseUrl + Id);

            ProductDetailsDto = getProduct;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        
    }

    protected async Task AddProductAsync()
    {
        IsSaving = true;
        await MudForm.Validate();
        if (!MudForm.IsValid)
        {
            IsSaving = false;
            return;
        }

        try
        {
            if (ProductDto != null)
            {
                await HttpClient.PostAsJsonAsync(baseUrl, ProductDto);
                MudDialog.Close();
            }
            else
            {
                IsSaving = false;
                return;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        finally
        {
            Snackbar.Add("Product Added successfully!");
            IsSaving = false;
        }
    }

    protected async Task UpdateProductAsync()
    {
        IsSaving = false;
        try
        {
            ProductDetailsDto.Name = ProductDto.Name;
            ProductDetailsDto.Price = ProductDto.Price;
            ProductDetailsDto.Description = ProductDto.Description;

            var update = await HttpClient.PatchAsJsonAsync(baseUrl + Id, ProductDto);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        finally
        {
            IsSaving = true;
            Snackbar.Add("Product updated successfully!");
            await LoadData();
        }
    }

    string Validate(decimal param)
    {
        if(param <= 0)
        {
            return "Must not be 0";
        }

        return null;
    }

    private void UploadFiles(IReadOnlyList<IBrowserFile> files)
    {
        foreach (var file in files)
        {
            this.files.Add(file);
        }

    }


    protected void Cancel() => MudDialog.Cancel();
}
