using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;
using MTGIconicCards.MTGIconicCardsCode.RestSite;


namespace MTGIconicCards.MTGIconicCardsCode.Relics;

[Pool(typeof(SharedRelicPool))]
public class OracleOfNine() : MTGIconicCardsRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != this.Owner)
            return false;
        options.Add((RestSiteOption) new RestSiteOracleOfNine(player));
        return true;
    }
    
}


    