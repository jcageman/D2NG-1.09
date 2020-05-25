﻿using D2NG.D2GS.Items;
using System.Collections.Generic;

namespace ConsoleBot.Pickit
{
    public static class Pickit
    {
        public static bool ShouldPickupItem(Item item)
        {
            if(GoldItems.ShouldPickupItem(item))
            {
                return true;
            }

            switch (item.Classification)
            {
                case ClassificationType.Amulet:
                    return Amulets.ShouldPickupItem(item);
                case ClassificationType.Armor:
                    return Armors.ShouldPickupItem(item);
                case ClassificationType.Belt:
                    return Belts.ShouldPickupItem(item);
                case ClassificationType.Boots:
                    return Boots.ShouldPickupItem(item);
                case ClassificationType.Gem:
                    return Gems.ShouldPickupItem(item);
                case ClassificationType.Gloves:
                    return Gloves.ShouldPickupItem(item);
                case ClassificationType.Gold:
                    return item.Amount > 1000;
                case ClassificationType.Helm:
                    return Helms.ShouldPickupItem(item);
                case ClassificationType.Ring:
                    return Rings.ShouldPickupItem(item);
                case ClassificationType.Shield:
                    return Shields.ShouldPickupItem(item);
                case ClassificationType.Bow:
                case ClassificationType.Club:
                case ClassificationType.Crossbow:
                case ClassificationType.Dagger:
                case ClassificationType.Sword:
                case ClassificationType.Spear:
                case ClassificationType.Mace:
                case ClassificationType.Polearm:
                case ClassificationType.Axe:
                case ClassificationType.Hammer:
                    return Weapons.ShouldPickupItem(item);
                case ClassificationType.Scepter:
                case ClassificationType.Staff:
                case ClassificationType.Wand:
                    return Staves.ShouldPickupItem(item);
                case ClassificationType.AmazonBow:
                case ClassificationType.AmazonJavelin:
                case ClassificationType.AmazonSpear:
                case ClassificationType.AntidotePotion:
                case ClassificationType.Arrows:
                case ClassificationType.AssassinKatar:
                case ClassificationType.BarbarianHelm:
                case ClassificationType.BodyPart:
                case ClassificationType.Bolts:
                case ClassificationType.Circlet:
                case ClassificationType.DruidPelt:
                case ClassificationType.Ear:
                case ClassificationType.Elixir:
                case ClassificationType.Key:
                case ClassificationType.LargeCharm:
                case ClassificationType.ManaPotion:
                case ClassificationType.Rune:
                case ClassificationType.HealthPotion:
                case ClassificationType.GrandCharm:
                case ClassificationType.Herb:
                case ClassificationType.Javelin:
                case ClassificationType.NecromancerShrunkenHead:
                case ClassificationType.PaladinShield:
                case ClassificationType.RejuvenationPotion:
                case ClassificationType.Scroll:
                case ClassificationType.SorceressOrb:
                case ClassificationType.SmallCharm:
                case ClassificationType.StaminaPotion:
                case ClassificationType.ThawingPotion:
                case ClassificationType.ThrowingAxe:
                case ClassificationType.ThrowingKnife:
                case ClassificationType.ThrowingPotion:
                case ClassificationType.Tome:
                case ClassificationType.Torch:
                case ClassificationType.Jewel:
                case ClassificationType.QuestItem:
                    break;
            }

            return false;
        }

        public static bool ShouldKeepItem(Item item)
        {
            if (!item.IsIdentified)
            {
                return true;
            }

            if (!CanTouchInventoryItem(item))
            {
                return true;
            }

            switch (item.Classification)
            {
                case ClassificationType.Amulet:
                    return Amulets.ShouldKeepItem(item);
                case ClassificationType.Armor:
                    return Armors.ShouldKeepItem(item);
                case ClassificationType.Belt:
                    return Belts.ShouldKeepItem(item);
                case ClassificationType.Boots:
                    return Boots.ShouldKeepItem(item);
                case ClassificationType.Gem:
                    return Gems.ShouldKeepItem(item);
                case ClassificationType.Gloves:
                    return Gloves.ShouldKeepItem(item);
                case ClassificationType.Gold:
                    return item.Amount > 1000;
                case ClassificationType.Helm:
                    return Helms.ShouldKeepItem(item);
                case ClassificationType.Ring:
                    return Rings.ShouldKeepItem(item);
                case ClassificationType.Shield:
                    return Shields.ShouldKeepItem(item);
                case ClassificationType.Bow:
                case ClassificationType.Club:
                case ClassificationType.Crossbow:
                case ClassificationType.Dagger:
                case ClassificationType.Sword:
                case ClassificationType.Spear:
                case ClassificationType.Mace:
                case ClassificationType.Polearm:
                case ClassificationType.Axe:
                    return Weapons.ShouldKeepItem(item);
                case ClassificationType.Scepter:
                case ClassificationType.Staff:
                case ClassificationType.Wand:
                    return Staves.ShouldKeepItem(item);
                case ClassificationType.AmazonBow:
                case ClassificationType.AmazonJavelin:
                case ClassificationType.AmazonSpear:
                case ClassificationType.AntidotePotion:
                case ClassificationType.Arrows:
                case ClassificationType.AssassinKatar:
                case ClassificationType.BarbarianHelm:
                case ClassificationType.BodyPart:
                case ClassificationType.Bolts:
                case ClassificationType.Circlet:
                case ClassificationType.DruidPelt:
                case ClassificationType.Ear:
                case ClassificationType.Elixir:
                case ClassificationType.Key:
                case ClassificationType.LargeCharm:
                case ClassificationType.ManaPotion:
                case ClassificationType.Hammer:
                case ClassificationType.Rune:
                case ClassificationType.HealthPotion:
                case ClassificationType.GrandCharm:
                case ClassificationType.Herb:
                case ClassificationType.Javelin:
                case ClassificationType.NecromancerShrunkenHead:
                case ClassificationType.PaladinShield:
                case ClassificationType.RejuvenationPotion:
                case ClassificationType.Scroll:
                case ClassificationType.SorceressOrb:
                case ClassificationType.SmallCharm:
                case ClassificationType.StaminaPotion:
                case ClassificationType.ThawingPotion:
                case ClassificationType.ThrowingAxe:
                case ClassificationType.ThrowingKnife:
                case ClassificationType.ThrowingPotion:
                case ClassificationType.Tome:
                case ClassificationType.Torch:
                case ClassificationType.Jewel:
                case ClassificationType.QuestItem:
                    break;
            }

            return false;
        }

        public static bool ShouldGamble(Item item)
        {
            if(item.IsIdentified)
            {
                return false;
            }

            if (item.Name == "Bone Helm" || item.Name == "Bone Shield" || item.Name == "Belt")
            {
                return true;
            }
            
            if(item.Name == "Boots" || item.Name == "Heavy Boots")
            {
                return true;
            }

            if(item.Classification == ClassificationType.Ring)
            {
                return true;
            }

            return false;
        }

        public static bool CanTouchInventoryItem(Item item)
        {
            var defaultInventoryItems = new List<string>() { "Tome of Town Portal", "Tome of Identify", "Horadric Cube" };
            if (defaultInventoryItems.Contains(item.Name))
            {
                return false;
            }

            return true;
        }
    }
}