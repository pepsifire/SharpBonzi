﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace SharpBonzi
{
    
    public class Permissions
    {
        //Check if command issuer is owner of the bot (application)
        static public bool CheckOwner(DiscordUser user)
        {
            if (user == Program.discord.CurrentApplication.Owner) 
                return true;
            else
                return false;
        }
    }
}
