using System.Collections.Generic;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_gen_lib.Data.Rules
{
    public static class RuleData
    {
        public static readonly IReadOnlyDictionary<RuleEffect, Rule> Entries =
            new Dictionary<RuleEffect, Rule>
            {
                [RuleEffect.Add1Attr]				    = new Rule { NameId = RuleEffect.Add1Attr.ToString(),				    PointCost = 0 },
                [RuleEffect.Subtract1Attr]			= new Rule { NameId = RuleEffect.Subtract1Attr.ToString(),			PointCost = 0 },
                [RuleEffect.Add1Result]				= new Rule { NameId = RuleEffect.Add1Result.ToString(),				PointCost = 0 },
                [RuleEffect.Subtract1Result]			= new Rule { NameId = RuleEffect.Subtract1Result.ToString(),			PointCost = 0 },
                [RuleEffect.Reroll6]				    = new Rule { NameId = RuleEffect.Reroll6.ToString(),				    PointCost = 0 },
                [RuleEffect.Reroll1]				    = new Rule { NameId = RuleEffect.Reroll1.ToString(),				    PointCost = 0 },
                [RuleEffect.RerollSuccesses]			= new Rule { NameId = RuleEffect.RerollSuccesses.ToString(),			PointCost = 0 },
                [RuleEffect.RerollMisses]				= new Rule { NameId = RuleEffect.RerollMisses.ToString(),				PointCost = 0 },
                [RuleEffect.AlwaysHitOn2]			    = new Rule { NameId = RuleEffect.AlwaysHitOn2.ToString(),			    PointCost = 0 },
                [RuleEffect.AlwaysHitOn3]			    = new Rule { NameId = RuleEffect.AlwaysHitOn3.ToString(),			    PointCost = 0 },
                [RuleEffect.AlwaysHitOn4]			    = new Rule { NameId = RuleEffect.AlwaysHitOn4.ToString(),			    PointCost = 0 },
                [RuleEffect.AlwaysHitOn5]			    = new Rule { NameId = RuleEffect.AlwaysHitOn5.ToString(),             PointCost = 0 },
                [RuleEffect.AlwaysWoundOn2]			= new Rule { NameId = RuleEffect.AlwaysWoundOn2.ToString(),			PointCost = 0 },
                [RuleEffect.AlwaysWoundOn3]			= new Rule { NameId = RuleEffect.AlwaysWoundOn3.ToString(),			PointCost = 0 },
                [RuleEffect.AlwaysWoundOn4]			= new Rule { NameId = RuleEffect.AlwaysWoundOn4.ToString(),			PointCost = 0 },
                [RuleEffect.AlwaysWoundOn5]			= new Rule { NameId = RuleEffect.AlwaysWoundOn5.ToString(),			PointCost = 0 },
                [RuleEffect.AlwaysArmorSaveOn2]		= new Rule { NameId = RuleEffect.AlwaysArmorSaveOn2.ToString(),		PointCost = 0 },
                [RuleEffect.AlwaysArmorSaveOn3]		= new Rule { NameId = RuleEffect.AlwaysArmorSaveOn3.ToString(),		PointCost = 0 },
                [RuleEffect.AlwaysArmorSaveOn4]		= new Rule { NameId = RuleEffect.AlwaysArmorSaveOn4.ToString(),		PointCost = 0 },
                [RuleEffect.AlwaysArmorSaveOn5]		= new Rule { NameId = RuleEffect.AlwaysArmorSaveOn5.ToString(),		PointCost = 0 },
                [RuleEffect.AlwaysArmorSaveOn6]		= new Rule { NameId = RuleEffect.AlwaysArmorSaveOn6.ToString(),		PointCost = 0 },
                [RuleEffect.Hatred]					= new Rule { NameId = RuleEffect.Hatred.ToString(),					PointCost = 0 },
                [RuleEffect.CloseOrder]				= new Rule { NameId = RuleEffect.CloseOrder.ToString(),				PointCost = 0 },
                [RuleEffect.CleavingBlow]			= new Rule { NameId = RuleEffect.CleavingBlow.ToString(),			PointCost = 0 },
                [RuleEffect.ElvenReflexes]			= new Rule { NameId = RuleEffect.ElvenReflexes.ToString(),			PointCost = 0 },
                [RuleEffect.MartialProwess]			= new Rule { NameId = RuleEffect.MartialProwess.ToString(),			PointCost = 0 },
                [RuleEffect.Veteran]				= new Rule { NameId = RuleEffect.Veteran.ToString(),				PointCost = 0 },
                [RuleEffect.WillOfTheGods]			= new Rule { NameId = RuleEffect.WillOfTheGods.ToString(),			PointCost = 0 },
                [RuleEffect.AdditionalHandWeapon]	    = new Rule { NameId = RuleEffect.AdditionalHandWeapon.ToString(),		PointCost = 0 },
                [RuleEffect.ArmorBane1]				= new Rule { NameId = RuleEffect.ArmorBane1.ToString(),				PointCost = 0 },
                [RuleEffect.ArmorBane2]				= new Rule { NameId = RuleEffect.ArmorBane2.ToString(),				PointCost = 0 },
                [RuleEffect.ArmorBane3]				= new Rule { NameId = RuleEffect.ArmorBane3.ToString(),				PointCost = 0 },
                [RuleEffect.ArmorBane3Charge]		    = new Rule { NameId = RuleEffect.ArmorBane3Charge.ToString(),			PointCost = 0 },
                [RuleEffect.FightExtraRank]			= new Rule { NameId = RuleEffect.FightExtraRank.ToString(),			PointCost = 0 },
                [RuleEffect.MoveAndShoot]			    = new Rule { NameId = RuleEffect.MoveAndShoot.ToString(),				PointCost = 0 },
                [RuleEffect.MultipleShots2]			= new Rule { NameId = RuleEffect.MultipleShots2.ToString(),			PointCost = 0 },
                [RuleEffect.MultipleShots3]			= new Rule { NameId = RuleEffect.MultipleShots3.ToString(),			PointCost = 0 },
                [RuleEffect.MultipleShots4]			= new Rule { NameId = RuleEffect.MultipleShots4.ToString(),			PointCost = 0 },
                [RuleEffect.Poison]					= new Rule { NameId = RuleEffect.Poison.ToString(),					PointCost = 0 },
                [RuleEffect.Ponderous]				= new Rule { NameId = RuleEffect.Ponderous.ToString(),				PointCost = 0 },
                [RuleEffect.QuickShot]				= new Rule { NameId = RuleEffect.QuickShot.ToString(),				PointCost = 0 },
                [RuleEffect.RequireToHands]			= new Rule { NameId = RuleEffect.RequireToHands.ToString(),			PointCost = 0 },
                [RuleEffect.StrengthPlus1Charge]		= new Rule { NameId = RuleEffect.StrengthPlus1Charge.ToString(),		PointCost = 0 },
                [RuleEffect.StrengthPlus2Charge]		= new Rule { NameId = RuleEffect.StrengthPlus2Charge.ToString(),		PointCost = 0 },
                [RuleEffect.StrengthPlus3Charge]		= new Rule { NameId = RuleEffect.StrengthPlus3Charge.ToString(),		PointCost = 0 },
                [RuleEffect.StrikeFirst]				= new Rule { NameId = RuleEffect.StrikeFirst.ToString(),				PointCost = 0 },
                [RuleEffect.StrikeLast]				= new Rule { NameId = RuleEffect.StrikeLast.ToString(),				PointCost = 0 },
                [RuleEffect.VollyFire]				= new Rule { NameId = RuleEffect.VollyFire.ToString(),				PointCost = 0 },
                [RuleEffect.PrimalFury]				= new Rule { NameId = RuleEffect.PrimalFury.ToString(),				PointCost = 0 },
                [RuleEffect.Backstab]				    = new Rule { NameId = RuleEffect.Backstab.ToString(),				    PointCost = 0 },
                [RuleEffect.MarkOfNurgle]			    = new Rule { NameId = RuleEffect.MarkOfNurgle.ToString(),				PointCost = 0 },
                [RuleEffect.DaemonOfNurgle]			= new Rule { NameId = RuleEffect.DaemonOfNurgle.ToString(),			PointCost = 0 },
                [RuleEffect.RuneOfStriking]			= new Rule { NameId = RuleEffect.RuneOfStriking.ToString(),			PointCost = 0 },
                [RuleEffect.RuneOfParrying]			= new Rule { NameId = RuleEffect.RuneOfParrying.ToString(),			PointCost = 0 },
                [RuleEffect.GrudgeRune]				= new Rule { NameId = RuleEffect.GrudgeRune.ToString(),				PointCost = 0 },
                [RuleEffect.RuneOfTheReckless]		= new Rule { NameId = RuleEffect.RuneOfTheReckless.ToString(),		PointCost = 0 },
                [RuleEffect.InnerCircle]				= new Rule { NameId = RuleEffect.InnerCircle.ToString(),				PointCost = 0 },
                [RuleEffect.GuardiansOfTheTemple]	    = new Rule { NameId = RuleEffect.GuardiansOfTheTemple.ToString(),		PointCost = 0 },
                [RuleEffect.TheWanderingDuelists]	    = new Rule { NameId = RuleEffect.TheWanderingDuelists.ToString(),		PointCost = 0 },
                [RuleEffect.CloudOfFlies]			    = new Rule { NameId = RuleEffect.CloudOfFlies.ToString(),				PointCost = 0 },
                [RuleEffect.DoL_WovenMist]			= new Rule { NameId = RuleEffect.DoL_WovenMist.ToString(),			PointCost = 0 },
                [RuleEffect.EternalHatred]			= new Rule { NameId = RuleEffect.EternalHatred.ToString(),			PointCost = 0 },
                [RuleEffect.HekartisBlessing]		= new Rule { NameId = RuleEffect.HekartisBlessing.ToString(),		PointCost = 0 },
                [RuleEffect.Murderous]				= new Rule { NameId = RuleEffect.Murderous.ToString(),				PointCost = 0 },
                [RuleEffect.SeaDragonCloak]			= new Rule { NameId = RuleEffect.SeaDragonCloak.ToString(),			PointCost = 0 }
            };
    }
}
