using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.Handlers.Item;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Threading;
using DragonBoyZ.Model.Item;
using DragonBoyZ.Model.ModelBase;
using DragonBoyZ.Model.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sources.Model.Clan
{
    public class ClanBox
    {
        public List<Item> ItemBoxClan { get; set; }
        public ClanBox()
        {
            ItemBoxClan = new List<Item>(2);
        }
        public int BoxLength()
        {
            return 20;
        }

    }
}
