﻿using ConsoleBot.Enums;
using D2NG.Core;
using D2NG.Core.D2GS;
using D2NG.Core.D2GS.Objects;
using Serilog;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleBot.Helpers
{
    public static class CubeHelpers
    {
        public static void TransmutePerfectSkulls(Game game)
        {
            var flawlessSkulls = game.Stash.Items.Where(i => i.Name == "Flawless Skull")
                                                 .ToList();
            if (flawlessSkulls.Count < 3)
            {
                return;
            }

            if (game.Cube.Items.Any())
            {
                return;
            }

            var stashes = game.GetEntityByCode(EntityCode.Stash);
            if (!stashes.Any())
            {
                return;
            }

            var stash = stashes.Single();

            bool result = GeneralHelpers.TryWithTimeout((retryCount) =>
            {
                if (game.Me.Location.Distance(stash.Location) >= 5)
                {
                    if(game.Me.HasSkill(D2NG.Core.D2GS.Players.Skill.Teleport))
                    {
                        game.TeleportToLocation(stash.Location);
                    }
                    else
                    {
                        game.MoveTo(stash);
                    }
                }
                else
                {
                    return game.OpenStash(stash);
                }

                return false;
            }, TimeSpan.FromSeconds(4));

            if (!result)
            {
                Log.Error($"Failed to open stash");
                return;
            }

            foreach (var skull in flawlessSkulls)
            {
                if (InventoryHelpers.MoveItemFromStashToInventory(game, skull) != MoveItemResult.Succes)
                {
                    break;
                }
            }

            Thread.Sleep(300);
            game.ClickButton(ClickType.CloseStash);
            Thread.Sleep(100);
            game.ClickButton(ClickType.CloseStash);

            Log.Information($"Moved skulls to inventory for transmuting");

            var remainingSkulls = flawlessSkulls;
            while (remainingSkulls.Count() > 2)
            {
                Log.Information($"Transmuting 3 flawless skulls to perfect skull");
                var skullsToTransmute = remainingSkulls.Take(3);
                remainingSkulls = remainingSkulls.Skip(3).ToList();
                foreach (var skull in skullsToTransmute)
                {
                    var inventoryItem = game.Inventory.FindItemById(skull.Id);
                    if (inventoryItem == null)
                    {
                        Log.Error($"Skull to be transmuted not found in inventory");
                        return;
                    }
                    var freeSpace = game.Cube.FindFreeSpace(inventoryItem);
                    if (freeSpace != null)
                    {
                        InventoryHelpers.PutInventoryItemInCube(game, inventoryItem, freeSpace);
                    }
                }

                if (!InventoryHelpers.TransmuteItemsInCube(game))
                {
                    Log.Error($"Transmuting items failed");
                    return;
                }

                var newCubeItems = game.Cube.Items;
                foreach (var item in newCubeItems)
                {
                    if (InventoryHelpers.PutCubeItemInInventory(game, item) != MoveItemResult.Succes)
                    {
                        Log.Error($"Couldn't move transmuted items out of cube");
                        continue;
                    }
                }
            }

            if (!game.OpenStash(stash))
            {
                Log.Error($"Opening stash failed");
                return;
            }

            var inventoryItemsToKeep = game.Inventory.Items.Where(i => Pickit.Pickit.ShouldKeepItem(i) && Pickit.Pickit.CanTouchInventoryItem(i))
                                                           .ToList();
            foreach (var item in inventoryItemsToKeep)
            {
                if (InventoryHelpers.MoveItemToStash(game, item) != MoveItemResult.Succes)
                {
                    continue;
                }
            }

            Thread.Sleep(300);
            game.ClickButton(ClickType.CloseStash);
            Thread.Sleep(100);
            game.ClickButton(ClickType.CloseStash);

            Log.Information($"Transmuting items succeeded");
        }
    }
}
