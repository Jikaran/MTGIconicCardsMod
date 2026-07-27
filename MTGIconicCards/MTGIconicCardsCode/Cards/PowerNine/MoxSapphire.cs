using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MTGIconicCards.MTGIconicCardsCode.CardPool;
using MTGIconicCards.MTGIconicCardsCode.Powers;
using MTGIconicCards.MTGIconicCardsCode.Cards;

namespace MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;

[Pool(typeof(PowerNineCardPool))]
public class MoxSapphire() : MTGIconicCardsCard(0,
    CardType.Power, CardRarity.Rare,
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
        MoxSapphire cardSource = this;
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