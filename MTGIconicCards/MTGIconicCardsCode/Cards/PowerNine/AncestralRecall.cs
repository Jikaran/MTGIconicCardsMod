using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MTGIconicCards.MTGIconicCardsCode.CardPool;
using MTGIconicCards.MTGIconicCardsCode.Cards;

namespace MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;

[Pool(typeof(PowerNineCardPool))]
public class AncestralRecall() : MTGIconicCardsCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyPlayer)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        new CardsVar(3)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    { 
        AncestralRecall ancestralRecall = this;

        if (play.Target == null)
        {
            await CardPileCmd.DrawWithoutBlockingOnOtherPlayers(choiceContext, ancestralRecall.DynamicVars.Cards.BaseValue,
                ancestralRecall.Owner);
        }
        else
        {
            await CardPileCmd.DrawWithoutBlockingOnOtherPlayers(choiceContext,
                ancestralRecall.DynamicVars.Cards.BaseValue,
                play.Target.Player);
        }
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}