using BaseLib.Extensions;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.RestSite;

namespace MTGIconicCards.MTGIconicCardsCode.Patches;
[HarmonyPatch(typeof(RestSiteOption))]
[HarmonyPatch("IconPath", MethodType.Getter)]
public class RestSitePatch
{
    public static void Postfix(RestSiteOption __instance, ref string __result)
    {
        if (__instance.OptionId == "ORACLE_OF_NINE")
        {
            __result = ImageHelperExtensions.GetModImagePath("/ui/rest_site/option_rest_site_oracle_of_nine.png");
        }
        
    }
}