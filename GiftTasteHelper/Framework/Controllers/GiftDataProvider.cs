﻿using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftTasteHelper.Framework
{
    internal abstract class BaseGiftDataProvider : IGiftDataProvider
    {
        public event DataSourceChangedDelegate DataSourceChanged;

        protected IGiftDatabase Database;

        public BaseGiftDataProvider(IGiftDatabase database)
        {
            this.Database = database;
            this.Database.DatabaseChanged += () => DataSourceChanged?.Invoke();
        }

        public int[] GetFavouriteGifts(string npcName)
        {
            return Database.GetGiftsForTaste(npcName, GiftTaste.Love);
        }
    }

    internal class ProgressionGiftDataProvider : BaseGiftDataProvider
    {
        public ProgressionGiftDataProvider(IGiftDatabase database)
            : base(database)
        {
        }
    }

    internal class AllGiftDataProvider : BaseGiftDataProvider
    {
        public AllGiftDataProvider(IGiftDatabase database)
            : base(database)
        {
            // TODO: filter out names that will never be used
            foreach (var giftTaste in Game1.NPCGiftTastes)
            {
                // The first few elements are universal_tastes and we only want names.
                // None of the names contain an underscore so we can check that way.
                string npcName = giftTaste.Key;
                if (npcName.IndexOf('_') != -1)
                    continue;

                Database.AddGifts(npcName, GiftTaste.Love, Utils.GetItemsForTaste(npcName, GiftTaste.Love));
                Database.AddGifts(npcName, GiftTaste.Like, Utils.GetItemsForTaste(npcName, GiftTaste.Like));
                Database.AddGifts(npcName, GiftTaste.Dislike, Utils.GetItemsForTaste(npcName, GiftTaste.Dislike));
                Database.AddGifts(npcName, GiftTaste.Hate, Utils.GetItemsForTaste(npcName, GiftTaste.Hate));
                Database.AddGifts(npcName, GiftTaste.Neutral, Utils.GetItemsForTaste(npcName, GiftTaste.Neutral));
            }
        }
    }
}