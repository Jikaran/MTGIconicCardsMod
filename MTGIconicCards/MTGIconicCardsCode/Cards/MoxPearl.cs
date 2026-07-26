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
public class MoxPearl() : MTGIconicCardsCard(0,
    CardType.Power, CardRarity.Ancient,
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
        MoxPearl cardSource = this;
        if (cardSource.IsUpgraded)
        {
            await PlayerCmd.GainEnergy(cardSource.DynamicVars.Energy.BaseValue, cardSource.Owner);
        }
        MoxPower pyrePower = await PowerCmd.Apply<MoxPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars.Energy.BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }

    protected override void OnUpgrade()
    {

    }
}