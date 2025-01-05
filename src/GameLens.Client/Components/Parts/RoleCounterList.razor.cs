using GameLens.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameLens.Client.Components.Parts;

public partial class RoleCounterList
{
    [Parameter] public string Role { get; set; } = "NO ROLE SET";

    [Parameter] public HeroCounter[] Counters { get; set; } = [];

    [Parameter] public bool ShowTank { get; set; }
    [Parameter] public bool ShowDamage { get; set; }
    [Parameter] public bool ShowSupport { get; set; }

    private string GetRoleIcon(Hero hero)
    {
        return hero.Role switch
        {
            "tank" => Icons.Material.Filled.Shield,
            "damage" => Icons.Material.Filled.Sick,
            "support" => Icons.Material.Outlined.Healing,
            _ => Icons.Material.Filled.QuestionMark
        };
    }

    private Color GetRoleColor(Hero hero)
    {
        return hero.Role switch
        {
            "tank" => Color.Primary,
            "damage" => Color.Error,
            "support" => Color.Success,
            _ => Color.Dark
        };
    }

    private bool ShouldShow(Hero hero)
    {
        if (hero.Role == "tank" && ShowTank)
            return true;

        if (hero.Role == "damage" && ShowDamage)
            return true;

        if (hero.Role == "support" && ShowSupport)
            return true;

        return false;
    }
}
