using BaseLib.Extensions;
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
public class Sheoldred() : MTGIconicCardsCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
        new PowerVar<SheoldredPower>(1M)
        ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => 
        [
         CardKeyword.Ethereal
        ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Sheoldred sheoldred = this;
        SheoldredPower sheoldredPower = await PowerCmd.Apply<SheoldredPower>(choiceContext, target: sheoldred.Owner.Creature, (Decimal) sheoldred.DynamicVars["SheoldredPower"].IntValue, sheoldred.Owner.Creature, (CardModel) sheoldred);
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Innate);
}