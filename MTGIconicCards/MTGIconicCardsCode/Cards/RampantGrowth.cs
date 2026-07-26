using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MTGIconicCards.MTGIconicCardsCode.Cards;
using MTGIconicCards.MTGIconicCardsCode.Powers;

namespace MTGIconicCards.MTGIconicCardsCode.Cards;

[Pool(typeof(ColorlessCardPool))]
public class RampantGrowth() : MTGIconicCardsCard(3,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new EnergyVar(1)
        ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        RampantGrowth cardSource = this;
        RampantGrowthPower pyrePower = await PowerCmd.Apply<RampantGrowthPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars.Energy.BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}