﻿using StardewModdingAPI.Events;
using StardewValley.Menus;

namespace GiftTasteHelper
{
    // TODO: document
    internal interface IGiftHelper
    {
        /*********
        ** Accessors
        *********/
        bool IsInitialized { get; }
        bool IsOpen { get; }


        /*********
        ** Methods
        *********/
        void Init(IClickableMenu menu);
        bool OnOpen(IClickableMenu menu);
        void OnResize(IClickableMenu menu);
        void OnClose();
        bool CanDraw();
        void OnDraw();
        bool CanTick();
        void OnMouseStateChange(EventArgsMouseStateChanged e);
    }
}
