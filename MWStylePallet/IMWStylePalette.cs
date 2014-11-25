using MWDataSerilizationType;
using System;


namespace MWStylePalette
{/*
  * Redudndant Now
  */
    public interface IMWStylePalette : IDisposable
    {
        /*
         * Created by Ayush
         * Style Palette messaging functionality.
         * 1. Share Request from a user to share the style pallet view with a contact
         * 2. Accept sharing request and save friends environment if s/he was working previously on their own style pallet
         * 3. Style Palette change notification
         * 4. Saving the modifications/style on user's wardrobe
         * 5. Retrieving a user's preivous working style pallet
         */


        string ShareRequest(MWShareRequestJSON request);
        string AcceptRequest(MWAcceptRequestJSON acceptRequest);
        string StyleChangeNotify(MWSPEnvironmentJSON style);
        string SaveOutfit(MWSPEnvironmentJSON style);
        string GetPreviousEnvironment(string UserId);
        
    }

    /*
     * Code From Alfred's Searching Engine Interface Class
     */
    public class MWStylePaletteFactory 
    {
        /*
         * Singleton for one application
         */
        static IMWStylePalette _impl = new MWStylePalette();

        public static IMWStylePalette NewInstance()
        {
            return _impl;
        }
    }
}
