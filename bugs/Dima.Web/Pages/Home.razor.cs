using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages;

public partial class HomePage : ComponentBase
{
    #region Properties

    public bool ShowValues { get; set; } = true;
    public FinancialSummary? Summary { get; set; }

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IReportHandler Handler { get; set; } = null!;

    [Inject]
    public AuthenticationStateProvider AuthProvider { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var userId = (await AuthProvider.GetAuthenticationStateAsync())
            .User;

        var request = new GetFinancialSummaryRequest();
        var result = await Handler.GetFinancialSummaryReportAsync(request);
        if (result.IsSuccess)
            Summary = result.Data;
    }

    #endregion

    #region Methods

    public void ToggleShowValues() 
        => ShowValues = !ShowValues;

    #endregion
}