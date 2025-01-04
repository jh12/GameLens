using GameLens.Shared.Models;
using GameLens.Shared.Services;

namespace GameLens.Client.Components.Pages;

public partial class Counters
{
    private readonly ICounterService _counterService;
    public HeroCounter[] TankCounters { get; set; } = [];
    public HeroCounter[] DamageCounters { get; set; } = [];
    public HeroCounter[] SupportCounters { get; set; } = [];
    public bool ShowTank { get; set; } = true;
    public bool ShowDamage { get; set; } = true;
    public bool ShowSupport { get; set; } = true;

    private bool _roleFilterOpen;

    public Counters(ICounterService counterService)
    {
        _counterService = counterService;
    }

    protected override async Task OnInitializedAsync()
    {
        var lookup = await _counterService.GetCounters().ToLookupAsync(h => h.Hero.Role);

        TankCounters = lookup["tank"].OrderBy(x => x.Hero, HeroRoleComparer.Instance).ToArray();
        DamageCounters = lookup["damage"].OrderBy(x => x.Hero, HeroRoleComparer.Instance).ToArray();
        SupportCounters = lookup["support"].OrderBy(x => x.Hero, HeroRoleComparer.Instance).ToArray();
        StateHasChanged();
    }

    private void RoleFilterToggle()
    {
        _roleFilterOpen = !_roleFilterOpen;
    }
}

internal class HeroRoleComparer : IComparer<Hero>
{
    public static readonly HeroRoleComparer Instance = new();

    private readonly Dictionary<string, int> _roleOrder = new()
    {
        ["tank"] = 1,
        ["damage"] = 2,
        ["support"] = 3
    };

    public int Compare(Hero? x, Hero? y)
    {
        int xRole = _roleOrder.GetValueOrDefault(x.Role, -1);
        int yRole = _roleOrder.GetValueOrDefault(y.Role, -1);

        int roleCompare = xRole.CompareTo(yRole);

        if(roleCompare != 0)
            return roleCompare;

        return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}
