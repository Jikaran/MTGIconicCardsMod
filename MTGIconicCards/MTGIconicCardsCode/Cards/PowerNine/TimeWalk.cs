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
public class TimeWalk() : MTGIconicCardsCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new PowerVar<TimeWalkPower>(1M)
        ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        TimeWalk timeWalk = this;
        await PowerCmd.Apply<TimeWalkPower>(choiceContext, timeWalk.Owner.Creature, timeWalk.DynamicVars["TimeWalkPower"].BaseValue, timeWalk.Owner.Creature, (CardModel) timeWalk);
    }

    protected override void OnUpgrade()
    {

    }
}