using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net;
using System.Xml.Linq;
using MWDataSerilizationType;

namespace MWStylePalette
{
    public class MWStylePalette : IMWStylePalette
    {
        public void Dispose()
        {

        }
        private static SynchronizedCollection<MWStylePaletteSharer> sharerList = new SynchronizedCollection<MWStylePaletteSharer>();
        private static SynchronizedCollection<MWSPEnvironement> friendEnv = new SynchronizedCollection<MWSPEnvironement>();
        public string ShareRequest(MWShareRequestJSON request)
        {
            string response = string.Empty;

            //check if the user ID and friend ID is valid
            //TODO: update DB schema to get linq for checking

            Boolean createList = true;

            //lock??

            //check if list is empty
            if (sharerList.Count != 0)
            {
                //check if a list of that sharer already exists
                var sharer = sharerList.FirstOrDefault(s => s.UserID.Equals(request.UserId));
                if (sharer != null)
                {
                    createList = false;
                    if (!sharer.FriendPending.Contains(request.FriendId))
                    {
                        //check if already added
                        if(!sharer.Friends.Contains(request.FriendId))
                            sharer.FriendPending.Add(request.FriendId);
                    }
                    else
                    {
                        //generate error: friend request pending?
                        //or ignore as human (frustation) error
                    }
                    response = sharer.ID;
                }

            }

            //else create a list
            if (createList)
            {
                MWStylePaletteSharer newSharer = new MWStylePaletteSharer(request.UserId);
                //add the friend id to pending
                newSharer.FriendPending.Add(request.FriendId);
                sharerList.Add(newSharer);
                response = newSharer.ID;
            }

            //send request to the friend. 
            //TODO: find a method for notification update

            return response;
        }
        public string AcceptRequest(MWAcceptRequestJSON request)
        {
            string response = string.Empty;

            //find pending request in list
            var sharer = sharerList.FirstOrDefault(s => s.ID.Equals(request.ID));
            if (sharer != null)
            {
                if (sharer.FriendPending.Contains(request.UserId))
                {
                    //accept the friend
                    sharer.FriendPending.Remove(request.UserId);
                    sharer.Friends.Add(request.UserId);


                    if (friendEnv.Count != 0)
                    {
                        //check if already exists
                        var env = friendEnv.FirstOrDefault(e => e.UserId.Equals(request.UserId));
                        if (env == null)
                        {
                            //add in the environment list
                            MWSPEnvironement newEnv = new MWSPEnvironement
                            {
                                UserId = request.UserId
                            };
                            newEnv.Items.AddRange(request.Items);
                            friendEnv.Add(newEnv);
                            response = "accepted";
                        }//else do nothing?
                    }
                    else //it is the first acceptance!!
                    {
                        //add in the environment list
                        MWSPEnvironement newEnv = new MWSPEnvironement
                        {
                            UserId = request.UserId
                        };
                        newEnv.Items.AddRange(request.Items);
                        friendEnv.Add(newEnv);
                        response = "accepted";
                    }

                }
                else
                {
                    // error: go away, he doesn't wanna share
                }
            }
            else
            {
                //error: invalid ID
            }

            return response;
        }
        public string StyleChangeNotify(MWSPEnvironmentJSON style)
        {
            string response = string.Empty;
            List<string> users = new List<string>();
            //lock this method
            lock (this)
            {
                //retrieve other clients from the sharerList
                foreach (var sharer in sharerList)
                {
                    if (sharer.UserID.Equals(style.UserId))
                    {
                        users.AddRange(sharer.Friends);
                        break;
                    }
                    else if (sharer.Friends.Contains(style.UserId))
                    {
                        //check the friends' IDs
                        foreach (string userId in sharer.Friends)
                        {
                            if (!userId.Equals(style.UserId))
                                users.Add(userId);
                        }
                        users.Add(sharer.UserID);
                    }
                }
                //forward change notification to each client
                //TODO: same method as above, probably looped.
            }
            return response;
        }
        public string SaveOutfit(MWSPEnvironmentJSON style)
        {
            string response = string.Empty;

            //validate token

            /*
             * Consider leaving it up to Sean
             */
            return response;
        }
        public string GetPreviousEnvironment(string UserId)
        {
            string response = string.Empty;

            //look up userid in the friendenv list
            var prevEnv = friendEnv.FirstOrDefault(e => e.UserId.Equals(UserId));
            if (prevEnv != null)
            {
                response = prevEnv.ToString();
            }
            return response;
        }
    }
}
