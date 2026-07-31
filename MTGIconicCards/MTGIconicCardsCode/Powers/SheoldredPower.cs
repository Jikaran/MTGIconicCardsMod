using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MTGIconicCards.MTGIconicCardsCode.Powers;

namespace MTGIconicCards.MTGIconicCardsCode.Powers;


public class SheoldredPower() : MTGIconicCardsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


    public override async Task AfterCardDrawn(
        PlayerChoiceContext choiceContext, 
        CardModel card, 
        bool fromHandDraw)
    {
        SheoldredPower sheoldredPower = this;
        if (fromHandDraw)
        {
            return;
        }
        VfxCmd.PlayOnCreatureCenters((IEnumerable<Creature>) sheoldredPower.CombatState.HittableEnemies, "vfx/vfx_attack_slash");
        SfxCmd.Play("slash_attack.mp3");
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>) sheoldredPower.CombatState.HittableEnemies, (Decimal) sheoldredPower.Amount, ValueProp.Unpowered, sheoldredPower.Owner);
        await CreatureCmd.Heal(sheoldredPower.Owner, (Decimal) sheoldredPower.Amount, true);
    }
}