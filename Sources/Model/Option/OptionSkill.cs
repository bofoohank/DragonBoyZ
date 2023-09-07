using System;
using DragonBoyZ.Model.ModelBase;

namespace DragonBoyZ.Model.Option
{
    public class OptionSkill : OptionBase
    {
        public override object Clone()
        {
            return new OptionSkill()
            {
                Id = Id,
                Param = Param
            };
        }
    }
}